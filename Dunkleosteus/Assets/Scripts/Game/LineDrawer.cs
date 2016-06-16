using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour {

	// Use this for initialization
    private LineRenderer lineRender;
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Draw(GameObject goBegin, GameObject goEnd)
    {
        Debug.Log("Begin to Draw Line");
        lineRender = GetComponent<LineRenderer>();
        Vector3 posBegin = goBegin.GetComponent<Transform>().position;
        Vector3 posEnd = goEnd.GetComponent<Transform>().position;
        lineRender.SetPosition(0, posBegin);
        lineRender.SetPosition(1, posEnd);
    }
}
