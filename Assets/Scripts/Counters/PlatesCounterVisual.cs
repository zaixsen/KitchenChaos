using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform currentTopPoint;
    [SerializeField] private Transform plateVisualPrefab;


    private List<GameObject> plateVisualGameObjectList;
    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawed += PlatesCounter_OnPlateSpawed;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawed(object sender, System.EventArgs e)
    {
        Transform plateVisualTranform = Instantiate(plateVisualPrefab, currentTopPoint);

        float plateOffsetY = .1f;
        plateVisualTranform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTranform.gameObject);
    }
}
