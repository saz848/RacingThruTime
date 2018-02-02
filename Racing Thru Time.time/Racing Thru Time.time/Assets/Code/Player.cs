using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int direction;
    public Waypoint to;
    public Waypoint from;
    public Vector3 vel; 

	// Use this for initialization
	void Start () {
        direction = 0;
        to = GameObject.FindGameObjectWithTag("Goal").GetComponent<Waypoint>();
        from = GameObject.FindGameObjectWithTag("Start").GetComponent<Waypoint>();
        ChangeVelocity(to, from);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + vel * Time.fixedDeltaTime;
        if (Mathf.Abs(transform.position.magnitude - to.transform.position.magnitude) <= 1)
        {
            ChooseDirection(to, direction);
        }
	}

    void ChangeVelocity(Waypoint current, Waypoint goal)
    {
        vel = -(goal.transform.position - current.transform.position) / 5f;
    }
    
    void ChooseDirection(Waypoint current, int direction)
    {
        if (current.neighbors[Right(direction)] != null)
        {
            to = current.neighbors[Right(direction)];
            from = current;
            ChangeVelocity(to, from);
            // implement velocity update
        }

        else if (current.neighbors[Left(direction)] != null)
        {
            to = current.neighbors[Left(direction)];
            from = current;
            ChangeVelocity(to, from);
            // implement velocity update
        }

        else if (current.neighbors[direction] != null)
        {
            to = current.neighbors[direction];
            from = current;
            ChangeVelocity(to, from);
            // implement velocity update
        }

        else if (current.neighbors[Behind(direction)] != null)
        {
            to = current.neighbors[Behind(direction)];
            from = current;
            ChangeVelocity(to, from);
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
        return ((direction - 1) % 4);
    }

    public static int Behind(int direction)
    {
        return ((direction + 2) % 4);
    }
}
