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
            UNUserNotificationCenter *center = [UNUserNotificationCenter currentNotificationCenter];
            [center removeAllPendingNotificationRequests];
            [center removeAllDeliveredNotifications];
            [[UIApplication sharedApplication] setApplicationIconBadgeNumber:0];
        } else {
            UILocalNotification *localNotif = [[UILocalNotification alloc] init];
            localNotif.applicationIconBadgeNumber = 0;
            [[UIApplication sharedApplication] presentLocalNotificationNow:localNotif];
            [[UIApplication sharedApplication] cancelAllLocalNotifications];
        }
    }

    void NotificationMessageRepeatWeek(const char* title, const char* message)
    {
        if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"10.0")) {
            NSDate *now = [NSDate date];
            // Get yesterday
            NSCalendar *gregorian = [[NSCalendar alloc] initWithCalendarIdentifier:NSCalendarIdentifierGregorian];
            NSDateComponents *components = [gregorian components:NSCalendarUnitWeekday | NSCalendarUnitYear | NSCalendarUnitMonth | NSCalendarUnitDay fromDate:now];
            [components setDay:([components day]-1)];

            NSDateComponents *triggerDate = [[NSDateComponents alloc] init];
            triggerDate.weekday = components.weekday;
            triggerDate.hour = 19;
            UNCalendarNotificationTrigger *trigger = [UNCalendarNotificationTrigger triggerWithDateMatchingComponents:triggerDate repeats:YES];
            
            UNMutableNotificationContent *content = [[UNMutableNotificationContent alloc] init];
            //content.title = [NSString localizedUserNotificationStringForKey:__makeNSString(title) arguments:nil];
            content.body = [NSString localizedUserNotificationStringForKey:__makeNSString(message) arguments:nil];
            content.sound = [UNNotificationSound defaultSound];
            
            UNNotificationRequest *request = [UNNotificationRequest requestWithIdentifier:@"repeatweek" content:content trigger:trigger];
            [[UNUserNotificationCenter currentNotificationCenter] addNotificationRequest:request withCompletionHandler:^(NSError * _Nullable error) {
                if(error) {
                    NSLog(@"%@", error);
                }
            }];
        } else {
            NSDate *now = [NSDate date];
            NSCalendar *gregorian = [[NSCalendar alloc] initWithCalendarIdentifier:NSCalendarIdentifierGregorian];
            NSDateComponents *components = [gregorian components:NSCalendarUnitWeekday | NSCalendarUnitYear | NSCalendarUnitMonth | NSCalendarUnitDay fromDate:now];
            [components setDay:([components day]-1)];
            [components setWeekday:([components weekOfYear] + 1)];
            NSDate *triggerDate = [gregorian dateFromComponents:components];
            
            UILocalNotification *notification = [[UILocalNotification alloc] init];
            notification.fireDate = triggerDate;
            notification.timeZone = [NSTimeZone defaultTimeZone];
            notification.repeatInterval = NSCalendarUnitWeekday;
            notification.alertBody = __makeNSString(message);
            notification.soundName = UILocalNotificationDefaultSoundName;
            
            [[UIApplication sharedApplication] scheduleLocalNotification:notification];
        }
    }

    void NotificationMessageInterval(const char* title, const char* message, int interval)
    {
        if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"10.0")) {
            UNMutableNotificationContent *content = [[UNMutableNotificationContent alloc] init];
            content.badge = [NSNumber numberWithInteger:1];
            //content.title = [NSString localizedUserNotificationStringForKey:__makeNSString(title) arguments:nil];
            content.body = [NSString localizedUserNotificationStringForKey:__makeNSString(message) arguments:nil];
            content.sound = [UNNotificationSound defaultSound];
            
            UNTimeIntervalNotificationTrigger *trigger = [UNTimeIntervalNotificationTrigger triggerWithTimeInterval:interval repeats:NO];
            UNNotificationRequest *request = [UNNotificationRequest requestWithIdentifier:@"interval notification" content:content trigger:trigger];
            [[UNUserNotificationCenter currentNotificationCenter] addNotificationRequest:request withCompletionHandler:^(NSError * _Nullable error) {
                if(error) {
                    NSLog(@"%@", error);
                }
            }];
        } else {

        }
    }

}