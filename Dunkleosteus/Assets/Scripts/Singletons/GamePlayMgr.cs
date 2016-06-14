using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    private List<LinkedPair> _connectedLineList;
    private Hashtable _starHashTable;
    private GameObject _readyBegin;
    private GameObject _readyEnd;

	// Use this for initialization
	void Start () 
    {
        _connectedLineList = new List<LinkedPair>();
        _starHashTable = new Hashtable();
        _readyBegin = null;
        _readyEnd = null;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    ///////////////////////////////////////////////////////////////////////////////////
    /// Interfaces                                                                  ///
    ///////////////////////////////////////////////////////////////////////////////////

    //Check whether line(b,e) is already existed
    public bool HaveLinkedLine(int b, int e)
    {
        foreach( LinkedPair lp in _connectedLineList )
        {
            if( lp.EqualPair(b, e) ) {
                return true;
            }
        }
        return false;
    }

    //Get the reference of line(b,e)
    public int GetLinkedLine(int b, int e)
    {
        for ( int i = 0; i < _connectedLineList.Count; ++i )
        {
            if(_connectedLineList[i].EqualPair(b, e)) {
                return i;
            }
        }
        return -1;
    }

    //Check whether star(s) is already have be connected
    public bool IsStarLinked(int s)
    {
        if(_starHashTable.Contains(s) && (int)_starHashTable[s] > 0) {
            return true;
        }
        return false;
    }

    //Refresh Stars state
    public void RefreshStar(GameObject goStar)
    {

    }

    //Add a line, this function is also the key logic of linked
    public void AddLinkedLine(GameObject goBegin, GameObject goEnd)
    {
        if(goBegin == null || goEnd == null) {
            Debug.LogError("GamePlayMgr:AddLinkedLine: Something wrong, star GameObject cant be null!");
            return;
        }
        int indexB = goBegin.GetComponent<Star>().index;
        int indexE = goEnd.GetComponent<Star>().index;

        //Check whether already linke
        int index = this.GetLinkedLine(indexB, indexE);
        if(index == -1) {
            //Already linked, try to delete it
            Debug.Log("GamePlayMgr:AddLinkedLine: Already linked, try to delete it. start = " + indexB.ToString() + " end = " + indexE.ToString());
            GameObject goLine = _connectedLineList[index].line;
            //delte from list
            _connectedLineList.RemoveAt(index);
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
            //Try to link two stars
        }
        
        //reset ready starts
        _readyBegin = null;
        _readyEnd = null;
        //refresh the stars state
        this.RefreshStar(goBegin);
        this.RefreshStar(goEnd);
    }
}
