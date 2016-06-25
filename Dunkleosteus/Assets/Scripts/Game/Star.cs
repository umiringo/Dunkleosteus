using UnityEngine;
using System.Collections;
using GlobalDefines;

public class Star : MonoBehaviour {

    public GameObject starChild;
    public GameObject shineChild;
    public int index;
    private StarState _state;

    public string blueStarPath;
    public string yellowStarPath;

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
        //change sprite to yellow
        starSprite.spriteName = yellowStarPath;
        starSprite.MakePixelPerfect();
        //start to shine
        shineSprite.spriteName = yellowStarPath;
        shineSprite.MakePixelPerfect();
        shineLogic.StartShine();
    }

    public void SetChosen() 
    {
        state = GlobalDefines.StarState.Chosen;
        //change sprite to blue
        starSprite.spriteName = blueStarPath;
        starSprite.MakePixelPerfect();
        //stop shine
        shineLogic.StopShine();
    }

    public void SetLinked()
    {
        state = GlobalDefines.StarState.Linked;
        //change sprite to yellow
        starSprite.spriteName = yellowStarPath;
        starSprite.MakePixelPerfect();
        //start to shine
        shineSprite.spriteName = yellowStarPath;
        shineSprite.MakePixelPerfect();
        shineLogic.StartShine();
    }
    
}
