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
	            Game this_game = FindObjectOfType<Game>();
                if (c.type == 0)
	            {
	                Camera.main.GetComponent<Camera>().backgroundColor = new Color(0, 0, 0);
                    this_game.dead = true; 

	            }
                all_chars = FindObjectsOfType<Character>();
                
                RotateTile[] rotate_tiles = FindObjectsOfType<RotateTile>();
                this_game.game_chars = all_chars;
                foreach (RotateTile rt in rotate_tiles)
                {
                    if (this_game.dead == true)
                    {
                        SpriteRenderer sr = rt.GetComponent<SpriteRenderer>();
                        sr.color = new Color((128f / 255f), (128f / 255f), (128f / 255f));
                    }
                    rt.characters = all_chars;
                }
            }
        }
	}
}
