using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ó�����Ʒ�ĸ���
/// </summary>
public interface IKitchenObjectParent
{
    //���ó�����Ʒ
    public void SetKitchenObject(KitchenObject kitchenObject);
    //��ȡ������Ʒ
    public KitchenObject GetKitchenObject();
    //���������Ʒ
    public void ClearKitchenObject();
    //�Ƿ��г�����Ʒ
    public bool HasKitchenObject();
    //��ȡ�������ڵĸ���  ����Ʒ�����ڴ���Ʒ��
    public Transform GetKitchenObjectFollowTransform();
}
