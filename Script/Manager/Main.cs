using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Toast.Gamebase;
using LitJson;

namespace Ingame
{

    public class Main : MonoBehaviour
    {
        public static Ingame_Agent Agent;
        public Text uiScore;
        public Text uiTimer;
        private float BestTime;
        public GameObject uiResult, uiLeaderboard, ui_Setup, ui_shop, ui_Main, ui_ingame, ui_Setup_Replay, ui_Setup_Home;
        public GameObject start_Btn, shop_Btn, Leaderboard_Btn, Setup_Btn, SoundBtn, MuteBtn, Pause_exit_Btn, Setup_exit_Btn;
        public GameObject Timer, Timer_Image, Countdown, NoAds_Obj;
        public GameObject Banner;
        public Text uiResultScore, uiRank;
        [SerializeField]
        private GameObject Countdown_3;
        [SerializeField]
        private GameObject Countdown_2;
        [SerializeField]
        private GameObject Countdown_1;
        [SerializeField]
        private GameObject START;
        public bool IsMute, paused, CountdownStart;
        private bool Noads;
        public Slider Volume_Slider;
        public AudioSource Sound;
        public AudioSource Background_bgm;
        public AudioSource Whistle;
        public AudioSource Click;
        IEnumerator routine;

        private void Awake()
        {
            Agent = new Ingame_Agent(this);
            paused = false;
            if (PlayerPrefs.GetInt("noads", 0) == 1)
                Noads = true;

        }

        private void Start()
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1);
            if (AudioListener.volume == 0)
            {
                MuteBtn.SetActive(true);
                SoundBtn.SetActive(false);
            }
            Volume_Slider.value = AudioListener.volume;
            LeaderboardApi.Get_Single_Score(0,(userdata) =>
            {
                if (userdata == null)
                    BestTime = 9999f;
                else
                    BestTime = (float)userdata.score;

            });

            if (Noads)
                NoAds_Obj.SetActive(false);

        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
            Agent = null;
        }

        private void Update()
        {
            Agent.Update();
            uiTimer.text = Agent.PlayingTimerString;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }



        public void PrintResult() // 결과창 띄워주기 
        {
            uiResult.SetActive(true);
            uiResultScore.text = Agent.PlayingTimerString;
            if(BestTime >= Agent.playingTime)
            {
                uiRank.gameObject.SetActive(true);
                BestTime = Agent.playingTime;
            }

            else
            {
                uiRank.gameObject.SetActive(false);
            }
        }


        public void set_text(int score)
        {
            uiScore.text = score.ToString();

        }

        #region UIButton.onClick

        public void Start_Btn_Push()
        {
            
            uiResult.SetActive(false);
            ui_Main.SetActive(false);
            Time.timeScale = 1f;
            CountdownStart = true;
            routine = Count_Down();
            StartCoroutine(routine);
            uiTimer.text = "0:00.00";
            uiScore.text = "1";
            // 메인 버튼들 다 없애기 
            // Ingame Object start, 카운트 다운 하기 
        }

        public void Replay_Btn()
        {
            CountdownStart = false;
            uiScore.text = 1.ToString();
            uiTimer.text = string.Format("{0:00}:{1:00.00}", 0, 0);
            Time.timeScale = 0f;
            Agent.Home_Btn();
            if (ui_Setup.activeSelf)
            {
                paused = true;
                StopCoroutine(routine);
                ui_Setup.SetActive(false);
                paused = false;
                routine = Count_Down();
                StartCoroutine(routine);
            }

            else
            {
                uiResult.SetActive(false);
                paused = false;
                StartCoroutine(Count_Down());
            }

            
            
        }

        public void Home_Btn()
        {
            CountdownStart = false;
            paused = false;
            Agent.Home_Btn();
            uiResult.SetActive(false);
            ui_ingame.SetActive(false);
            ui_Main.SetActive(true);
            ui_Setup.SetActive(false);
        }

        public void Leaderboard_Btn_Click()
        {
            
            uiLeaderboard.SetActive(true);
            

        }

        public void Setup_Btn_Click()
        {
            ui_Setup.gameObject.SetActive(true);
            ui_Setup_Replay.SetActive(false);
            ui_Setup_Home.SetActive(false);
            Pause_exit_Btn.SetActive(false);
            Setup_exit_Btn.SetActive(true);
            if (AudioListener.volume != 0)
            {
                SoundBtn.SetActive(true);
                MuteBtn.SetActive(false);
            }

            else
            {
                SoundBtn.SetActive(true);
                MuteBtn.SetActive(false);
            }
        }

        public void Setup_exit()
        {
            ui_Setup.gameObject.SetActive(false);

        }

        public void Pause_Btn_Click()
        {
            Agent.Pause();
            paused = true;
            ui_Setup.gameObject.SetActive(true);
            ui_Setup_Replay.SetActive(true);
            ui_Setup_Home.SetActive(true);
            Pause_exit_Btn.SetActive(true);
            Setup_exit_Btn.SetActive(false);

            if (AudioListener.volume != 0)
            {
                SoundBtn.SetActive(true);
                MuteBtn.SetActive(false);
            }

            else
            {
                SoundBtn.SetActive(true);
                MuteBtn.SetActive(false);

            }
        }

        public void Pause_exit()
        {
            Agent.Continue();
            paused = false;
            ui_Setup.gameObject.SetActive(false);

        }

        public void Mute_Click()
        {
            if (!IsMute)
            {
                AudioListener.volume = 0;
                Volume_Slider.value = 0;
                IsMute = true;
                SoundBtn.gameObject.SetActive(false);
                MuteBtn.gameObject.SetActive(true);

            }
            else
            {
                AudioListener.volume = 1;
                Volume_Slider.value = 1;
                IsMute = false;
                SoundBtn.gameObject.SetActive(true);
                MuteBtn.gameObject.SetActive(false);
            }

        }

        public void OnClick_shop()
        {
            ui_Main.SetActive(false);
            ui_shop.SetActive(true);
        }

        public void OnClick_shop_exit()
        {
            ui_Main.SetActive(true);
            ui_shop.SetActive(false);
        }

        public void OnClick_VolumeSlider()
        {
            AudioListener.volume = Volume_Slider.value;
            PlayerPrefs.SetFloat("Volume", AudioListener.volume);
            if (Volume_Slider.value == 0)
            {
                MuteBtn.gameObject.SetActive(true);
                SoundBtn.gameObject.SetActive(false);
            }


            else
            {
                MuteBtn.gameObject.SetActive(false);
                SoundBtn.gameObject.SetActive(true);
            }
        }

        public void OnClick_NoADS()
        {

            Shop.Purchase_Item.RequestPurchase_Noads(("ad_block"), (callback) =>
            {
                if (callback)
                {
                    NoAds_Obj.SetActive(false);
                    PlayerPrefs.SetInt("noads", 1);
                    Destroy(Banner);
                }
            });

        }

        #endregion

        #region CountDown
        IEnumerator Count_Down()
        {
            float F_TIME = 3.5f;
            float TIME = 0f;

            CountdownStart = true;
            ui_ingame.SetActive(true);
            Countdown.SetActive(true);
            Countdown_3.SetActive(false);
            Countdown_2.SetActive(false);
            Countdown_1.SetActive(false);
            
            while (F_TIME >= TIME)
            {
                if (!CountdownStart)
                {
                    Countdown.SetActive(false);
                    yield break;
                }

                if (!paused && CountdownStart)
                {
                    TIME += Time.unscaledDeltaTime;
                    if (TIME <= 1f)
                    {
                        Countdown_3.SetActive(true);

                    }

                    else if (TIME <= 2f && TIME > 1f)
                    {
                        Countdown_3.SetActive(false);
                        Countdown_2.SetActive(true);
                    }

                    else if (TIME <= 3f && TIME > 2f)
                    {
                        Countdown_2.SetActive(false);
                        Countdown_1.SetActive(true);
                    }
                }


                yield return null;
            }

            if (CountdownStart)
            {
                Countdown.SetActive(false);
                Whistle.Play();
                CountdownStart = false;
                Agent.Start();
            }
            yield return null;

        }

        #endregion


    }



}


