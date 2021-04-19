using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private Text Score_Text;
        [SerializeField]
        private AudioSource Sound;
        [SerializeField]
        private SpriteRenderer this_outline;

        private int Spawn_num = 10;
        private int score;
        private bool flag = false;
        
        public Vector3 Pos;
        private void Update()
        {
            if (score == Main.Agent.GameScore - Spawn_num)
            {
                flag = true;
                this_outline.color = new Color(245f / 255f, 255f / 255f, 115f / 255f);
            }
        }

        private void OnEnable() // 킬 때 현재의 스코어 반영하기 
        {
            Score_Text.text = (Main.Agent.GameScore).ToString();
            score = Main.Agent.GameScore;
            Pos = this.transform.position;

        }

        public void Onclick_Block()
        {
            if (flag)
            {
                Main.Agent.Click_Sound();
                Main.Agent.Hit(this);
                flag = false;
                this_outline.color = new Color(139f / 255f, 230f / 255f, 188f / 255f);
            }

            else
            {
                Main.Agent.penalty();

            }
        }


    }
}
