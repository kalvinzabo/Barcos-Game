using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMainController : MonoBehaviour
{
    TradeController tradeC;
    BaseGoodsHandler goodsC;
    public GameObject uiObject;
    void Start()
    {
        tradeC = GetComponent<TradeController>();
        goodsC = GetComponent<BaseGoodsHandler>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //como hay un layer, esto solo deberia entrar con el player
        uiObject.SetActive(true);     
    }

    private void OnTriggerExit(Collider other) { uiObject.SetActive(false); }
}
