using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    /*
        neighbors[0] = north;
        neighbors[1] = east;
        neighbors[2] = south;
        neighbors[3] = west; 
    */

    public Waypoint[] neighbors = new Waypoint[4];
    public int radius = 1;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
