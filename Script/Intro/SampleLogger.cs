using UnityEngine;
using UnityEngine.UI;

namespace Toast.Gamebase
{
    public class SampleLogger : MonoBehaviour
    {
        private const string LOG_PREFIX = "SampleLog";
        private static string logString;

        [SerializeField]
        private Text logTxt;

        private void Start()
        {
            logTxt = GetComponent<Text>();
        }

        private void Update()
        {
            logTxt.text = logString;
        }

        public static void Log(string log)
        {
            log = string.Format("[{0}] {1}", LOG_PREFIX, log);

            logString = log;
        }
    }
}
