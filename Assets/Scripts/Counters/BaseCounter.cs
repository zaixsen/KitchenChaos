using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    public virtual void Interact(Player player)
    {
        //Debug.LogError("BaseCounter.Interact()");
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.InteractAlternate()");
    }


    //设置厨房物品
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    //获取厨房物品
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    //清除厨房物品
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    //是否含有厨房物品
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

}
