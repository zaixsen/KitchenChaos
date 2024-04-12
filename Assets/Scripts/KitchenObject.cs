using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 厨房物品
/// </summary>
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObject;

    //持有当前父级接口 用来设置父级
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObject;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //之前有父级接口 清除
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        //设置当前父级接口
        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            //输出报错  没设置上
        }

        //设置厨房物品
        kitchenObjectParent.SetKitchenObject(this);
        //设置父级
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        //位置清零
        transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 返回当前父级接口
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

    //用来设置父级的静态函数 再生 静态函数属于类而不属于实例 脚本都是实例
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenTransform = Instantiate(kitchenObjectSO.prefabs);
        //获取类的实例
        //设置父级
        KitchenObject kitchenObject = kitchenTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return true;
        }
    }
}
