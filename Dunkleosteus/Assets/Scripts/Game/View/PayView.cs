using UnityEngine;
using System.Collections;
using GlobalDefines;

public class PayView : MonoBehaviour {
    public UILabel labelCoin;
    public GameDirector gameDirector;
    public UILabel labelPrice10;
    public UILabel labelPrice40;
    public UILabel lablePrice160;
    public UILabel labelPrice360;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RefreshView()
    {
        labelCoin.text = gameDirector.GetCoin().ToString();
        labelPrice10.text = gameDirector.GetLocalPrice(DefinePurchaseId.PurchaseId10);
        labelPrice40.text = gameDirector.GetLocalPrice(DefinePurchaseId.PurchaseId40);
        lablePrice160.text = gameDirector.GetLocalPrice(DefinePurchaseId.PurchaseId160);
        labelPrice360.text = gameDirector.GetLocalPrice(DefinePurchaseId.PurchaseId360);
    }

    public void RefreshCoin()
    {
        labelCoin.text = gameDirector.GetCoin().ToString();
    }
}
