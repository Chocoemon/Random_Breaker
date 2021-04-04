using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
    public class ObjectPool<T> where T : Component
    {
        private readonly Queue<T> objects = new Queue<T>();
        private Transform parent;
        private GameObject biTweenObject;
        public bool IsEmpty 
        {
            get { return objects.Count == 0; }
        }

        public void Allocate(T prefab, Transform transform, int amount) // 오브젝트 풀에서 미리 객체를 만들어냄 
        {
            parent = transform;

            for (int i = 0; i < amount; i++)
            {
                T createdObject = UnityEngine.Object.Instantiate(prefab) as T;
                createdObject.name = prefab.name;

                Despawn(createdObject);
            }
        }

        public T Spawn(Vector3 pos, Quaternion rot) // 풀에서 꺼내서 Spawn 시킴 
        {
            if (objects.Count == 0)
            {
                return null;
            }

            T spawn = objects.Dequeue();

            spawn.transform.position = pos;
            spawn.transform.rotation = rot;
            spawn.transform.localScale = Vector3.one;
            spawn.gameObject.SetActive(true);
            biTweenObject = spawn.gameObject;
            return spawn;
        }
    
        public void Despawn(T obj) // 풀로 다시 집어넣음 
        {
            obj.gameObject.SetActive(false);
            if (parent != null)
            {
                obj.GetComponent<RectTransform>().SetParent(parent);
            }

            objects.Enqueue(obj);
        }



    }
}