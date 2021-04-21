using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Shop
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject Triangle, Rect, Rhombous, Star, Equipped, PriceObj, LeftObj, RightObj;
        [SerializeField]
        private Button BuyBtn, EquipBtn;
        private int Item_num = 0;
        private int IsStar= 0, IsTriangle = 0, IsRhombus = 0;

        private void Start()
        {
            int Which_Equipped = PlayerPrefs.GetInt("Block", 0);
            if (Which_Equipped == 0)
            {
                Equipped.SetActive(true);
                EquipBtn.gameObject.SetActive(false);
                BuyBtn.gameObject.SetActive(false);
                PriceObj.gameObject.SetActive(false);
            }

            else
            {
                Equipped.SetActive(false);
                EquipBtn.gameObject.SetActive(true);
                BuyBtn.gameObject.SetActive(false);
                PriceObj.gameObject.SetActive(false);
            }
            LeftObj.gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("star", 0) == 1)
                IsStar = 1;

            if (PlayerPrefs.GetInt("triangle", 0) == 1)
                IsTriangle = 1;

            if (PlayerPrefs.GetInt("rhombus", 0) == 1)
                IsRhombus = 1;


        }

        public void Onclick_Purchase()
        {
            switch (Item_num) {
                case 1:
                    Purchase_Item.RequestPurchase(("Heart_Block"), (callback)=> {
                        if (callback)
                        {
                            Equipped_func();
                            PlayerPrefs.SetInt("triangle", 1);
                            PlayerPrefs.SetInt("Block", 1);
                            IsTriangle = 1;
                        }
                    
                    });
                    break;

                case 2:
                    Purchase_Item.RequestPurchase(("star_block"), (callback)=>
                    {
                        if (callback)
                        {
                            Equipped_func();
                            PlayerPrefs.SetInt("star", 1);
                            PlayerPrefs.SetInt("Block", 2);
                            IsStar = 2;
                        }
                    });
                    break;

                case 3:
                    Purchase_Item.RequestPurchase(("Rhombus_block"),(callback) =>
                    {
                        if (callback)
                        {
                            Equipped_func();
                            PlayerPrefs.SetInt("rhombus", 1);
                            PlayerPrefs.SetInt("Block", 3);
                            IsRhombus = 3;
                        }
                    });
                    break;
            }
        }


        public void Left_Arrow() // 화살표 넘길때 가지고 있는지 유무 판단해서 버튼도 바꾸어 주어야함 
        {
            int Equipped_num = PlayerPrefs.GetInt("Block", 0);
            if (Rect.activeSelf) { 
                return;
            }

            if (Triangle.activeSelf)
            {
                Triangle.SetActive(false);
                Rect.SetActive(true);
                LeftObj.gameObject.SetActive(false);
                Item_num = 0;
                if (Item_num == Equipped_num)
                {
                    Equipped_func();
                }

                else
                    Is_Purchased();

                return;
            }

            if (Star.activeSelf)
            {
                Star.SetActive(false);
                Triangle.SetActive(true);
                Item_num = 1;
                if (Item_num == Equipped_num)
                {
                    Equipped_func();
                }

                else
                {
                    if (IsTriangle == 1)
                        Is_Purchased();
                    else
                        Is_not_Purchased();
                }
                      
                return;
            }

            if (Rhombous.activeSelf)
            {
                RightObj.SetActive(true);
                Rhombous.SetActive(false);
                Star.SetActive(true);
                Item_num = 2;
                if (Item_num == Equipped_num)
                {
                    Equipped_func();
                }

                else
                {
                    if(IsStar == 1)
                        Is_Purchased();
                    else
                        Is_not_Purchased();
                }
                return;
            }
        }


        public void Right_Arrow()
        {
            int Equipped_num = PlayerPrefs.GetInt("Block", 0);
            if (Rect.activeSelf)
            {
                LeftObj.gameObject.SetActive(true);
                Triangle.SetActive(true);
                Rect.SetActive(false);
                Item_num = 1;
                if (Item_num == Equipped_num)
                {
                    Equipped_func();
                }

                else
                {
                    if(IsTriangle == 1)
                        Is_Purchased();
                    else
                        Is_not_Purchased();
                }
                return;
            }

            if (Triangle.activeSelf)
            {
                Triangle.SetActive(false);
                Star.SetActive(true);
                Item_num = 2;
                if (Item_num == Equipped_num)
                {
                    Equipped_func();
                }

                else
                {
                    if (IsStar == 1)
                        Is_Purchased();
                    else
                        Is_not_Purchased();
                }
                return;
            }

            if (Star.activeSelf)
            {
                Star.SetActive(false);
                Rhombous.SetActive(true);
                RightObj.SetActive(false);
                Item_num = 3;
                if (Item_num == Equipped_num)
                {
                    Equipped_func();
                }

                else
                {
                    if (IsRhombus == 1)
                        Is_Purchased();

                    else
                        Is_not_Purchased();
                }
                        return;
            }

            if (Rhombous.activeSelf)
            {
                return;
            }
        }


        public void Onclick_Equip()
        {
            PlayerPrefs.SetInt("Block", Item_num);
            Equipped_func();

        }

        private void Equipped_func()
        {
            EquipBtn.gameObject.SetActive(false);
            Equipped.SetActive(true);
            BuyBtn.gameObject.SetActive(false);
            PriceObj.SetActive(false);

        }

        private void Is_Purchased()
        {
            EquipBtn.gameObject.SetActive(true);
            Equipped.SetActive(false);
            BuyBtn.gameObject.SetActive(false);
            PriceObj.SetActive(false);
        }

        private void Is_not_Purchased()
        {
            EquipBtn.gameObject.SetActive(false);
            Equipped.SetActive(false);
            BuyBtn.gameObject.SetActive(true);
            PriceObj.SetActive(true);
        }
    }
}
