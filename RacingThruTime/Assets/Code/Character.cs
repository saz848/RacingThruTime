using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public int MaxProgress;
    public int progress;
    public int direction;
    public Waypoint to;
    public Waypoint from;
    public Vector3 vel;
    public Waypoint[] all_wp;
    public Goal g;
    public float radius;
    public bool stopped;
    public bool trapped; 
    public bool rotating;
    public bool waiting;
    public bool starting;
    public bool frozen;
    public bool paused; 
    public int timefactor;
    public Quaternion initRotation;
    public int type;
    public const int PLAYER = 0;
    public const int SIMPLE_ENEMY = 1;
    public const int HEAVY_ENEMY = 3; 

    public static Sprite player; 
    public static SpriteRenderer my_sprite;
    public Color frozen_crab;
    public Color original_color; 

    public static Sprite down;
    public static Sprite right;
    public static Sprite up;
    public static Sprite left;

    public float curr_time; 

// Use this for initialization
public void Start()
{
        MaxProgress = 20;
        progress = MaxProgress;
        stopped = false;
        rotating = false;
        waiting = false;
        frozen = false;
 
        all_wp = FindObjectsOfType<Waypoint>();
        from = FindClosestWaypoint(transform.position);
        to = from;
        g = FindObjectOfType<Goal>();
        starting = true;
        trapped = false;
        radius = 0.1f;
        timefactor = 1;

        down = Resources.Load<Sprite>("CharacterSprites/Down");
        left = Resources.Load<Sprite>("CharacterSprites/Left");
        right = Resources.Load<Sprite>("CharacterSprites/Right");
        up = Resources.Load<Sprite>("CharacterSprites/Up");

        my_sprite = GetComponent<SpriteRenderer>();
        if (type == 0)
        {
            original_color = new Color((23f / 255f), (152f / 255f), (15f / 255f));
        }
        else if (type == 1)
        {
            original_color = new Color((9f / 255f), (39f / 255f), (128f / 255f));
        }
        else if (type == 3)
        {
            original_color = new Color((206f / 255f), (79f / 255f), (255f / 255f));
        }
        else if (type == 4)
        {
            original_color = new Color((238f / 255f), (0f / 255f), (0f / 255f));
        }
        my_sprite.color = original_color;

        //my_sprite.sprite = down;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        if (starting)
        {
            ChooseDirection(from, direction, type);
            starting = false; 
        }

        if (trapped)
        {
            ChooseDirection(from, direction, type);
        }

        if (to == null)
        {
            trapped = true;
            to = from;
        }
        else
        {
            trapped = false; 
        }

        if (trapped)
        {
            return; 
        }

        if (stopped == false && frozen == false && paused == false)
        {
            if (waiting == false && progress != 0)
            {
                transform.position += (to.transform.position - transform.position) / (progress);
                progress = progress - 1;
            }

            if (progress <= 0)
            {
                if (!rotating)
                {
                    ChooseDirection(to, direction, type);
                    progress = MaxProgress;
                }
                else
                {
                    waiting = true;
                }
            }

            float distance = Vector3.Distance(transform.position, g.transform.position);
            if (distance < (radius + g.radius) && type == PLAYER)
            {
                Game.EndGame(Game.VICTORY);
            }
        }
    }

    public Waypoint FindClosestWaypoint(Vector3 p)
    {
        float min_dist = Single.PositiveInfinity;
        Waypoint min_wp = null;

        foreach (Waypoint w in all_wp)
        {
            float dist = Vector3.Distance(w.transform.position, p);
            if (dist < min_dist)
            {
                min_dist = dist;
                min_wp = w;
            }
        }
        return min_wp;
    }


    public void ChooseDirection(Waypoint current, int dir, int type)
    {
        if (false)
        {
            if (current.neighbors[Right(dir)] != null)
            {
                to = current.neighbors[Right(dir)];
                from = current;
                direction = Right(dir);



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

                
                // implement velocity update
            }

            else if (current.neighbors[Behind(dir)] != null)
            {
                to = current.neighbors[Behind(dir)];
                from = current;
                direction = Behind(dir);


                // implement velocity update
            }
            else
            {
                from = current;
                to = null;
                trapped = true;
            }
        }
        else if(true)
        {
            if (current.neighbors[dir] != null)
            {
                to = current.neighbors[dir];
                from = current;
                
                // implement velocity update
            }
            else if (current.neighbors[Right(dir)] != null)
            {
                to = current.neighbors[Right(dir)];
                from = current;
                direction = Right(dir);
                //transform.rotation = initRotation; 
                //transform.Rotate(Vector3.back * 270);
                // implement velocity update
            }
            else if (current.neighbors[Left(dir)] != null)
            {
                to = current.neighbors[Left(dir)];
                from = current;
                direction = Left(dir);
                //transform.rotation = initRotation;
                //transform.Rotate(Vector3.back * 90);
                // implement velocity update
            }
            else if (current.neighbors[Behind(dir)] != null)
            {
                to = current.neighbors[Behind(dir)];
                from = current;
                direction = Behind(dir);
                //transform.Rotate(Vector3.back * 180);
                // implement velocity update
            }
            else
            {
                from = current;
                to = null;
                trapped = true;
            }


            if (direction == 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (direction == 2)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direction == 3)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }

        }
        
        
    }

    public void updateCrabColor()
    {
        if (type == 4)
        {
            SpriteRenderer this_sprite = GetComponent<SpriteRenderer>();
            if (frozen)
            {
                this_sprite.color = new Color((238f / 255f), (129f / 255f), (7f / 255f));                
            }
            else this_sprite.color = original_color;
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


