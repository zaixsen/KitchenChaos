using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;

    public override void Interact(Player player)
    {
        //������û����Ʒ
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
            //There is a KitchenObject here   �������Ʒ
            if (player.HasKitchenObject())
            {
                //Player is carring something �Ƿ��õ�������
                if (player.GetKitchenObject().TryGetComponent(out PlateKitchenObject plateKitchenObject))
                {
                    //�����ϵ���Ʒ�Ƿ�����ӵ�������
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else  //�õĲ�������
                {
                    //�������Ƿ�������
                    if (GetKitchenObject().TryGetComponent(out plateKitchenObject))
                    {
                        //������ϵ���Ʒ�Ƿ��ܷŵ�������
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
