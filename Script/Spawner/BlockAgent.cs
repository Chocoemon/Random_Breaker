using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ingame
{
    // 해당 클래스의 목적: 무작위 숫자를 만들어 버튼을 만들어냄 
    public class BlockAgent
    {
        private const int BLOCK_POOL_COUNT = 35;
        private Quaternion QI = Quaternion.identity;
        private readonly GameObject groupObject;
        private readonly ObjectPool<Block> BlockPool;

        public BlockAgent(Transform parent)
        {
            groupObject = new GameObject("Block_Pool");
            groupObject.transform.parent = parent;
            groupObject.transform.localScale = Vector3.one;
            groupObject.transform.localPosition = Vector3.zero;
            BlockPool = new ObjectPool<Block>();
            if (PlayerPrefs.GetInt("Block", 0) == 0)
            {
                BlockPool.Allocate(Resources.Load<Block>("Prefabs/Block"), groupObject.transform, BLOCK_POOL_COUNT); 
            }

            else if (PlayerPrefs.GetInt("Block", 0) == 1) // 삼각형
            {
                BlockPool.Allocate(Resources.Load<Block>("Prefabs/Triangle"), groupObject.transform, BLOCK_POOL_COUNT); 
            }

            else if (PlayerPrefs.GetInt("Block", 0) == 2) // 별
            {
                BlockPool.Allocate(Resources.Load<Block>("Prefabs/Star"), groupObject.transform, BLOCK_POOL_COUNT); 
            }

            else if (PlayerPrefs.GetInt("Block", 0) == 3) // 마름모
            {
                BlockPool.Allocate(Resources.Load<Block>("Prefabs/Rhombus"), groupObject.transform, BLOCK_POOL_COUNT); 
            }
        }


        public void Spawn(Vector3 pos)
        {
            BlockPool.Spawn(pos, QI); // 지정 위치에서 블록을 로드함 
        }

        public void Despawn(Block block)
        {
            BlockPool.Despawn(block); // 특정 블럭을 디스폰함 
        }

        public void DespawnAll()
        {
            for (int i = 0; i < groupObject.transform.childCount; i++)
                Despawn(groupObject.transform.GetChild(i).GetComponent<Block>());

        }
    }
}