using UnityEngine;
using UnityEngine.Advertisements;
using System;
using System.Collections;

namespace Ingame
{
    public class Banner : MonoBehaviour
    {

        private readonly string game_id = "4055163";
        private readonly string placementId = "Banner_Android";
        private bool testmode = false;

        // Start is called before the first frame update
        void Start()
        {
            if (PlayerPrefs.GetInt("noads", 0) == 0)
            {
                Advertisement.Initialize(game_id, testmode);
                StartCoroutine(ShowBannerWhenReady());
            }
        }

        IEnumerator ShowBannerWhenReady()
        {
            while (!Advertisement.IsReady(placementId))
            {
                yield return new WaitForSeconds(0.5f);
            }
            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
            Advertisement.Banner.Show(placementId);
            yield return null;
        }

        private void OnDestroy()
        {
            Advertisement.Banner.Hide(true);
        }

    }
}
