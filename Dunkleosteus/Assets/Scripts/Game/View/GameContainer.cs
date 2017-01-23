using UnityEngine;
using System.Collections;

public class GameContainer : MonoBehaviour {

	// Use this for initialization
    public GameObject skyGameObject;
    public GameObject detailGameObject;
    public GameObject starContainer;

    private LevelPlayModel levelPlayModel;
    private GameObject triggerGameObject;
    private TweenRotation skyTween;
    private TweenRotation detailTween;
    private UIWidget textureWidget;

    private float duration = 0.8f;
    
    private bool detailTriggered;

    private UIPlayTween winTween;
	void Start () 
    {
        triggerGameObject = null;
        detailTriggered = false;
        
        detailGameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
        skyTween = skyGameObject.GetComponent<TweenRotation>();
        if (skyTween == null) {
            skyTween = skyGameObject.AddComponent<TweenRotation>();
        }
        detailTween = detailGameObject.GetComponent<TweenRotation>();
        if (detailTween == null) {
            detailTween = detailGameObject.AddComponent<TweenRotation>();
        }

        skyTween.enabled = false;
        detailTween.enabled = false;

        skyTween.from = Vector3.zero;
        skyTween.to = new Vector3(0, 90, 0);
        skyTween.duration = duration;

        detailTween.from = new Vector3(0, 90, 0);
        detailTween.to = Vector3.zero;
        detailTween.duration = duration;

        skyTween.onFinished.Add(new EventDelegate(SkyTweenEventDelegate));
        detailTween.onFinished.Add(new EventDelegate(DetailTweenEventDelegate));
        
        // Set winTween
        winTween = gameObject.GetComponent<UIPlayTween>();
        winTween.onFinished.Add(new EventDelegate(WinPlayTweenEventDelegate));
        
        // Unable star and detail box collider
        skyGameObject.GetComponent<BoxCollider>().enabled = false;
        detailGameObject.GetComponent<BoxCollider>().enabled = false;

        // Set levelPlay
        levelPlayModel = GameObject.Find("UI Root/PanelLevel").GetComponent<LevelPlayModel>();

        // Set texture widget
        textureWidget = skyGameObject.transform.FindChild("StarChart").gameObject.GetComponent<UIWidget>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void GameWin()
    {
        textureWidget.alpha = 0;
        // Unable star touch
        SwitchStarsBoxCollider(false);
        // Start scale
        winTween.Play(true);
    }
    
    public void SwitchStarsBoxCollider(bool enable)
    {
        foreach(Transform child in starContainer.transform) {
            child.gameObject.GetComponent<BoxCollider>().enabled = enable;
        }
    }

    public void ShowPreview()
    {
        if (textureWidget.alpha <= 0) {
            textureWidget.alpha = 1;
        }
        else {
            textureWidget.alpha = 0;
        }
    }
    ///////////////////////////////////////////////////////////////
    // delegate                                                  //
    ///////////////////////////////////////////////////////////////
    public void BeginToFlop(GameObject touchedGameObject)
    {
        triggerGameObject = touchedGameObject;
        if (touchedGameObject == skyGameObject) {
            skyTween.Play(true);
            if(!detailTriggered){
                // Show the menu
                levelPlayModel.FlopToCardBack();
                detailTriggered = true;
            }
        }
        else if (touchedGameObject == detailGameObject) {
            detailTween.Play(false);
        }
    }
    
    // StarTween.onFinished
    public void SkyTweenEventDelegate()
    {
        if(triggerGameObject == skyGameObject) {
            detailTween.Play(true);
        }
    }
    
    // DetailTween.onFinish
    public void DetailTweenEventDelegate()
    {
        if(triggerGameObject == detailGameObject) {
            skyTween.Play(false);
        }
    }
    
    // WinTween.onFinish
    public void WinPlayTweenEventDelegate()
    {
        // Enable star and detail box collider
        skyGameObject.GetComponent<BoxCollider>().enabled = true;
        detailGameObject.GetComponent<BoxCollider>().enabled = true;
        
        // Show complete label
        levelPlayModel.AfterWinTween();
    }
    
}
