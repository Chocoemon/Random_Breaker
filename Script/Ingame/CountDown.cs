using System.Collections;
using System;
using UnityEngine;

namespace Ingame {
    public class CountDown : MonoBehaviour
    {
        [SerializeField]
        private GameObject Countdown_3;
        [SerializeField]
        private GameObject Countdown_2;
        [SerializeField]
        private GameObject Countdown_1;
        [SerializeField]
        private GameObject START;
        float TIME = 0f;

        private void Start()
        {
            Countdown_3.SetActive(true);
        }

        void Update()
        {
            Debug.Log(TIME);
            TIME += Time.deltaTime;
            if (TIME >= 1f)
            {
                Countdown_3.SetActive(false);
                Countdown_2.SetActive(true);

            }
            else if (TIME >= 2f)
            {
                Countdown_2.SetActive(false);
                Countdown_1.SetActive(true);
            }
            else if (TIME >= 3f)
            {
                Countdown_1.SetActive(false);
                gameObject.SetActive(false);
                Main.Agent.Start_Spawn();
            }
        }
    }

}




            
            

        

    

