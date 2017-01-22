#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

@interface IAPManager : NSObject<SKProductsRequestDelegate, SKPaymentTransactionObserver> {
    NSMutableDictionary *mProductHash;
}

- (void) attachObserver;
- (BOOL) CanMakePay;
- (void) requestProductData : (NSString *)productIndentifiers;
- (void) buyRequest:(NSString *)productIndentifiers;

@end