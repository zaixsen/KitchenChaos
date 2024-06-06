using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 主要清除侦听时间
/// </summary>
public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
