using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class CardView : MonoBehaviour {
    private Dictionary<string, GameObject> menuHash;
    private string selectedCatagory;

    public GameObject containerMenu;

    void Awake() {
       menuHash = new Dictionary<string, GameObject>();
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitCardInfo()
    {
        // 获取Catagory的名称列表
    }

    public void Show(string selectedCatagory)
    {
        // 构造各个catagory的CatagoryInfo对象

        // 确定显示的catagory
    }

    public void BeforeExit()
    {

    }
}
