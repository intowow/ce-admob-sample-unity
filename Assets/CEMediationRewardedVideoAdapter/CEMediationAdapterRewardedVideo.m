// Version 2
// minimum support Intowow SDK 3.16.0
//
//  CEMediationAdapterRewardedVideo.m
//
//  Copyright © 2017 intowow. All rights reserved.
//

#define CEMediationAdapterRewardedVideoVersion @"2.0"
#define LoadAdTimeout 10

#import "CEMediationAdapterRewardedVideo.h"
#import "I2WAPI.h"
#import "CERewardedVideoAD.h"

/// Constant for CrystalExpress Ad Network adapter error domain.
static NSString *const customEventErrorDomain = @"com.intowow.CrystalExpress";

@interface CEMediationAdapterRewardedVideo () <CERewardedVideoADDelegate>

@property (nonatomic, weak) id<GADMRewardBasedVideoAdNetworkConnector> rewardBasedVideoAdConnector;
@property (nonatomic, strong) CERewardedVideoAD * ceRewardedAD;
@property (nonatomic, assign) BOOL adLoaded;

@end

@implementation CEMediationAdapterRewardedVideo

#pragma mark - GADMRewardBasedVideoAdNetworkAdapter
+ (Class<GADAdNetworkExtras>)networkExtrasClass
{
    return nil;
}

+ (NSString *)adapterVersion
{
    return CEMediationAdapterRewardedVideoVersion;
}

- (instancetype)initWithRewardBasedVideoAdNetworkConnector:(id<GADMRewardBasedVideoAdNetworkConnector>)connector
{
    [I2WAPI initWithVerboseLog:YES isTestMode:YES];

    if (!connector) {
        return nil;
    }

    self = [super init];
    if (self) {
        _rewardBasedVideoAdConnector = connector;
    }
    return self;
}

- (void)setUp
{
    NSDictionary * dic = [_rewardBasedVideoAdConnector credentials];
    NSString *placement = [dic objectForKey:GADCustomEventParametersServer];
    if (placement == nil || [placement isEqualToString:@""]) {
        NSString *description = @"Invalid placment from server";
        NSDictionary *userInfo = @{NSLocalizedDescriptionKey : description, NSLocalizedFailureReasonErrorKey : description};
        NSError * error = [NSError errorWithDomain:customEventErrorDomain code:0 userInfo:userInfo];
        [self.rewardBasedVideoAdConnector adapter:self didFailToSetUpRewardBasedVideoAdWithError:error];
        return;
    }
    _ceRewardedAD = [[CERewardedVideoAD alloc] initWithPlacement:placement];
    _ceRewardedAD.delegate = self;

    [self.rewardBasedVideoAdConnector adapterDidSetUpRewardBasedVideoAd:self];
}

- (void)requestRewardBasedVideoAd
{
    self.adLoaded = NO;
    [_ceRewardedAD loadAdWithTimeout:LoadAdTimeout];
}

- (void)presentRewardBasedVideoAdWithRootViewController:(UIViewController *)viewController
{
    if (self.adLoaded) {
        [_ceRewardedAD showFromViewController:viewController animated:YES];
    } else {
        NSLog(@"[CrystalExpress]: Ad is not ready.");
    }
}

- (void)stopBeingDelegate
{
    self.rewardBasedVideoAdConnector = nil;
    self.ceRewardedAD = nil;
}

#pragma mark - CESplash2ADDelegate

- (void)rewardedVideoADDidLoaded:(CERewardedVideoAD *)rewardedVideoAD
{
    self.adLoaded = YES;
    [self.rewardBasedVideoAdConnector adapterDidReceiveRewardBasedVideoAd:self];
}

- (void)rewardedVideoADDidFail:(CERewardedVideoAD *)rewardedVideoAD withError:(NSError *)error
{
    self.adLoaded = NO;
    [self.rewardBasedVideoAdConnector adapter:self didFailToLoadRewardBasedVideoAdwithError:error];
}

- (void)rewardedVideoADDidClick:(CERewardedVideoAD *)rewardedVideoAD
{
    [self.rewardBasedVideoAdConnector adapterDidGetAdClick:self];
}

- (void)rewardedVideoADWillDisplay:(CERewardedVideoAD *)rewardedVideoAD
{
    [self.rewardBasedVideoAdConnector adapterDidOpenRewardBasedVideoAd:self];
}

- (void)rewardedVideoADDidVideoStart:(CERewardedVideoAD *)rewardedVideoAD
{
    [self.rewardBasedVideoAdConnector adapterDidStartPlayingRewardBasedVideoAd:self];
}

- (void)rewardedVideoADDidDismiss:(CERewardedVideoAD *)rewardedVideoAD
{
    [self.rewardBasedVideoAdConnector adapterDidCloseRewardBasedVideoAd:self];
}

- (void)rewardedVideoADDidRewardUser:(CERewardedVideoAD *)rewardedVideoAD
{
    GADAdReward * reward = [[GADAdReward alloc] init];
    [self.rewardBasedVideoAdConnector adapter:self didRewardUserWithReward:reward];
}
@end
