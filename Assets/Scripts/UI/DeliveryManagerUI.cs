using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeComPleted += DeliverManager_OnRecipeComPleted;
        DeliveryManager.Instance.OnRecipeSpawned += DeliverManager_OnRecipeSpawned;
        UpdateVisual();
    }

    private void DeliverManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliverManager_OnRecipeComPleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipce(recipeSO);
        }
    }
}
