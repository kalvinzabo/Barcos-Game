using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    public enum TypesOfGoods
    {
        Wood,
        Iron,
        Corn,
        Cotton,
        Clay
    }

    public TypesOfGoods type {get; private set;}
    
    [SerializeField]
    int amount = 1;
    IslandController myIsland;
    Renderer render;

    private void Start()
    {
        if(!CompareTag("Goods"))
        //parece que esto es necesario porque el enterarea de las islas tiene un goods, pero no termino de entender por que.
        {   return; }

        myIsland = transform.parent.gameObject.GetComponent<IslandController>();
        render = GetComponent<Renderer>();
        SetType();
    }
    public void SetGoods(string typeOfGood, int amt)
    {
        type = ToType(typeOfGood);
        amount = amt;
    }

    public void SetGoods(int typeId, int amt)
    {
        type = ToType(typeId);
        amount = amt;
    }
    public static TypesOfGoods ToType(int id)
    {   return (TypesOfGoods)id;}

    public static TypesOfGoods ToType(string typeName)
    {   return (TypesOfGoods)System.Enum.Parse(typeof(TypesOfGoods), typeName);}
    void SetType()
    {
        type = myIsland.GetIslandGoodsType();
        render.sharedMaterial = myIsland.GetIslandGoodsMaterial();
    }
    public TypesOfGoods GetTypeOfGood()
    {   return type;}
    public string GetTypeOfGoodString()
    {   return type.ToString();}
    public int GetAmount()
    {   return amount;}

    public void SubstractGoods(int amt)
    {
        amount -= amt;
    }  
}
