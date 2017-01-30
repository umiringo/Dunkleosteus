using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GlobalDefines;

public class ProductInfo {
    string Id;
    string price;
    string description;
};

public class IAPMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init() {
        OCBridge.InitIAP();
        this.GetProductInfo(DefinePurchaseId.PurchaseId10 + "\t" + DefinePurchaseId.PurchaseId40 + "\t" +
                            DefinePurchaseId.PurchaseId160 + "\t" + DefinePurchaseId.PurchaseIdSale12);
    }

    public void IsAvailable()
    {
        OCBridge.IsIAPAvailable();
    }

    public void GetProductInfo(string productId)
    {
        OCBridge.RequestProductInfoById(productId);
    }

    public void Purchase(string productId)
    {
        OCBridge.PurchaseProduct(productId);
    }

}
