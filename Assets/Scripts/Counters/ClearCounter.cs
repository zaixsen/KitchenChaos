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
            //There is a KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is carring something
            }
            else
            {
                //Player not carring anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
