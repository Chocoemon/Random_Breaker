using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;

namespace Toast.Gamebase.Gamebase_Intro
{
    public class Intro : MonoBehaviour
    {
        [SerializeField]
        private Image bi;
        [SerializeField]
        private Image poweredBy;
        [SerializeField]
        private GameObject Require_Update;
        [SerializeField]
        private GameObject Set_nickname;

        [SerializeField]
        private GameObject Banned_User;

        private GameObject biTweenObject;
        private GameObject poweredByTweenObject;


        private void Start()
        {
            if (bi == null)
            {
                Debug.LogWarning("BI image not found.");
                return;
            }

            biTweenObject = bi.gameObject;
            poweredByTweenObject = poweredBy.gameObject;
            ShowBI();
        }

        private void ShowBI()
        {
            SampleLogger.Log("시작");
            BiTweenAlpha(biTweenObject, 0f, 1f, 1f, "Delay");
        }

        private void Delay()
        {
            PoweredByTweenAlpha(poweredByTweenObject, 0f, 1f, 1f, string.Empty);
            SampleLogger.Log("진행중");
            UserImplementation.Initialize(message => {
                if (message == 1)
                {
                    Login(); // 로그인 시키고, 리퀘스트 받아와서 extra data가 없다면 받아오게 해야함. 닉네임은 10자로 제한하기 
                    SampleLogger.Log("로그인 시도 ");
                }

                else if (message == 2)
                {
                    Require_Update.SetActive(true);
                    SampleLogger.Log("예외1");
                }

                else if (message == 3)
                {
                    Banned_User.SetActive(true);
                    SampleLogger.Log("예외2");
                }

                else
                {
                    SampleLogger.Log("예외3");

                }

            });
        }

        private void HideBI()
        {
            MobileAds.Initialize(initStatus => { } );
            BiTweenAlpha(biTweenObject, 1f, 0f, 1f, "LoadLoginScene");
            PoweredByTweenAlpha(poweredByTweenObject, 1f, 0f, 1f, string.Empty);
        }


        private void LoadLoginScene()
        {
            SceneManager.LoadSceneAsync("MainGame");
        }

        #region Alpha
        private void BiTweenAlpha(GameObject target, float from, float to, float time, string completeCallback)
        {
            TweenAlpha(target, from, to, time, "BiUpdateColor", completeCallback);
        }

        private void PoweredByTweenAlpha(GameObject target, float from, float to, float time, string completeCallback)
        {
            TweenAlpha(target, from, to, time, "PoweredByUpdateColor", completeCallback);
        }

        private void BiUpdateColor(float value)
        {
            Color color = bi.color;
            color.a = value;
            bi.color = color;
        }

        private void PoweredByUpdateColor(float value)
        {
            Color color = bi.color;
            color.a = value;
            poweredBy.color = color;
        }

        private void TweenAlpha(GameObject target, float from, float to, float time, string updateCallback, string completeCallback)
        {
            iTween.ValueTo(target, iTween.Hash(
                "from", from,
                "to", to,
                "time", time,
                "onupdatetarget", gameObject,
                "onupdate", updateCallback,
                "oncomplete", completeCallback,
                "oncompletetarget", gameObject));

        }
        #endregion


        #region UI_Button

        public void Quit_Button()
        {
            Application.Quit();
        }
        #endregion


        private void Login()
        {

            LoginForLastLoggedInProvider();
               
        }


        private void LoginForLastLoggedInProvider()
        {
            if (string.IsNullOrEmpty(Gamebase.GetLastLoggedInProvider()) == true)
           {
#if UNITY_ANDROID
                Gamebase.Login(GamebaseAuthProvider.GOOGLE, (authToken, error) =>
                {
                    if (Gamebase.IsSuccess(error) == true)
                    {
                        string userId = authToken.member.userId;
                        SampleLogger.Log(string.Format("Login succeeded. Gamebase userId is {0}", userId));
                        HideBI();
                    }

                    else
                    {
                        // Check the error code and handle the error appropriately.
                        SampleLogger.Log(string.Format("Login failed. error is {0}", error));
                        if (error.code == GamebaseErrorCode.AUTH_EXTERNAL_LIBRARY_ERROR)
                        {
                            GamebaseError moduleError = error.error; // GamebaseError.error object from external module
                            if (null != moduleError)
                            {
                                int moduleErrorCode = moduleError.code;
                                string moduleErrorMessage = moduleError.message;
                                SampleLogger.Log(string.Format("moduleErrorCode:{0}, moduleErrorMessage:{1}", moduleErrorCode, moduleErrorMessage));
                            }
                        }
                        if (error.code == GamebaseErrorCode.BANNED_MEMBER)
                        {
                            GamebaseResponse.Auth.BanInfo banInfo = GamebaseResponse.Auth.BanInfo.From(error);
                            if (banInfo != null)
                            {
                            }
                        }
                    }
                });
#elif UNITY_IOS
                Gamebase.Login(GamebaseAuthProvider.GAMECENTER, (authToken, error) =>
                {
                    if (Gamebase.IsSuccess(error) == true)
                    {
                        string userId = authToken.member.userId;
                        SampleLogger.Log(string.Format("Login succeeded. Gamebase userId is {0}", userId));
                        HideBI();
                    }

                    else
                    {
                        // Check the error code and handle the error appropriately.
                        SampleLogger.Log(string.Format("Login failed. error is {0}", error));
                        if (error.code == GamebaseErrorCode.AUTH_EXTERNAL_LIBRARY_ERROR)
                        {
                            GamebaseError moduleError = error.error; // GamebaseError.error object from external module
                            if (null != moduleError)
                            {
                                int moduleErrorCode = moduleError.code;
                                string moduleErrorMessage = moduleError.message;
                                SampleLogger.Log(string.Format("moduleErrorCode:{0}, moduleErrorMessage:{1}", moduleErrorCode, moduleErrorMessage));
                            }
                        }
                        if (error.code == GamebaseErrorCode.BANNED_MEMBER)
                        {
                            GamebaseResponse.Auth.BanInfo banInfo = GamebaseResponse.Auth.BanInfo.From(error);
                            if (banInfo != null)
                            {
                            }
                        }
                    }
                });
#endif

            }

            else
            {
                Gamebase.LoginForLastLoggedInProvider((authToken, error) =>
                {
                   if (Gamebase.IsSuccess(error) == true)
                    {
                        SampleLogger.Log("여기");
                        HideBI();
                    }

                });
            }
        }
    }
}



