using System.Collections.Generic;
using UnityEngine;

namespace Ingame
{
    public class LeaderBoardRankView : MonoBehaviour
    {
        private const int TOP_RANK_LIST_COUNT = 50;
        [SerializeField]
        private RectTransform ItemPrefab;
        [SerializeField]
        private GameObject EmptyView;
        [SerializeField]
        private Transform TR, Object_Pool;
        public List<LeaderBoardRankItem> items = new List<LeaderBoardRankItem>();
        private void Awake()
        {
            ShowEmptyView(false);
        }

        private void Start()
        {
            for (int i = 0; i < TOP_RANK_LIST_COUNT; i++)
            {
                var newObject = Instantiate(ItemPrefab, transform, true);
                newObject.transform.localPosition = Vector3.zero;
                newObject.transform.localScale = Vector3.one;
                newObject.transform.SetParent(Object_Pool);
                newObject.gameObject.SetActive(false);
            }
        }


        public void SetInfo(LeaderboardVo.UserInfosByRange userInfosByRange)
        {
            ShowEmptyView(false);
            if (userInfosByRange == null || userInfosByRange.userInfos == null)
            {
                ShowEmptyView(true);
                return;
            }

            var count = userInfosByRange.userInfos.Count;
            if (count == 0)
            {
                ShowEmptyView(true);
            }


            // Stack 형태로 구현 앞에서만 넣어주고 뺌 
            if (TR.childCount < count)
            {
                int dest = TR.childCount;
                for (int i = 0; i < count - dest; i++)
                {
                    var item = Object_Pool.GetChild(0).GetComponentInChildren<LeaderBoardRankItem>();
                    items.Add(item);
                    Spawn_Ranking(Object_Pool.GetChild(0));

                }
            }

            else
            {
                int dest = TR.childCount;
                if (TR.childCount > count)
                {
                    for (int i = 0; i < dest - count; i++)
                    {
                        var item = TR.GetChild(TR.childCount - 1).GetComponentInChildren<LeaderBoardRankItem>();
                        items.RemoveAt(items.Count-1);
                        Despawn_Ranking(TR.GetChild(TR.childCount - 1));
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                LeaderboardVo.UserInfo userInfo = userInfosByRange.userInfos[i];
                items[i].SetInfo(userInfo);
                Debug.Log("실행");
            }
        }


        private void ShowEmptyView(bool isShow)
        {
            if (EmptyView != null)
            {
                EmptyView.SetActive(isShow);
            }
        }

        private void Spawn_Ranking(Transform TR)
        {
            TR.SetParent(this.TR);
            TR.gameObject.SetActive(true);
        }

        private void Despawn_Ranking(Transform TR)
        {
            TR.SetParent(this.Object_Pool);
            TR.gameObject.SetActive(false);

        }
    }
}