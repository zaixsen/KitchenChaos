using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;

    public override void Interact(Player player)
    {
        //桌子上没有物品
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carring something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carring anything

            }
        }
        else
        {
            //There is a KitchenObject here   玩家有物品
            if (player.HasKitchenObject())
            {
                //Player is carring something 是否拿的是盘子
                if (player.GetKitchenObject().TryGetComponent(out PlateKitchenObject plateKitchenObject))
                {
                    //桌子上的物品是否能添加到盘子里
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else  //拿的不是盘子
                {
                    //桌子上是否有盘子
                    if (GetKitchenObject().TryGetComponent(out plateKitchenObject))
                    {
                        //玩家身上的物品是否能放到盘子里
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //Player not carring anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
