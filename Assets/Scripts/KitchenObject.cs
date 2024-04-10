using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������Ʒ
/// </summary>
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObject;

    //���е�ǰ�����ӿ� �������ø���
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObject;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //֮ǰ�и����ӿ� ���
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        //���õ�ǰ�����ӿ�
        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            //�������  û������
        }

        //���ó�����Ʒ
        kitchenObjectParent.SetKitchenObject(this);
        //���ø���
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        //λ������
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// ���ص�ǰ�����ӿ�
    /// </summary>
    /// <returns></returns>
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    //�������ø����ľ�̬����
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent  kitchenObjectParent)
    {
        Transform kitchenTransform = Instantiate(kitchenObjectSO.prefabs);
        //���ø���
        KitchenObject kitchenObject = kitchenTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
