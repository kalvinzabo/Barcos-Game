using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsHandler : MonoBehaviour
{
    Transform currentGoodsT;
    public Transform shipHold;
    IslandController currentIsland;
    public GameObject corsairBase;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 3)        //"Enter Area" Layer. Por ahora no se pueden usar triggers para nada más desde el player, pero otros agentes sí podrían hacer cosas al verle entrar en sus propios triggers.
        {   return;}

        if(other.CompareTag("Base"))
        {}

        if(currentIsland != null && other.CompareTag("Island"))
        {   return;}

        currentIsland = other.GetComponent<IslandController>();
        currentIsland.AskGoodsTrade();
        
        if(currentGoodsT == null)   //esto es matizable, tienen que darse mas cosas.
        {
            currentGoodsT = currentIsland.GiveGoods().transform; 
            SetCurrentGoodsTransform();
            return;
        }
        
        Debug.Log("already carrying goods");
        currentIsland.TakeGoods(currentGoodsT.gameObject);             
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer != 3)
        {   return;}

        if(currentIsland != null && other.CompareTag("Island"))
        {   currentIsland = null;}          
    }

    void SetCurrentGoodsTransform()      //adjust transform properties to avoid deformation if you pick it up while leaning.
    {
        currentGoodsT.SetParent(shipHold, true);
        currentGoodsT.position = shipHold.position;
        currentGoodsT.localRotation = Quaternion.Euler(Vector3.back*90);
        currentGoodsT.localScale = new Vector3(0.25f, 1.2f, 0.25f);
    }
}
