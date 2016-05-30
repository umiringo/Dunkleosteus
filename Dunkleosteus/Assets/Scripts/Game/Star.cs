using UnityEngine;
using System.Collections;
using GlobalDefines;

public class Star : MonoBehaviour {

    public GameObject starChild;
    public GameObject shineChild;
    public int index;
    public StarState state;
	// Use this for initialization
	void Start () {
        state = StarState.NORMAL;
        RefreshStar();
        starChild.GetComponent<UISprite>().MakePixelPerfect();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateStatus(GlobalDefines.StarState s)
    {
        state = s;
        RefreshStar();
    }

    public void RefreshStar()
    {
        switch (state)
        {
            case StarState.NORMAL:
                //显示蓝色
                starChild.GetComponent<UISprite>().spriteName = "circle_40_40_50cce5";
                starChild.GetComponent<UISprite>().MakePixelPerfect();

                shineChild.GetComponent<UISprite>().spriteName = "circle_40_40_50cce5";
                shineChild.GetComponent<UISprite>().MakePixelPerfect();
                break;
            case StarState.CHOSEN:
                //显示绿色，大一号
                starChild.GetComponent<UISprite>().spriteName = "circle_80_80_28ed7b";
                starChild.GetComponent<UISprite>().MakePixelPerfect();

                break;
            case StarState.CONNECTED:
                //显示黄色
                starChild.GetComponent<UISprite>().spriteName = "circle_40_40_f8b711";
                shineChild.GetComponent<UISprite>().spriteName = "circle_40_40_f8b711";
                break;
            default:
                break;
        }
    }

}
