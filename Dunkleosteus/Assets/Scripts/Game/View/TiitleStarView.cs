using UnityEngine;
using System.Collections;
using GlobalDefines;
using System.Collections.Generic;

public class TiitleStarView : MonoBehaviour {
	// Use this for initialization
	void Start () {
        // 添加连接线
        for (int i = 1; i < 7; i++) {
            Transform bTrans = gameObject.transform.Find("Star" + i);
            int eIndex = i + 1;
            Transform eTrans = gameObject.transform.Find("Star" + eIndex);
            this.AddLine(bTrans, eTrans);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void AddLine(Transform beginTransform, Transform endTransform)
    {
        GameObject linkedLine = beginTransform.FindChild("Line").gameObject;
        UISprite lineSprite = linkedLine.GetComponent<UISprite>();
        lineSprite.pivot = UIWidget.Pivot.Center;

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
        int width = (int)(distance / scale);
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
