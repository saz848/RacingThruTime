﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    public int MaxProgress = 60;
    public int progress;
    public int direction;
    public Waypoint to;
    public Waypoint from;
    public Vector3 vel; 

	// Use this for initialization
	void Start () {
        progress = MaxProgress;
        direction = 0;
        to = GameObject.FindGameObjectWithTag("Goal").GetComponent<Waypoint>();
        from = GameObject.FindGameObjectWithTag("Start").GetComponent<Waypoint>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += (to.transform.position - transform.position) / progress;
        progress = progress - 1;
        if (progress == 0)
        {
            ChooseDirection(to, direction);
            progress = MaxProgress;
        }
	}
    
    void ChooseDirection(Waypoint current, int dir)
    {
        if (current.neighbors[Right(dir)] != null)
        {
            to = current.neighbors[Right(dir)];
            from = current;
            direction = Right(dir);
            //transform.Rotate(Vector3.forward * -90);
            // implement velocity update
        }

        else if (current.neighbors[dir] != null)
        {
            to = current.neighbors[dir];
            from = current;
            // implement velocity update
        }

        else if (current.neighbors[Left(dir)] != null)
        {
            to = current.neighbors[Left(dir)];
            from = current;
            direction = Left(dir);
            //transform.Rotate(Vector3.forward * 90);
            // implement velocity update
        }

        else if (current.neighbors[Behind(dir)] != null)
        {
            to = current.neighbors[Behind(dir)];
            from = current;
            direction = Behind(dir);
            //transform.Rotate(Vector3.forward * 180);
            // implement velocity update
        }
    }

    // direction helper functions
    public static int Right(int direction)
    {
        return ((direction + 1) % 4);
    }

    public static int Left(int direction)
    {
        return ((direction + 3) % 4);
    }

    public static int Behind(int direction)
    {
        return ((direction + 2) % 4);
    }
}
