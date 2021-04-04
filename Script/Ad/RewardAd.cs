using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ingame
{
    public static class RewardAd 
    {
        private readonly static string game_id = "4055163";
        private readonly static string placementId = "Rewarded_Android";
        private static bool testmode = false;

        public static void Show_RewardAd(Action<bool> callback)
        {

            if (PlayerPrefs.GetInt("noads", 0) == 0)
            {
                if (Advertisement.IsReady(placementId))
                {
                    callback(true);
                    Advertisement.Show(placementId);
                }

                else
                {
                    callback(false);

                }
            }
        }
    }
}
