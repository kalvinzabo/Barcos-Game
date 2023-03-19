using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TradeController : MonoBehaviour
{
    //esta clase estará suscrita a todos los eventos con su funcion updatevalues para mostrar siempre lo que quiere cada isla
    // public VisualTreeAsset marketUI;
    VisualElement root;
    Label woodLabel, ironLabel, cornLabel, cottonLabel, clayLabel;
    Goods[] goodsWantedArray = new Goods[5];
    IslandController[] islandControllerArray = new IslandController[5];
    private void Awake()
    {
        Transform islands = GameObject.Find("Islands").transform;
        for(int i = 0; i < islands.childCount; i++)
        {
            islandControllerArray[i] = islands.GetChild(i).GetChild(0).GetComponent<IslandController>();
        }        
    }
    private void Start() {
    }
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;    //POR ALGUN MOTIVO ES IMPRESCINDIBLE QUE TODO ESTO ESTÉ EN ONENABLE, NI START NI AWAKE
        woodLabel = root.Q<Label>("Wood");
        ironLabel = root.Q<Label>("Iron");
        cornLabel = root.Q<Label>("Corn");
        cottonLabel = root.Q<Label>("Cotton");
        clayLabel = root.Q<Label>("Clay");        

        GetWantedGoods();
        UpdateValues();
    }

    void UpdateValues()
    {
        woodLabel.text = "Egurra quiere: " + goodsWantedArray[0].GetAmount() + " of " + goodsWantedArray[0].GetTypeOfGoodString();
        ironLabel.text = "El Hierro quiere: " + goodsWantedArray[1].GetAmount() + " of " + goodsWantedArray[1].GetTypeOfGoodString();
        cornLabel.text = "Millo quiere: " + goodsWantedArray[2].GetAmount() + " of " + goodsWantedArray[2].GetTypeOfGoodString();
        cottonLabel.text = "Coto quiere: " + goodsWantedArray[3].GetAmount() + " of " + goodsWantedArray[3].GetTypeOfGoodString();
        clayLabel.text = "Barru quiere: " + goodsWantedArray[4].GetAmount() + " of " + goodsWantedArray[4].GetTypeOfGoodString();

    }

    void GetWantedGoods()
    {
        for (int i = 0; i < islandControllerArray.Length; i++)
        {
            goodsWantedArray[i] = islandControllerArray[i].GetGoodsWanted();
        }
    }
}
