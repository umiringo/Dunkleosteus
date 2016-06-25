using UnityEngine;
using System.Collections;

public class GameContainer : MonoBehaviour {

	// Use this for initialization
    public GameObject skyGameObject;
    public GameObject detailGameObject;
    public GameObject starContainer;
    public EventController eventController;
   
    private GameObject triggerGameObject;
    private TweenRotation skyTween;
    private TweenRotation detailTween;
    
    public float duration = 1.0f;
    
    private bool detailTriggered;
	void Start () 
    {
        triggerGameObject = null;
        detailTriggered = false;
        
        detailGameObject.transform.localEulerAngles = new Vector3(0, 90, 0);
        skyTween = starGameObject.GetComponent<TweenRotation>();
        if (skyTween == null) {
            skyTween = starGameObject.AddComponent<TweenRotation>();
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
        
        //set play tween(scale)'s onFinished TODO
        
        //unable star and detail box collider
        skyGameObject.GetComponent<BoxCollider2D>().enable = false;
        detailGameObject.GetComponent<BoxCollider2D>().enable = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void GameWin()
    {
        //unable star touch
        SwitchStarsBoxCollider(false);
        //start scale
    }
    
    public void SwitchStarsBoxCollider(bool enable)
    {
        foreach(Transform child in starContainer.transform) {
            child.gameObject.GetComponent<BoxCollider2D>().enabled = enable;
        }
    }

    ///////////////////////////////////////////////////////////////
    // delegate                                                  //
    ///////////////////////////////////////////////////////////////
    public void BeginToFlop(GameObject touchedGameObject)
    {
        triggerGameObject = touchedGameObject;
        if (touchedGameObject == starGameObject) {
            starTween.Play(true);
            if(!detailTriggered){
                // show the menu
                eventController.OnDetailTriggered();
                detailTriggered = true;
            }
        }
        else if (touchedGameObject == detailGameObject) {
            detailTween.Play(false);
        }
    }
    
    //starTween.onFinished
    public StarTweenEventDelegate()
    {
        if(triggerGameObject == starGameObject) {
            detailGameObject.Play(true);
        }
    }
    
    //detailTween.onFinish
    public DetailTweenEventDelegate()
    {
        if(triggerGameObject == detailGameObject) {
            starTween.Play(false);
        }
    }
    
    //winTween.onFinish
    public WinPlayTweenEventDelegate()
    {
        //enable star and detail box collider
        starGameObject.GetComponent<BoxCollider2D>().enable = true;
        detailGameObject.GetComponent<BoxCollider2D>().enable = true;
             
    }
    
}
