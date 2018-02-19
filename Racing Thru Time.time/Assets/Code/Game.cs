using System.Collections;
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
    public RotateTile[] tiles;
    public static Color[] default_colors = new Color[3];
    public static Color[] highlighted_colors = new Color[3];
    // Use this for initialization
    void Start ()
    {
        highlighted_colors[0] = new Color((173f/255f), (51f/255f), 1);
        default_colors[0] = new Color((214f/255f), (153f/255f), 1);
        highlighted_colors[1] = new Color((173f / 255f), (51f / 255f), 1);
        default_colors[1] = new Color((214f / 255f), (153f / 255f), 1);
        highlighted_colors[2] = new Color((173f / 255f), (51f / 255f), 1);
        default_colors[2] = new Color((214f / 255f), (153f / 255f), 1);
        allWaypoints = FindObjectsOfType<Waypoint>();
        tiles = FindObjectsOfType<RotateTile>();
        AdjustInput(1);
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
	            Debug.Log(player.radius + c.radius);
                if (distance < (player.radius + c.radius))
	            {
	                EndGame(DEFEAT);
	            }
            }
	    }

	    if (Input.GetKeyDown(KeyCode.Q))
	    {
	        AdjustInput(0);
	    }

	    else if (Input.GetKeyDown(KeyCode.W))
	    {
	        AdjustInput(1);
	    }

        else if (Input.GetKeyDown(KeyCode.E))
	    {
	        AdjustInput(2);
	    }

	    else if (Input.GetKeyDown(KeyCode.R))
	    {
	        AdjustInput(3);
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
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void AdjustInput(int cat)
    {
        foreach (RotateTile r in tiles)
        {
            SpriteRenderer tile_color = r.GetComponent<SpriteRenderer>();
            if (r.category == cat)
            {
                r.rotatable = true; 
                tile_color.color = highlighted_colors[r.category];
            }
            else
            {
                r.rotatable = false; 
                tile_color.color = default_colors[r.category];
            }
        }
    }



    
}
