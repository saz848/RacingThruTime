﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    public Waypoint[] allWaypoints;
    public float radius = 0.61f;
    public const int VICTORY = 1;
    public const int DEFEAT = 5;
    Character[] game_chars;
    Character player; 

    // Use this for initialization
    void Start () {

        allWaypoints = FindObjectsOfType<Waypoint>();

     
        foreach (Waypoint w in allWaypoints) {
            SetNeighbors(w);
        }
        game_chars = FindObjectsOfType<Character>();
        foreach (Character c in game_chars)
        {
            if (c.type == 0)
            {
                player = c;
                break;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
	    foreach (Character c in game_chars)
	    {
	        if (c != player)
	        {
	            float distance = Vector3.Distance(player.transform.position, c.transform.position);
	            if (distance < (player.radius + c.radius))
	            {
	                EndGame(DEFEAT);
	            }
            }
	    }
    }

    public void SetNeighbors(Waypoint w)
    {
        foreach (Waypoint other_w in allWaypoints)
        {

            if (w != other_w)
            {
                float dist = Vector3.Distance(w.transform.position, other_w.transform.position);

                if (dist < 1.02f)
                {
                    double deltaX = Mathf.Abs(w.transform.position.x - other_w.transform.position.x);
                    double deltaY = Mathf.Abs(w.transform.position.y - other_w.transform.position.y);

                    if (deltaY > deltaX)
                    {
                        if (w.transform.position.y > other_w.transform.position.y)
                        {
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
                        if (w.transform.position.x > other_w.transform.position.x)
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

    public static void EndGame(int result)
    {
        if (result == VICTORY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
