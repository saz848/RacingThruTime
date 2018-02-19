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
    public static RotateTile[] tiles;
    public static Color default_color = new Color();
    public static Color highlighted_color = new Color();
    // Use this for initialization
    void Start ()
    {
        highlighted_color = new Color((173f/255f), (51f/255f), 1);
        default_color = new Color((214f/255f), (153f/255f), 1);
        allWaypoints = FindObjectsOfType<Waypoint>();
        tiles = FindObjectsOfType<RotateTile>();
        AdjustInput(0, tiles);
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

	    if (Input.GetKeyDown(KeyCode.Q))
	    {
	        AdjustInput(0, tiles);
	    }
        
	    else if (Input.GetKeyDown(KeyCode.W))
	    {
	        AdjustInput(1, tiles);
	    }

        else if (Input.GetKeyDown(KeyCode.E))
	    {
	        AdjustInput(2, tiles);
	    }

	    else if (Input.GetKeyDown(KeyCode.R))
	    {
	        AdjustInput(3, tiles);
	    }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            AdjustInput(4, tiles);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            AdjustInput(5, tiles);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            AdjustInput(6, tiles);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            AdjustInput(7, tiles);
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
        int numScenes = SceneManager.sceneCountInBuildSettings;
        if (result == VICTORY)
        {
            if (SceneManager.GetActiveScene().buildIndex < numScenes - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
            
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public static void AdjustInput(int cat, RotateTile[] tile_list)
    {
        foreach (RotateTile r in tile_list)
        {
            SpriteRenderer tile_color = r.GetComponent<SpriteRenderer>();
            if (r.category == cat)
            {
                r.rotatable = true; 
                tile_color.color = highlighted_color;
            }
            else
            {
                r.rotatable = false; 
                tile_color.color = default_color;
            }
        }
    }



    
}
