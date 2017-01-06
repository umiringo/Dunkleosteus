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
            [components setDay:([components day]+1)];

            NSDateComponents *triggerDate = [[NSDateComponents alloc] init];
            triggerDate.weekday = components.weekday;
            triggerDate.hour = 19;
            UNCalendarNotificationTrigger *trigger = [UNCalendarNotificationTrigger triggerWithDateMatchingComponents:triggerDate repeats:YES];
            
            UNMutableNotificationContent *content = [[UNMutableNotificationContent alloc] init];
            content.badge = [NSNumber numberWithInteger:1];
            content.title = [NSString localizedUserNotificationStringForKey:__makeNSString(title) arguments:nil];
            content.body = [NSString localizedUserNotificationStringForKey:__makeNSString(message) arguments:nil];
            content.sound = [UNNotificationSound defaultSound];
            
            UNNotificationRequest *request = [UNNotificationRequest requestWithIdentifier:@"repeatweek" content:content trigger:trigger];
            [[UNUserNotificationCenter currentNotificationCenter] addNotificationRequest:request withCompletionHandler:^(NSError * _Nullable error) {
                if(error) {
                    NSLog(@"%@", error);
                }
            }];
        } else {

                UILocalNotification *notification = [[UILocalNotification alloc] init];
    // 设置触发通知的时间
    //需要使用时间戳
    NSDate *fireDate = [NSDate dateWithTimeIntervalSince1970:alertTime];
    NSLog(@"fireDate=%@",fireDate);
    notification.fireDate = fireDate;
    // 时区
    notification.timeZone = [NSTimeZone defaultTimeZone];
    // 设置重复的间隔
    notification.repeatInterval = 0;//0表示不重复
    // 通知内容
    notification.alertBody =  string;
    notification.applicationIconBadgeNumber = 1;
    // 通知被触发时播放的声音
    notification.soundName = UILocalNotificationDefaultSoundName;
    // 通知参数

    NSDictionary *userDict = [NSDictionary dictionaryWithObject:string forKey:key];
    notification.userInfo = userDict;

    // ios8后，需要添加这个注册，才能得到授权
    if ([[UIApplication sharedApplication] respondsToSelector:@selector(registerUserNotificationSettings:)]) {
        UIUserNotificationType type =  UIUserNotificationTypeAlert | UIUserNotificationTypeBadge | UIUserNotificationTypeSound;
        UIUserNotificationSettings *settings = [UIUserNotificationSettings settingsForTypes:type
                                                                                 categories:nil];
        [[UIApplication sharedApplication] registerUserNotificationSettings:settings];
        // 通知重复提示的单位，可以是天、周、月
        //        notification.repeatInterval = NSCalendarUnitDay;
    } else {
        // 通知重复提示的单位，可以是天、周、月
        //        notification.repeatInterval = NSDayCalendarUnit; //ios7使用
    }

    // 执行通知注册
    [[UIApplication sharedApplication] scheduleLocalNotification:notification];
            
        }
    }

    void NotificationMessageInterval(const char* title, const char* message, int interval)
    {
        if (SYSTEM_VERSION_GREATER_OR_EQUAL_TO(@"10.0")) {
            UNMutableNotificationContent *content = [[UNMutableNotificationContent alloc] init];
            content.badge = [NSNumber numberWithInteger:1];
            content.title = [NSString localizedUserNotificationStringForKey:__makeNSString(title) arguments:nil];
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