using System.Collections.Generic;

/*
 해당 클래스의 목적 : REST API를 호출할 때 해당 틀에 맞는 요청을 보내기 위함   
 */
namespace Ingame
{
    public class LeaderboardVo
    {
        // 아이피 , 포트번호 , 테이블명 
        public class SetSingleUserScoreRequest
        {
            public int transactionId;
            public double score;
            public string extra;
        }

        public class Response
        {
            public class ResponseHeader
            {
                public bool isSuccessful;
                public int resultCode;
                public string resultMessage;
                public string transactionId;
            }
            public int transactionId;
            public ResponseHeader header;
        }

        public class UserInfo
        {
            public int resultCode;
            public string userId;
            public int transactionId;
            public double score;
            public int rank;
            public int preRank;
            public string extra;
            public string date;
        }

        public class UserInfosByRange
        {
            public int resultCode;
            public int factor;
            public List<UserInfo> userInfos;
        }

        public class GetSingleUserInfoResponse : Response
        {
            public UserInfo userInfo;
        }

        public class GetMultipleUserInfoByRangeResponse : Response
        {
            public UserInfosByRange userInfosByRange;
        }

        public class SetSingleUserScoreResponse : Response
        {
            public class ResultInfo
            {
                public int transactionId;
                public int resultCode;
                public string userId;
            }

            public ResultInfo resultInfo;
        }
    }
}