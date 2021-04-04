using System;

namespace Toast.Gamebase.Gamebase_Intro
{

    public static class UserImplementation
    {
        private const string MESSAGE_NOT_IMPLEMENTED = "Not implemented.";

        public static void Initialize(Action<int> callback)
        {
            var configuration = new GamebaseRequest.GamebaseConfiguration();
            configuration.appID = "lKLQObvM";
            configuration.appVersion = "1.4.0";
            configuration.storeCode = "GG";
            configuration.displayLanguageCode = Gamebase.GetDisplayLanguageCode();
            Gamebase.Initialize(configuration, (launchingInfo, error) =>
            {
                if (Gamebase.IsSuccess(error) == true)
                {
                    SampleLogger.Log("Gamebase initialization is succeeded.");

                    var status = launchingInfo.launching.status;


                    if (status.code == GamebaseLaunchingStatus.IN_SERVICE)
                    {
                        SampleLogger.Log("Playable");
                        callback(1);
                    }

                    else
                    {
                        switch (status.code)
                        {
                            case GamebaseLaunchingStatus.REQUIRE_UPDATE:
                                {
                                    callback(2);
                                    SampleLogger.Log(string.Format("Playable message : {0}", launchingInfo.launching.status.message));
                                    break;
                                }

                            case GamebaseLaunchingStatus.IN_SERVICE_BY_QA_WHITE_LIST:
                                {
                                    SampleLogger.Log(string.Format("Playable message : {0}", launchingInfo.launching.status.message));
                                    break;
                                }

                            case GamebaseLaunchingStatus.IN_TEST:
                                {
                                    SampleLogger.Log(string.Format("Playable message : {0}", launchingInfo.launching.status.message));
                                    callback(1);
                                    break;
                                }

                            case GamebaseLaunchingStatus.IN_REVIEW:
                                {
                                    SampleLogger.Log(string.Format("Playable message : {0}", launchingInfo.launching.status.message));
                                    break;
                                }

                            case GamebaseLaunchingStatus.BLOCKED_USER:
                                {
                                    SampleLogger.Log(string.Format("Playable message : {0}", launchingInfo.launching.status.message));
                                    callback(3);
                                    break;
                                }

                            default:
                                {
                                    SampleLogger.Log(string.Format("Unable message : {0}", launchingInfo.launching.status.message));
                                    break;
                                }

                        }
                    }
                }
                else
                {
                    SampleLogger.Log(string.Format("Gamebase initialization is failed. Error is {0}", error.ToString()));


                }
            });
        }









    }





}