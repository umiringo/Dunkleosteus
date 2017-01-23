#import "IAPManager.h"

@implementation IAPManager

- (void) attachObserver {
    NSLog(@"AttachObserver");
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}

- (BOOL)CanMakePay {
    return [SKPaymentQueue canMakePayments];
}

-(void)requestProductData:(NSString *)productIdentifiers {
    NSArray *idArray = [productIdentifiers componentsSeparatedByString:@"\t"];
    NSSet *idSet = [NSSet setWithArray:idArray];
    [self sendRequest:idSet];
}

// 向苹果商店获取可销售商品
-(void)sendRequest:(NSSet *)idSet {
    SKProductsRequest *request = [[SKProductsRequest alloc] initWithProductIdentifiers:idSet];
    request.delegate = self;
    [request start];
}

// 收到可销售商品之后的回调
-(void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response {
    NSArray *products = response.products;
    for(SKProduct *p in products) {
        // 通知unity
        UnitySendMessage("GameController", "OnProductsRequest", [[self productInfo:p] UTF8String]);
        if(nil == mProductHash) {
            mProductHash = [[NSMutableDictionary alloc] init];
        }
        [mProductHash setValue:p forKey:p.productIdentifier];
    }

    for(NSString *invalidProductId in response.invalidProductIdentifiers) {
        NSLog(@"Invalid product id : %@", invalidProductId);
    }
}

-(void)buyRequest:(NSString *)productIdentifier {
    SKProduct *p = mProductHash[productIdentifier];
    if( nil == p ) {
        UnitySendMessage("GameController", "OnProductNotExisted", [productIdentifier UTF8String]);
        return;
    }
    SKPayment *payment = [SKPayment paymentWithProduct:p];
    [[SKPaymentQueue defaultQueue] addPayment:payment];
}

-(NSString *)productInfo:(SKProduct *)product {
    NSNumberFormatter *numberFormatter = [[NSNumberFormatter alloc] init];
    [numberFormatter setFormatterBehavior:NSNumberFormatterBehavior10_4];
    [numberFormatter setNumberStyle:NSNumberFormatterCurrencyStyle];
    [numberFormatter setLocale:product.priceLocale];
    NSString *formattedString = [numberFormatter stringFromNumber:product.price];
    NSArray *info = [NSArray arrayWithObjects:product.localizedTitle, product.localizedDescription, formattedString, product.productIdentifier,nil];
    return [info componentsJoinedByString:@"\t"];
}

-(void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions {
    for(SKPaymentTransaction *transaction in transactions) {
        switch(transaction.transactionState) {
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
            default:
                break;
        }
    }
}

-(void)completeTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"Complete transaction : %@", transaction.transactionIdentifier);
    UnitySendMessage("GameController", "OnPurchaseSuccess", [[NSString stringWithFormat:@"%@", transaction.payment.productIdentifier] UTF8String]);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];

}

-(void)failedTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"Failed transaction : %@", transaction.transactionIdentifier);
    if(transaction.error.code != SKErrorPaymentCancelled) {
        NSLog(@"!Cancelled");
    }
     UnitySendMessage("GameController", "OnPurchaseEnd", [[NSString stringWithFormat:@"%@", transaction.payment.productIdentifier] UTF8String]);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

-(void)restoreTransaction:(SKPaymentTransaction *)transaction {
    NSLog(@"Restore transaction : %@", transaction.transactionIdentifier);
     UnitySendMessage("GameController", "OnPurchaseEnd", [[NSString stringWithFormat:@"%@", transaction.payment.productIdentifier] UTF8String]);
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

@end