using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设置厨房物品的父级
/// </summary>
public interface IKitchenObjectParent
{
    //设置厨房物品
    public void SetKitchenObject(KitchenObject kitchenObject);
    //获取厨房物品
    public KitchenObject GetKitchenObject();
    //清除厨房物品
    public void ClearKitchenObject();
    //是否含有厨房物品
    public bool HasKitchenObject();
    //获取厨房现在的父级  将物品设置在此物品上
    public Transform GetKitchenObjectFollowTransform();
}
