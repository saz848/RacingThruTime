using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusTile : MonoBehaviour {
    Quaternion fromAngle;
    Quaternion toAngle;
    bool rotateCCW = false;
    float target = 0; 
    bool rotateCC = false;
    float rotationSpeed = 15f; 
    Game g;
    Player p; 
    Waypoint[] children; 

	// Use this for initialization
	void Start () {
        g = FindObjectOfType<Game>();
        p = FindObjectOfType<Player>(); 
        children = GetComponentsInChildren<Waypoint>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.X) && !rotateCC && !rotateCCW)
        {
            foreach (Waypoint child in children)
            {
                RemoveNeighbors(child); 
            }
            MoveWithTile(); 
            rotateCC = true;
            target -= 90;
        }

        if (rotateCC)
        {
            transform.Rotate(Vector3.back * rotationSpeed);
            if (transform.localRotation == Quaternion.Euler(0, 0, target))
            {
                rotateCC = false;
                transform.Rotate(Vector3.zero);
                p.transform.parent = null;
                foreach (Waypoint child in children)
                {
                    g.SetNeighbors(child);
                }
            }
        }



        if (Input.GetKeyDown(KeyCode.Z) && !rotateCCW && !rotateCC)
        {
            foreach (Waypoint child in children)
            {
                RemoveNeighbors(child);
            }

            MoveWithTile(); 
            rotateCCW = true;
            target += 90;
        }

        if (rotateCCW)
        {
            transform.Rotate(Vector3.forward* rotationSpeed);
            if (transform.localRotation == Quaternion.Euler(0, 0, target))
            {
                rotateCCW = false;
                transform.Rotate(Vector3.zero);
                p.transform.parent = null; 
                foreach (Waypoint child in children)
                {
                    g.SetNeighbors(child);
                }
                
            }
        }

 

    }

    void RemoveNeighbors(Waypoint w)
    {
        for(int i = 0; i < 4; i++)
        {
            if(w.neighbors[i] != null)
            {
                w.neighbors[i].neighbors[(i + 2) % 4] = null;
                w.neighbors[i] = null;
            }
        }
    }

  
    public bool OnRotatingTile(Waypoint w)
    {
        return (w.transform.parent == transform);
    }


    public void MoveWithTile()
    {
        if ((p.progress <= (p.MaxProgress / 2)) && OnRotatingTile(p.to) || 
            (p.progress > (p.MaxProgress / 2)) && OnRotatingTile(p.from))
        {
            // move with tile
            p.transform.parent = transform;
            if (rotateCC)
            {
                p.direction = Player.Right(p.direction);
            }

            else if (rotateCCW)
            {
                p.direction = Player.Left(p.direction);
            }
        }

        if (p.progress > (p.MaxProgress / 2) && (OnRotatingTile(p.to) ^ OnRotatingTile(p.from))) 
        {
            p.progress = p.MaxProgress - p.progress;
            p.to = p.from;
            p.direction = Player.Behind(p.direction);
        }
    }
}
