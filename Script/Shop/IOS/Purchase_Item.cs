using System;
using Toast.Gamebase;
using UnityEngine;

namespace Shop
{
    public static class Purchase_Item
    {
        public static void RequestNotConsumed()
        {
            // 미지급된 아이템 consume flow 진행하기 
            // 상점 진입시 반드시 실행.
            // 결제 됐는데, 폰 옮겨타서 설정 다른 경우 포함.
            //Gamebase.Purchase.RequestItemListOfNotConsumed

        }

        public static void RequestPurchase(string gamebaseProductId, Action<bool> callback)
        {
            Gamebase.Purchase.RequestPurchase(gamebaseProductId, (purchasableRecipt, error) =>
             {
                 if (Gamebase.IsSuccess(error))
                 {
                     SampleLog_Shop.Log("Purchase succeded");
                     if (gamebaseProductId == "triangle_block")
                         PlayerPrefs.SetInt("triangle", 1);

                     else if (gamebaseProductId == "rhombus_block")
                         PlayerPrefs.SetInt("rhombus", 1);

                     else if (gamebaseProductId == "circle_block")
                         PlayerPrefs.SetInt("circle", 1);

                     callback(true);
                 }

                 else
                 {
                     if (error.code == (int)GamebaseErrorCode.PURCHASE_USER_CANCELED)
                     {
                         SampleLog_Shop.Log("user canceled purchase");

                     }

                     else
                         SampleLog_Shop.Log(string.Format("Purchase failed. error is {0}", error));

                 }


             });
        }

        public static void RequestPurchase_Noads(string gamebaseProductId , Action<bool> callback)
        {
            Gamebase.Purchase.RequestPurchase(gamebaseProductId, (purchasableRecipt, error) =>
            {
                if (Gamebase.IsSuccess(error))
                {
                    SampleLog_Shop.Log("Purchase succeded");
                    callback(true);

                }

                else
                {
                    if (error.code == (int)GamebaseErrorCode.PURCHASE_USER_CANCELED)
                    {
                        SampleLog_Shop.Log("user canceled purchase");

                    }

                    else
                        SampleLog_Shop.Log(string.Format("Purchase failed. error is {0}", error));

                    callback(false);
                }


            });
        }
    }



}

