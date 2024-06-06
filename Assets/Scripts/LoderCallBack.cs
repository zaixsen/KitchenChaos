using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoderCallBack : MonoBehaviour
{
    private bool isFirsUpdate = true;
    private void Update()
    {
        if (isFirsUpdate)
        {
            isFirsUpdate = false;
            Loader.LoaderCallBack();
        }
    }
}
