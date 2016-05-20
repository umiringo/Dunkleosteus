using UnityEngine;
using System.Collections;

public class SimpleButton : MonoBehaviour {

	// Use this for initialization
    public GameObject beginObj;
    public GameObject endObj;
    public GameObject beginObj2;
    public GameObject endObj2;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick() {
        GameObject go = new GameObject();
        go.layer = 5;
        LineRenderer lineRender = go.AddComponent<LineRenderer>();
        lineRender.SetColors(Color.white, Color.white);
        lineRender.SetWidth(0.02f, 0.02f);
        lineRender.SetVertexCount(2);
        lineRender.SetPosition(0, beginObj.GetComponent<Transform>().position);
        lineRender.SetPosition(1, endObj.GetComponent<Transform>().position);

        GameObject go2 = new GameObject();

        LineRenderer lineRender2 = go2.AddComponent<LineRenderer>();
        lineRender2.SetColors(Color.white, Color.white);
        lineRender2.SetWidth(0.02f, 0.02f);
        lineRender2.SetVertexCount(2);
        lineRender2.SetPosition(0, beginObj2.GetComponent<Transform>().position);
        lineRender2.SetPosition(1, endObj2.GetComponent<Transform>().position);
    }
}
