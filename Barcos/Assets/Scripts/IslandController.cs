using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    GameObject myGoods;
    
    [SerializeField]
    int[] currentGoods;

    Goods goodsWanted;

    Goods.TypesOfGoods _myType;
    Material _myGoodsMaterial;
    private void Awake()
    {
        currentGoods = new int[5];
        SetMyTypeAndMaterial();
        currentGoods[(int) _myType] = int.MaxValue;
    }
    private void Start()
    {
        myGoods = transform.GetChild(0).gameObject;
        goodsWanted = GetComponent<Goods>();            //esto no es dinamico, solo se va llamando a SetGoodsToAskFor()
        SetGoodsToAskFor();
    }
    public GameObject GiveGoods()
    {
        if(myGoods == null)
        {
            Debug.Log("no goods to deliver");
            return null;
        }

        return myGoods;
    }

    public void OfferGoodsTrade()
    {}

    public void TakeGoods(GameObject goods)
    {
        Goods goodsC = goods.GetComponent<Goods>();
        if(goodsC.GetTypeOfGoodString() != goodsWanted.GetTypeOfGoodString())
        {
            Debug.Log("I don't want that");
            return;
        }

        if(goodsC.GetAmount() > goodsWanted.GetAmount())
        {
            Debug.Log(goodsWanted.GetAmount() + " is enough, thank you");
            goodsC.SubstractGoods(goodsWanted.GetAmount());
        }
        else
        {   GameObject.Destroy(goods);}
        
        AddNewGoods((int)goodsC.GetTypeOfGood(), goodsWanted.GetAmount());
    }

    void AddNewGoods(int type, int amount)
    {
        currentGoods[type] += amount;
    }

    public void AskGoodsTrade()
    {
        Debug.Log("I want " + goodsWanted.GetAmount() + " of " + goodsWanted.GetTypeOfGoodString());
    }

    public Goods GetGoodsWanted()
    {    return goodsWanted;}
    
    void SetGoodsToAskFor()
    {
        int wantedType = -1;
        do
        {
            wantedType = Random.Range(0,5);
        } while (Goods.ToType(wantedType) == _myType);
        
        int amount = Random.Range(1,4);
        goodsWanted.SetGoods(wantedType, amount);
    } 

    void SetMyTypeAndMaterial()
    {
        string islandName = transform.parent.name;
        switch(islandName)
        {
            case "Egurra":
                _myType = Goods.TypesOfGoods.Wood;
                _myGoodsMaterial = Resources.Load("WoodGoodsMaterial", typeof(Material)) as Material;
                break;
            case "El Hierro":
                _myType = Goods.TypesOfGoods.Iron;
                _myGoodsMaterial = Resources.Load("IronGoodsMaterial", typeof(Material)) as Material;
                break;
            case "Millo":
                _myType = Goods.TypesOfGoods.Corn;
                _myGoodsMaterial = Resources.Load("CornGoodsMaterial", typeof(Material)) as Material;
                break;
            case "Coto":
                _myType = Goods.TypesOfGoods.Cotton;
                _myGoodsMaterial = Resources.Load("CottonGoodsMaterial", typeof(Material)) as Material;
                break;
            case "Barru":
                _myType = Goods.TypesOfGoods.Clay;
                _myGoodsMaterial = Resources.Load("ClayGoodsMaterial", typeof(Material)) as Material;
                break;
            default:
                Debug.Log("invalid island name");
                break;
        }
    }

    public Goods.TypesOfGoods GetIslandGoodsType()
    {   return _myType;}
    public Material GetIslandGoodsMaterial()
    {   return _myGoodsMaterial;}



}
