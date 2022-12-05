using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsHandler : MonoBehaviour
{
    Transform currentGoods;
    public Transform shipHold;
    // private void Update()
    // {
    //     if(currentGoods == null)
    //     {   return;}

    //     currentGoods.position = shipHold.position;    
    // }
    private void OnTriggerEnter(Collider other)
    {
        if(currentGoods != null)
        {
            Debug.Log("already carrying goods");
            return;
        }

        if(other.gameObject.layer == 3)     //"Enter Area" layer
        {
            IslandController currentIsland = other.GetComponent<IslandController>();
            currentGoods = currentIsland.GiveGoods().transform;  
            SetCurrentGoods();          
        }        
    }

    void SetCurrentGoods()      //adjust transform properties to avoid deformation if you pick it up while leaning.
    {
        currentGoods.SetParent(shipHold, true);
        currentGoods.position = shipHold.position;
        currentGoods.localRotation = Quaternion.Euler(Vector3.back*90);
        currentGoods.localScale = new Vector3(0.25f, 1.2f, 0.25f);
    }
}
