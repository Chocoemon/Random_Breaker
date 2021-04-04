using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/*
 * 해당 클래스의 목적: 서버와의 직접적인 HTTP 통신을 담당함 
 */
namespace Ingame
{
    public class WebRequestObject : MonoBehaviour
    {
        private static WebRequestObject instance = null;

        public static WebRequestObject Instance // 인스턴스 초기화 
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject("WebRequest");
                    instance = obj.AddComponent<WebRequestObject>();

                    DontDestroyOnLoad(obj); // 나중에 필요 없으면 지우기 .
                }

                return instance;
            }
        }

        private const string HEADER_CONTENT_TYPE = "Content-Type";
        private const string HEADER_CONTENT_VALUE_JSON = "application/json;charset=UTF-8";
        private const string HEADER_X_SECRET_KEY = "X-Secret-Key";
        private const string HEADER_X_SECRET_VALUE_JSON = "7sEEQIZu";

        public void Get(string url, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            StartCoroutine(SendRequest(request, callback));
        }

        public void Request(UnityWebRequest request, Action<string> callback)
        {
            StartCoroutine(SendRequest(request, callback));
        }

        private IEnumerator SendRequest(UnityWebRequest request, Action<string> callback)
        {
            request.SetRequestHeader(HEADER_CONTENT_TYPE, HEADER_CONTENT_VALUE_JSON);
            request.SetRequestHeader(HEADER_X_SECRET_KEY, HEADER_X_SECRET_VALUE_JSON);

            yield return UnityCompatibility.UnityWebRequest.Send(request);

            if (request.isDone == true)
            {
                if (request.responseCode != 200)
                {
                    callback(null);
                    yield break;
                }

                if (UnityCompatibility.UnityWebRequest.IsError(request) == true)
                {
                    callback(null);
                    yield break;
                }

                if (string.IsNullOrEmpty(request.downloadHandler.text) == true)
                {
                    callback(null);
                    yield break;
                }

                callback(request.downloadHandler.text);
            }

            request.Dispose();
        }
    }
}