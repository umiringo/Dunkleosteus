using UnityEngine;
using System.Collections;
using GlobalDefines;

public class Star : MonoBehaviour {

    public GameObject starChild;
    public GameObject shineChild;
    public int index;
    private StarState _state;
    public StarState state
    {
        get {
            return _state;
        }
        set {
            _state = value;
        }
    }
    
    // inner property
    private UISprite starSprite;
    private UISprite shineSprite;
    private Shine shineLogic;

	// Use this for initialization
	void Start () 
    {
        starSprite = starChild.GetComponent<UISprite>();
        shineSprite = shineChild.GetComponent<UISprite>();
        shineLogic = shineChild.GetComponent<Shine>();

        this.SetNormal();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SetNormal() 
    {
        state = StarState.Normal;
        //change sprite to blue
        starSprite.spriteName = "circle_50_50_50cce5";
        starSprite.MakePixelPerfect();
        //start to shine
        shineSprite.spriteName = "circle_50_50_50cce5";
        shineSprite.MakePixelPerfect();
        shineLogic.StartShine();
    }

    public void SetChosen() 
    {
        state = GlobalDefines.StarState.Chosen;
        //change sprite to green
        starSprite.spriteName = "circle_60_60_28ed7b";
        starSprite.MakePixelPerfect();
        //stop shine
        shineLogic.StopShine();
    }

    public void SetLinked()
    {
        state = GlobalDefines.StarState.Linked;
        //change sprite to yellow
        starSprite.spriteName = "circle_50_50_f8b711";
        starSprite.MakePixelPerfect();
        //start to shine
        shineSprite.spriteName = "circle_50_50_f8b711";
        shineSprite.MakePixelPerfect();
        shineLogic.StartShine();
    }
}
