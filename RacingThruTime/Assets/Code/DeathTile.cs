using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTile : MonoBehaviour
{
    public float radius;
    private Character[] all_chars; 

	// Use this for initialization
	void Start ()
	{
	    radius = 0.15f;
	    all_chars = FindObjectsOfType<Character>();
	}
	
	// Update is called once per frame
	void Update () {
	    foreach (Character c in all_chars)
	    {
	        float distance = Vector3.Distance(transform.position, c.transform.position);
	        if (distance < (radius + c.radius))
	        {
	            if (c.type == 0)
	            {
	                Game.EndGame(Game.DEFEAT);
                }
	            else
	            {
                    DestroyImmediate(c.gameObject);
                    DestroyImmediate(c);
                    all_chars = FindObjectsOfType<Character>();
                    Game this_game = FindObjectOfType<Game>();
                    RotateTile[] rotate_tiles = FindObjectsOfType<RotateTile>();
                    this_game.game_chars = all_chars;
                    foreach (RotateTile rt in rotate_tiles)
                    {
                        rt.characters = all_chars;
                    }
	            }
	        }
        }
	}
}
