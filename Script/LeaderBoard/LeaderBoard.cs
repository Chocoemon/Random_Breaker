using UnityEngine;
using UnityEngine.UI;
using Toast.Gamebase;

/// <summary>
/// Factor 1 = 전체 랭킹, 2 = 일간 랭킹, 3 = 주간 랭킹 
/// </summary>


namespace Ingame
{
    public class LeaderBoard : MonoBehaviour
    {
        private const int TOP_RANK_LIST_COUNT = 50;
        private const int LEADERBOARD_NO_RECORD_HEIGHT = 300;
        private const int LEADERBOARD_RECORD_HEIGHT = 153;
        [SerializeField]
        private LeaderBoardRankItem myRankItem;
        [SerializeField]
        private LeaderBoardRankView topRankView;
        [SerializeField]
        private RectTransform rectTransform;
        [SerializeField]
        private Image Day_Button, Day_Text;
        [SerializeField]
        private Image Month_Button,Month_Text;
        [SerializeField]
        private Image All_Button, AllText;
        [SerializeField]
        private GameObject Day_CheckObj, Month_CheckObj, All_CheckObj;
        
        private Color Is_Pushed = new Color(253f/255f, 255f/255f, 0f/255f);
        private Color Isnt_Pushed = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            Initialize();
        }

        public void Onclick_Leader_Exit()
        {

            this.gameObject.SetActive(false);

        }

        /// <summary>
        /// 전체 기록 조회를 먼저 하므로, Factor를 0으로 만들어줌 
        /// </summary>
        private void Initialize()
        {
            Day_Button.color = Isnt_Pushed;
            Day_Text.color = Isnt_Pushed;
            Day_CheckObj.SetActive(false);
            Month_Button.color = Isnt_Pushed;
            Month_Text.color = Isnt_Pushed;
            Month_CheckObj.SetActive(false);
            AllText.color = Is_Pushed;
            All_Button.color = Is_Pushed;
            All_CheckObj.SetActive(true);
            LeaderboardApi.Get_Single_Score(1,
                (userData) =>
                {
                    if (userData == null)
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(), 0, 0);
                    }

                    else
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(),userData.rank,userData.score);
                    }
                });

            LeaderboardApi.GetMultipleUserInfoByRange(1, TOP_RANK_LIST_COUNT,1,
                (userInfosByRange) =>
                {
                    if (userInfosByRange == null || userInfosByRange.userInfos == null || userInfosByRange.userInfos.Count == 0)
                    {
                        Vector2 sizeDelta = rectTransform.sizeDelta;
                        sizeDelta.y = LEADERBOARD_NO_RECORD_HEIGHT;
                        rectTransform.sizeDelta = sizeDelta;
                    }

                    topRankView.SetInfo(userInfosByRange);
                });
        }

        /// <summary>
        /// Factor 1을 참조함 
        /// </summary>
        public void get_Daily()
        {
            Day_Button.color = Is_Pushed;
            Day_Text.color = Is_Pushed;
            Day_CheckObj.SetActive(true);
            Month_Button.color = Isnt_Pushed;
            Month_Text.color = Isnt_Pushed;
            Month_CheckObj.SetActive(false);
            AllText.color = Isnt_Pushed;
            All_Button.color = Isnt_Pushed;
            All_CheckObj.SetActive(false);
            LeaderboardApi.Get_Single_Score(2,
                (userData) =>
                {
                    if (userData == null)
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(), 0, 0);
                    }

                    else
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(), userData.rank, userData.score);
                    }
                });

            LeaderboardApi.GetMultipleUserInfoByRange(1, TOP_RANK_LIST_COUNT,2,
                (userInfosByRange) =>
                {

                    if (userInfosByRange == null || userInfosByRange.userInfos == null || userInfosByRange.userInfos.Count == 0)
                    {
                        Vector2 sizeDelta = rectTransform.sizeDelta;
                        sizeDelta.y = LEADERBOARD_NO_RECORD_HEIGHT;
                        rectTransform.sizeDelta = sizeDelta;
                    }

                    topRankView.SetInfo(userInfosByRange);

                });
        }

        public void get_Monthly()
        {
            Day_Button.color = Isnt_Pushed;
            Day_Text.color = Isnt_Pushed;
            Day_CheckObj.SetActive(false);
            Month_Button.color = Is_Pushed;
            Month_Text.color = Is_Pushed;
            Month_CheckObj.SetActive(true);
            AllText.color = Isnt_Pushed;
            All_Button.color = Isnt_Pushed;
            All_CheckObj.SetActive(false);

            LeaderboardApi.Get_Single_Score(3,
                (userData) =>
                {
                    if (userData == null)
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(), 0, 0);
                    }

                    else
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(), userData.rank, userData.score);
                    }
                });

            LeaderboardApi.GetMultipleUserInfoByRange(1, TOP_RANK_LIST_COUNT,3,
                (userInfosByRange) =>
                {

                    if (userInfosByRange == null || userInfosByRange.userInfos == null || userInfosByRange.userInfos.Count == 0)
                    {

                        Vector2 sizeDelta = rectTransform.sizeDelta;
                        sizeDelta.y = LEADERBOARD_NO_RECORD_HEIGHT;
                        rectTransform.sizeDelta = sizeDelta;
                    }

                    topRankView.SetInfo(userInfosByRange);
                });
        }

        public void get_all()
        {
            Day_Button.color = Isnt_Pushed;
            Day_Text.color = Isnt_Pushed;
            Day_CheckObj.SetActive(false);
            Month_Button.color = Isnt_Pushed;
            Month_Text.color = Isnt_Pushed;
            Month_CheckObj.SetActive(false);
            AllText.color = Is_Pushed;
            All_Button.color = Is_Pushed;
            All_CheckObj.SetActive(true);
            LeaderboardApi.Get_Single_Score(1,
                (userData) =>
                {

                    if (userData == null)
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(), 0, 0);
                    }

                    else
                    {
                        myRankItem.SetInfo(Gamebase.GetUserID(), userData.rank, userData.score);
                    }
                });

            LeaderboardApi.GetMultipleUserInfoByRange(1, TOP_RANK_LIST_COUNT,1,
                (userInfosByRange) =>
                {

                    if (userInfosByRange == null || userInfosByRange.userInfos == null || userInfosByRange.userInfos.Count == 0)
                    {
                        Vector2 sizeDelta = rectTransform.sizeDelta;
                        sizeDelta.y = LEADERBOARD_NO_RECORD_HEIGHT;
                        rectTransform.sizeDelta = sizeDelta;
                    }

                    topRankView.SetInfo(userInfosByRange);
                });
        }
    }


}
