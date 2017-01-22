#import "IAPInterface.h"
#import "IAPManager.h"

@implementation IAPInterface

IAPManager *iapManager = nil;

void InitIAPManager() {
    iapManager = [[IAPManager alloc] init];
    [iapManager attachObserver];
}

bool IsProductAvailable() {
    return [iapManager CanMakePay];
}

void RequestProductInfo(void *p) {
    NSString *list = [NSString stringWithUTF8String:p];
    NSLog(@"productKey : %@", list);
    [iapManager requestProductData:list];
}

void BuyProduct(void *p) {
    [iapManager buyRequest:[NSString stringWithUTF8String:p]];
}

@end