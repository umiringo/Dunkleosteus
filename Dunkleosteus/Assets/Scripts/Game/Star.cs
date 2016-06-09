using UnityEngine;
using System.Collections;
using GlobalDefines;

public class Star : MonoBehaviour {

    public GameObject starChild;
    public GameObject shineChild;
    public int index;
    public StarState state;
    
    // inner property
    private UISprite starSprite;
    private UISprite shineSprite;
    private Shine shineLogic;

	// Use this for initialization
	void Start () {
        starSprite = starChild.GetComponent<UISprite>();
        shineSprite = shineChild.GetComponent<UISprite>();
        shineLogic = shineChild.GetComponent<Shine>();

        this.EnterNormal();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateStatus(GlobalDefines.StarState s)
    {
        Debug.Log("Star.UpdateStatus, s = " + s.ToString());
        RefreshStar(s);
    }

    private void EnterNormal()
    {
        Debug.Log("Star.EnterNormal");
        state = GlobalDefines.StarState.Normal;
        //change sprite to blue
        starSprite.spriteName = "circle_50_50_50cce5";
        starSprite.MakePixelPerfect();
        //start to shine
        shineSprite.spriteName = "circle_50_50_50cce5";
        shineSprite.MakePixelPerfect();
        shineLogic.StartShine();
    }

    private void EnterChosen()
    {
        Debug.Log("Star.EnterChosen");
        state = GlobalDefines.StarState.Chosen;
        //change sprite to green
        starSprite.spriteName = "circle_60_60_28ed7b";
        starSprite.MakePixelPerfect();
        //stop shine
        shineLogic.StopShine();
    }

    private void EnterConnected()
    {
        Debug.Log("Star.EnterConnected");
        state = GlobalDefines.StarState.Connected;
        //change sprite to yellow
        starSprite.spriteName = "circle_50_50_f8b711";
        starSprite.MakePixelPerfect();
        //start to shine
        shineSprite.spriteName = "circle_50_50_f8b711";
        shineSprite.MakePixelPerfect();
        shineLogic.StartShine();
    }

    //TODO test function
    private void RefreshStar(GlobalDefines.StarState s)
    {
        switch (s)
        {
            case StarState.Normal:
                this.EnterNormal();
                break;
            case StarState.Chosen:
                //显示绿色，大一号
                this.EnterChosen();
                break;
            case StarState.Connected:
                this.EnterConnected();
                break;
            default:
                break;
        }
    }

}
