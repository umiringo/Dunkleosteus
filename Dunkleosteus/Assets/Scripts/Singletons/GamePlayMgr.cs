using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GlobalDefines;

class LinkedPair
{
    public int indexBegin;
    public int indexEnd;
    public GameObject line;
    public LinkedPair(int b, int e, GameObject l)
    {
        if(b > e) {
            indexBegin = b;
            indexEnd = e;
        }
        else if(e > b) {
            indexBegin = e;
            indexEnd = b;
        }
        else {
            Debug.LogError("GamePlayMgr:LinkedPair: Wired situation! indexBegin should euqals indexEnd! " + b.ToString() + e.ToString());
            return;
        }
        line = l;
    }
    public bool EqualPair(int s, int e)
    {
        if((indexBegin == s && indexEnd == e) || (indexBegin == e && indexEnd == s)) {
            return true;
        }
        return false;
    }
};

public class GamePlayMgr : Singleton<GamePlayMgr> {

    protected GamePlayMgr() {}

    public string identify = "GamePlayMgr";

    private List<LinkedPair> _linkedLineList;
    private Hashtable _starHashTable;
    private GameObject _readyStar;
    private GameObject _LineTemplate;
    private GameObject _LineContainer;

	// Use this for initialization
	void Start () 
    {
        _linkedLineList = new List<LinkedPair>();
        _starHashTable = new Hashtable();
        _readyStar = null;
        _LineTemplate = Resources.Load(PathContainer.LinkedLinePrefabPath) as GameObject;
        _LineContainer = GameObject.Find(PathContainer.LineContainerPath);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    ///////////////////////////////////////////////////////////////////////////////////
    /// Interfaces                                                                  ///
    ///////////////////////////////////////////////////////////////////////////////////
    public void OnStarClicked(GameObject goStar)
    {
        Debug.Log("GamePlayMgr:OnStarClick");
        //Check whether ready
        if (_readyStar == null) {
            //first one
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////
    /// Inner logic function                                                        ///
    ///////////////////////////////////////////////////////////////////////////////////
    //Check whether line(b,e) is already existed
    private bool HaveLinkedLine(int b, int e)
    {
        foreach( LinkedPair lp in _linkedLineList )
        {
            if( lp.EqualPair(b, e) ) {
                return true;
            }
        }
        return false;
    }

    //Get the reference of line(b,e)
    private int GetLinkedLine(int b, int e)
    {
        for ( int i = 0; i < _linkedLineList.Count; ++i )
        {
            if(_linkedLineList[i].EqualPair(b, e)) {
                return i;
            }
        }
        return -1;
    }

    //Check whether star(s) is already have be connected
    private bool IsStarLinked(int s)
    {
        if (_starHashTable.Contains(s) && (int)_starHashTable[s] > 0) {
            return true;
        }
        return false;
    }

    //Refresh Stars state
    private void RefreshStar(GameObject goStar)
    {
        Star starComponent = goStar.GetComponent<Star>();
        int indexStar = starComponent.index;
        //Check whether is ready
        if (_readyStar != null && _readyStar.GetComponent<Star>().index == indexStar) {
            //ready state, waiting for second star
            if (IsStarLinked(indexStar)) {
                //linked
                starComponent.SetLinked();
            }
            else {
                starComponent.SetChosen();
            }
        }
        else {
            //Not ready
            if (IsStarLinked(indexStar)) {
                starComponent.SetLinked();
            }
            else {
                starComponent.SetNormal();
            }
        }
    }

    //Add a line, this function is also the key logic of linked
    private void AddLinkedLine(GameObject goBegin, GameObject goEnd)
    {
        if (goBegin == null || goEnd == null) {
            Debug.LogError("GamePlayMgr:AddLinkedLine: Something wrong, star GameObject cant be null!");
            return;
        }
        int indexB = goBegin.GetComponent<Star>().index;
        int indexE = goEnd.GetComponent<Star>().index;

        //Check whether already linke
        int index = this.GetLinkedLine(indexB, indexE);
        if (index == -1) {
            //Already linked, try to delete it
            Debug.Log("GamePlayMgr:AddLinkedLine: Already linked, try to delete it. start = " + indexB.ToString() + " end = " + indexE.ToString());
            GameObject goLine = _linkedLineList[index].line;
            //delte from list
            _linkedLineList.RemoveAt(index);
            //modify hash table
            if(IsStarLinked(indexB)) {
                _starHashTable[indexB] = (int)_starHashTable[indexB] - 1;
            }
            if(IsStarLinked(indexE)) {
                _starHashTable[indexE] = (int)_starHashTable[indexE] - 1;
            }
            //delete the line
            Destroy(goLine);
        }
        else {
            // Try to link two stars

            // Instantiate line
            GameObject linkedLine = Instantiate(_LineTemplate);
            linkedLine.transform.parent = _LineContainer.transform;
            linkedLine.transform.localPosition = new Vector3(0, 0, 0);
            linkedLine.transform.localRotation = Quaternion.identity;

            // Add record to List
            LinkedPair lp = new LinkedPair(indexB, indexE, linkedLine);
            _linkedLineList.Add(lp);
            // Add record to Hash
            if (_starHashTable.ContainsKey(indexB)) {
                _starHashTable[indexB] = (int)_starHashTable[indexB] + 1;
            }
            else {
                _starHashTable[indexB] = 1;
            }
            if (_starHashTable.ContainsKey(indexE)) {
                _starHashTable[indexE] = (int)_starHashTable[indexE] + 1;
            }
            else {
                _starHashTable[indexE] = 1;
            }
            // Draw Line
            //linkedLine.GetComponent<LineDrawer>.Draw(goBegin, goEnd);
        }
        
        // Reset ready starts
        _readyStar = null;
        // Refresh the stars state
        this.RefreshStar(goBegin);
        this.RefreshStar(goEnd);
    }
}
