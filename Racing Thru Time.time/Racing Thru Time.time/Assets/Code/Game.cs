using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public Waypoint[] allWaypoints;
    public float radius = 0.61f;
    // Use this for initialization
    void Start () {
        allWaypoints = FindObjectsOfType<Waypoint>();
        Debug.Log(allWaypoints);
     
        foreach (Waypoint w in allWaypoints) {
            foreach (Waypoint other_w in allWaypoints) {
                Debug.Log(w == other_w);
                Debug.Log(w.transform.position);
                Debug.Log(w.transform.parent.position);
                if (w != other_w) {
                    float dist = Vector3.Distance(w.transform.parent.position, other_w.transform.parent.position);
                    Debug.Log(radius);
                    if (dist < 1.22f)
                    {
                        Debug.Log("we're in the loop");
                        if (w.transform.parent.position.x == other_w.transform.parent.position.x) {
                            if (w.transform.parent.position.y > other_w.transform.parent.position.y) {
                                w.neighbors[2] = other_w;
                                other_w.neighbors[0] = w;
                            }
                            else
                            {
                                w.neighbors[0] = other_w;
                                other_w.neighbors[2] = w;
                            }
                        }

                        else
                        {
                            if (w.transform.parent.position.x > other_w.transform.parent.position.x)
                            {
                                w.neighbors[3] = other_w;
                                other_w.neighbors[1] = w;
                            }
                            else
                            {
                                w.neighbors[1] = other_w;
                                other_w.neighbors[3] = w;
                            }
                        }
                    }
                }
            }
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
