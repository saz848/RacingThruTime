using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class Character : MonoBehaviour
    {

        public int MaxProgress = 60;
        public int progress;
        public int direction;
        public Waypoint to;
        public Waypoint from;
        public Vector3 vel;
        public Waypoint[] all_wp;
        public Goal g;
        public float radius = 0.2f;
        public bool stopped;
        public bool rotating;
        public bool waiting;
        public bool starting;
        public Quaternion initRotation;
        public int type;
        public const int PLAYER = 0;
        public const int SIMPLE_ENEMY = 1; 

    // Use this for initialization
    public void Start()
        {
            progress = MaxProgress;
            stopped = false;
            rotating = false;
            waiting = false;
            direction = 0;
            all_wp = FindObjectsOfType<Waypoint>();
            from = FindClosestWaypoint(transform.position);
            g = FindObjectOfType<Goal>();
            starting = true; 
        }

        // Update is called once per frame
        public void Update()
        {

            if (starting)
            {
                ChooseDirection(from, direction);
                starting = false; 
            }

            if (stopped == false)
            {
                if (waiting == false)
                {
                    transform.position += (to.transform.position - transform.position) / progress;
                    progress = progress - 1;
                }

                if (progress == 0)
                {
                    if (!rotating)
                    {
                        ChooseDirection(to, direction);
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


        public void ChooseDirection(Waypoint current, int dir)
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


