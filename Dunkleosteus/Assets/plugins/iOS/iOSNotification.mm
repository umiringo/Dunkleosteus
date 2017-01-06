#define SYSTEM_VERSION_GREATER_OR_EQUAL_TO(v) ([[[UIDevice currentDevice] systemVersion] compare:v options:NSNumericSearch] != NSOrderedAscending)
#import <UserNotifications/UserNotifications.h>

extern "C"
{
    NSString* __makeNSString(const char* cstring)
    {
        if(cstring == NULL) {
            return nil;
        }
        NSString *nsstring = [[NSString alloc] initWithCString:cstring encoding:NSUTF8StringEncoding];
        
        return nsstring;
    }
    
    void RegisterNotification()
    {
        if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"10.0")) {
            NSLog(@"----------------RegisterNotification");
            UNUserNotificationCenter *center = [UNUserNotificationCenter currentNotificationCenter];
            [center requestAuthorizationWithOptions:(UNAuthorizationOptionBadge | UNAuthorizationOptionSound | UNAuthorizationOptionAlert) completionHandler:^(BOOL granted, NSError * _Nullable error) {
                if(!error) {
                    NSLog(@"register notification success!");
                }
            }];
        } else if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"8.0")) {
            UIUserNotificationSettings *settings = [UIUserNotificationSettings settingsForTypes:UIUserNotificationTypeSound | UIUserNotificationTypeAlert | UIUserNotificationTypeBadge categories:nil];
            [[UIApplication sharedApplication] registerUserNotificationSettings:settings];
        }
    }
 
    void ClearNotification()
    {
        if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"10.0")) {
            NSLog(@"------------------------ClearNotification------------------");
            UNUserNotificationCenter *center = [UNUserNotificationCenter currentNotificationCenter];
            [center removeAllPendingNotificationRequests];
            [center removeAllDeliveredNotifications];
        } else {
            UILocalNotification *localNotif = [[UILocalNotification alloc] init];
            localNotif.applicationIconBadgeNumber = 0;
            [[UIApplication sharedApplication]presentLocalNotificationNow:localNotif];
            [[UIApplication sharedApplication]cancelAllLocalNotifications];
        }
    }

    void LocalRepeatWeekNotificationMessage(const char* title, const char* message)
    {
        if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"10.0")) {
            NSLog(@"------NotificationMessage greater than 10.0 interval = %d", interval);
            UNUserNotificationCenter* center = [UNUserNotificationCenter currentNotificationCenter];
            UNMutableNotificationContent *content = [[UNMutableNotificationContent alloc] init];
            content.badge = [NSNumber numberWithInteger:1];
            content.body = [NSString localizedUserNotificationStringForKey:__makeNSString(message) arguments:nil];
            content.sound = [UNNotificationSound defaultSound];         
        } else {
            
        }
    }

    void LocalIntervalNotificationMessage(const char* title, const char* message, int interval)
    {
        if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"10.0")) {
            
        } else {

        }
    }

}