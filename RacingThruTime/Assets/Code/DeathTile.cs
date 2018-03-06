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
        Debug.Log(all_chars[0]);
	}
	
	// Update is called once per frame
	void Update () {
	    foreach (Character c in all_chars)
	    {
            if (c.type == 0 && Game.player_exists == false)
            {
                return;
            }
	        float distance = Vector3.Distance(transform.position, c.transform.position);
	        if (distance < (radius + c.radius))
	        {
	            if (c.type == 0)
	            {
                    Game.restart_visible = true;
                    Game.restart_text.SetActive(Game.restart_visible);
                    Game.player_exists = false;
                }
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
