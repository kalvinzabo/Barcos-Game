using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    GameObject goods;
    private void Start()
    {
        goods = transform.GetChild(0).gameObject;   
    }
    public GameObject GiveGoods()
    {
        if(goods == null)
        {
            Debug.Log("no goods to deliver");
            return null;
        }

        return goods;
    }
}
