using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ingame
{
    public class Ingame_Agent : MonoBehaviour
    {
        private const int TARGET_SCORE = 100;
        private const int TARGET_SPAWN = 10;
        private readonly Main ingame;
        public float playingTime { get; private set; }
        private bool flag = false; // 깼음을 알려주는 Flag
        private BlockAgent block_spawner;
        private List<Vector3> SpawnList;
        private Vector3 SpawnPos;
        private Transform BlockPool;


        public int GameScore { get; private set;} // set은 private 하게만 가능  

        public string PlayingTimerString    
        {
            get
            {
                float amountTime = playingTime;
                int minutes = Mathf.FloorToInt(amountTime / 60f);
                float seconds = Mathf.Floor(amountTime % 60);
                float millisecond = (float)Math.Floor(playingTime*100%100);
                return string.Format("{0:0}:{1:00}.{2:00}", minutes, seconds, millisecond);

            }

        }
        public Ingame_Agent(Main ingame)
        {
            this.ingame = ingame;
        }

        public void Start()
        {
            Time.timeScale = 1f;
            GameScore = 1;
            playingTime = 0;
            ingame.ui_ingame.SetActive(true);
            if (block_spawner == null)
            {
                block_spawner = new BlockAgent(ingame.transform.GetChild(5));
            }

            else
            {
                block_spawner.DespawnAll();
                ingame.transform.GetChild(5).GetChild(8).gameObject.SetActive(true);
            }
            Start_Spawn();
            //AudioListener.volume = 1;
        }


        public void Update()
        {
            if (GameScore <= TARGET_SCORE+TARGET_SPAWN && flag )
            {
                playingTime += Time.deltaTime;
            }

            else if(GameScore > TARGET_SCORE+TARGET_SPAWN && flag)
                Finish();
        }

        public void Finish()
        {
            int Die_Count = 0;
            Die_Count = PlayerPrefs.GetInt("DieCount");
            Pause();
            flag = false;
            LeaderboardApi.AddExp(playingTime,1); // 전체 랭킹 등록
            LeaderboardApi.AddExp(playingTime,2); // 일간 랭킹 등록
            LeaderboardApi.AddExp(playingTime,3); // 월간 랭킹 등록 

            ingame.Whistle.Play();
            ingame.PrintResult();
            Destroy(ingame.transform.GetChild(5).GetChild(8).gameObject);
            block_spawner = null;

            if (PlayerPrefs.GetInt("noads", 0) == 0)
            {
                if (Die_Count >= 3)
                {
                    RewardAd.Show_RewardAd(callback =>
                    {
                        if (callback)
                            Die_Count = 0;

                        else
                            Die_Count = 3;

                    });
                }

                else
                    ++Die_Count;

                PlayerPrefs.SetInt("DieCount", Die_Count);
            }
        }


        public void Pause()
        {
            Time.timeScale = 0f;
        }

        public void Continue()
        {
            Time.timeScale = 1f;
        }


        public void Hit(Block target)
        {
            if (GameScore <= TARGET_SCORE)
            {
                int rand = UnityEngine.Random.Range(0, SpawnList.Count);
                block_spawner.Despawn(target);
                SpawnList.Add(target.Pos);
                block_spawner.Spawn(SpawnList[rand]);
                SpawnList.RemoveAt(rand);
                GameScore++;
                ingame.uiScore.text = (GameScore-10).ToString();
            }

            else
            {
                GameScore++;
                if (GameScore <= 110)
                {
                    ingame.uiScore.text = (GameScore - 10).ToString();
                }
                block_spawner.Despawn(target);
            }
            
        }

        public void Start_Spawn()
        {
            AudioListener.volume = 1;
            if (block_spawner == null)
            {
                block_spawner = new BlockAgent(ingame.transform.GetChild(5));
            }

            else
                block_spawner.DespawnAll();

            SpawnList = new List<Vector3>();
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    SpawnPos = new Vector3((float)(-640f + 214f * i), (float)(500f - (214 * j)), 0f);
                    SpawnList.Add(SpawnPos);
                }
            }


            for (int i = 0; i < TARGET_SPAWN; i++)
            {
                int rand = UnityEngine.Random.Range(0, SpawnList.Count);
                block_spawner.Spawn(SpawnList[rand]);
                SpawnList.RemoveAt(rand);
                GameScore++;
            }

            flag = true;
            ingame.paused = false;
        }

        public void Click_Sound()
        {

            ingame.Sound.Play();

        }

        public void Home_Btn()
        {
            flag = false;
            GameScore = 1;
            Pause();
            playingTime = 0;
            if (block_spawner != null)
            { 
                Destroy(ingame.transform.GetChild(5).GetChild(8).gameObject);
                block_spawner = null;
            }
        }


    }

}
