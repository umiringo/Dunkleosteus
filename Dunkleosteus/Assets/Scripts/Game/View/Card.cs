using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;
using SimpleJSON;

public class Card : MonoBehaviour {
    // Use this for initialization
    public string levelName;
    public GameObject skyGameObject;
    public GameObject detailGameObject;
    public GameObject starContainer;
    public GameObject lineContainer;

    private GameObject triggerGameObject;
    private TweenRotation skyTween;
    private TweenRotation detailTween;
    private UIWidget textureWidget;

    public float duration = 1.0f;
    
    private bool detailTriggered;
    private List<int> _correctAnswerList = new List<int>();

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

        // 自动连线
        AutoLinkLine();
    }
    
    // Update is called once per frame
    void Update () 
    {
    
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

    public void AutoLinkLine()
    {
        _correctAnswerList.Clear();
        JSONNode jo = TemplateMgr.Instance.GetTemplateString(ConfigKey.LevelInfo, levelName); 
        // Load answer
        JSONArray answerList = jo["answer"] as JSONArray;
        foreach (JSONNode answerObject in answerList) {
            int tmpAnswer = answerObject.AsInt;
            _correctAnswerList.Add(tmpAnswer);
        }

        // Draw Line
        foreach(int ans in _correctAnswerList) {
            GameObject beginAns = starContainer.transform.Find("Star" + ans / 100).gameObject;
            GameObject endAns = starContainer.transform.Find("Star" + ans % 10).gameObject;
            AddStarLine(beginAns.transform, endAns.transform);
        }
    }

    private void AddStarLine(Transform beginTransform, Transform endTransform)
    {
        GameObject linkedLine = Instantiate(Resources.Load(PathContainer.CardLinkedLinePrefabPath)) as GameObject;
        linkedLine.transform.parent = lineContainer.transform;
        UISprite lineSprite = linkedLine.GetComponent<UISprite>();
        lineSprite.pivot = UIWidget.Pivot.Center;
        //lineSprite.depth = DefineNumber.LineDepth;
        // Modify pivot
        if (beginTransform.position.x > endTransform.position.x) {
            if (beginTransform.position.y > endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.TopRight;
            }
            else if (beginTransform.position.y < endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.BottomRight;
            }
            else {
                lineSprite.pivot = UIWidget.Pivot.Right;
            }
        }
        else if (beginTransform.position.x < endTransform.position.x) {
            if (beginTransform.position.y > endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.TopLeft;
            }
            else if (beginTransform.position.y < endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.BottomLeft;
            }
            else {
                lineSprite.pivot = UIWidget.Pivot.Left;
            }
        }
        else {
            if (beginTransform.position.y > endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.Top;
            }
            else if (beginTransform.position.y < endTransform.position.y) {
                lineSprite.pivot = UIWidget.Pivot.Bottom;
            }
            else {
                lineSprite.pivot = UIWidget.Pivot.Center;
            }
        }
        float angle = this.CaculateAngle(beginTransform, endTransform);
        linkedLine.transform.position = beginTransform.position;
        linkedLine.transform.rotation = Quaternion.Euler(0, 0, angle);
        linkedLine.transform.localScale = new Vector3(1, 1, 1);

        float distance = Vector3.Distance(beginTransform.position, endTransform.position);
        float scale = GameObject.Find(PathContainer.UIRootPath).transform.localScale.x;
        float containerScale = lineContainer.transform.localScale.x;
        int width = (int)(distance / scale / containerScale / 0.6);
        lineSprite.width = width;
        linkedLine.SetActive(true);
    }
    
    private float CaculateAngle(Transform begin, Transform end)
    {
        float angle = 0.0f;
        angle = Mathf.Rad2Deg * Mathf.Atan((begin.position.y - end.position.y) / (begin.position.x - end.position.x));
        return angle;
    }
}
