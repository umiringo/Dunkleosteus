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
        
        //set winTween
        winTween = gameObject.GetComponent<UIPlayTween>();
        winTween.onFinished.Add(new EventDelegate(WinPlayTweenEventDelegate));
        
        //unable star and detail box collider
        skyGameObject.GetComponent<BoxCollider>().enabled = false;
        detailGameObject.GetComponent<BoxCollider>().enabled = false;
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
        winTween.Play(true);
    }
    
    public void SwitchStarsBoxCollider(bool enable)
    {
        foreach(Transform child in starContainer.transform) {
            child.gameObject.GetComponent<BoxCollider>().enabled = enable;
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
    public void SkyTweenEventDelegate()
    {
        if(triggerGameObject == skyGameObject) {
            detailTween.Play(true);
        }
    }
    
    //detailTween.onFinish
    public void DetailTweenEventDelegate()
    {
        if(triggerGameObject == detailGameObject) {
            skyTween.Play(false);
        }
    }
    
    //winTween.onFinish
    public void WinPlayTweenEventDelegate()
    {
        //enable star and detail box collider
        skyGameObject.GetComponent<BoxCollider>().enabled = true;
        detailGameObject.GetComponent<BoxCollider>().enabled = true;
             
    }
    
}
