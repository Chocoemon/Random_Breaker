
using UnityEngine;
using UnityEngine.UI;
using System;
using Toast.Gamebase;

namespace Ingame
{
    public class LeaderBoardRankItem : MonoBehaviour
    {

        private const string NONE_RANK_STRING = "-";
        [SerializeField]
        private Text rankText;
        [SerializeField]
        private Text usernicknameText;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Image MyRankMark;
        [SerializeField]
        private GameObject Rank1, Rank2, Rank3;


        public void SetInfo(LeaderboardVo.UserInfo userInfo)
        {
            SetInfo(userInfo.userId,userInfo.rank, userInfo.score);
            Rank1.SetActive(false);
            Rank2.SetActive(false);
            Rank3.SetActive(false);

            if (userInfo.rank == 1)
                Rank1.SetActive(true);

            else if (userInfo.rank == 2)
                Rank2.SetActive(true);

            else if (userInfo.rank == 3)
                Rank3.SetActive(true);
        }

        public void SetInfo(string userId, int rank, double score)
        {
            rankText.text = rank == 0 ? NONE_RANK_STRING : rank.ToString();
            Rank1.SetActive(false);
            Rank2.SetActive(false);
            Rank3.SetActive(false);

            if (rank == 1)
                Rank1.SetActive(true);

            else if (rank == 2)
                Rank2.SetActive(true);


            else if (rank == 3)
                Rank3.SetActive(true);


            usernicknameText.text = userId;
            float amountTime = (float)score;
            int minutes = Mathf.FloorToInt(amountTime / 60f);
            float seconds = Mathf.Floor(amountTime % 60);
            float millisecond = (float)Math.Floor(amountTime * 100 % 100);
            scoreText.text = score == 99999 ? NONE_RANK_STRING : string.Format("{0:0}:{1:00}.{2:00}", minutes, seconds, millisecond);

            if (MyRankMark != null)
            {
                MyRankMark.gameObject.SetActive(Gamebase.GetUserID().Equals(userId));
            }

        }


    }
}
