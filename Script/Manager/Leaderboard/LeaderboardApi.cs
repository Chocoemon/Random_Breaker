using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Toast.Gamebase;
using LitJson;
using UnityEngine.Networking;

namespace Ingame
{
    public static class LeaderboardApi
    {
        private static string URL = "https://api-gamebase.cloud.toast.com/tcgb-leaderboard/v1.3/apps/";
        private static string UserID = Gamebase.GetUserID();
        private static string AppID = "lKLQObvM";
        private const int TRANSACTION_ID = 12345;
       

        private const int LEADERBOARD_SUCCESS = 0;
        private const int LEADERBOARD_SUCCESS_BUT_NOT_UPDATE = 1;

        private const string ERROR_MESSAGE_GET_SINGLE_USER_INFO = "Failed to get single user info.";
        private const string ERROR_MESSAGE_GET_MULTIPLE_USER_INFO_BY_RANGE = "Failed to get multiple user info by range.";
        private const string ERROR_MESSAGE_SET_SINGLE_USER_SCORE = "Failed to save user score.";
        private const string ERROR_MESSAGE_DELETE_SINGLE_USER_INFO = "Failed to delete user info.";


        public static void Get_Single_Score(int Factor,Action<LeaderboardVo.UserInfo>callback)
        {
            string url = URL + string.Format("{0}/factors/{1}/users?userId={2}", AppID, Factor, UserID);
            WebRequestObject.Instance.Request(
                UnityWebRequest.Get(url),
                (message) =>
                {
                    if (string.IsNullOrEmpty(message) == true)
                    {
                        Logger.Debug(ERROR_MESSAGE_GET_SINGLE_USER_INFO, typeof(LeaderboardApi));
                        callback(null);
                        return;
                    }

                    var vo = JsonMapper.ToObject<LeaderboardVo.GetSingleUserInfoResponse>(message);
                    if (vo.header.resultCode != LEADERBOARD_SUCCESS && vo.header.resultCode != LEADERBOARD_SUCCESS_BUT_NOT_UPDATE)
                    {
                        Logger.Debug(string.Format("{0} (Code={1})", ERROR_MESSAGE_GET_SINGLE_USER_INFO, vo.header.resultCode), typeof(LeaderboardApi));
                        callback(null);
                        return;
                    }
                    callback(vo.userInfo);
                });
        

        }

        public static void AddExp(float exp, int Factor)
        {
            string url = URL +string.Format("{0}/factors/{1}/users/{2}/score", AppID, Factor, UserID);

            LeaderboardVo.SetSingleUserScoreRequest requestVo = new LeaderboardVo.SetSingleUserScoreRequest()
            {
                transactionId = TRANSACTION_ID,
                score = exp,
                extra = null
            };

            byte[] body = Encoding.UTF8.GetBytes(JsonMapper.ToJson(requestVo));

            UnityWebRequest request = UnityWebRequest.Put(url, body);
            request.method = UnityWebRequest.kHttpVerbPOST;

            WebRequestObject.Instance.Request(
                request,
                (message) =>
                {
                    if (string.IsNullOrEmpty(message) == true)
                    {
                        Logger.Debug(ERROR_MESSAGE_SET_SINGLE_USER_SCORE, typeof(LeaderboardApi));
                        return;
                    }

                    var vo = JsonMapper.ToObject<LeaderboardVo.SetSingleUserScoreResponse>(message);
                    if (vo.header.resultCode != LEADERBOARD_SUCCESS && vo.header.resultCode != LEADERBOARD_SUCCESS_BUT_NOT_UPDATE)
                    {
                        Logger.Debug(string.Format(" {0} (Code={1})", ERROR_MESSAGE_SET_SINGLE_USER_SCORE, vo.header.resultCode), typeof(LeaderboardApi));
                    }
                });
        }


        public static void GetMultipleUserInfoByRange(int start,int size,int Factor, Action<LeaderboardVo.UserInfosByRange> callback)
        {
            string url = URL + string.Format("{0}/factors/{1}/users?start={2}&size={3}", AppID, Factor, start,size);

            WebRequestObject.Instance.Request(
                UnityWebRequest.Get(url),
                (message) =>
                {
                    if (string.IsNullOrEmpty(message) == true)
                    {
                        Logger.Debug(ERROR_MESSAGE_GET_MULTIPLE_USER_INFO_BY_RANGE, typeof(LeaderboardApi));
                        callback(null);
                        return;
                    }

                    var vo = JsonMapper.ToObject<LeaderboardVo.GetMultipleUserInfoByRangeResponse>(message);
                    if (vo.header.resultCode != LEADERBOARD_SUCCESS && vo.header.resultCode != LEADERBOARD_SUCCESS_BUT_NOT_UPDATE)
                    {
                        Logger.Debug(string.Format("{0} (Code={1})", ERROR_MESSAGE_GET_MULTIPLE_USER_INFO_BY_RANGE, vo.header.resultCode), typeof(LeaderboardApi));
                        callback(null);
                        return;
                    }

                    callback(vo.userInfosByRange);
                });
        }

    }

   

}
