using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateTile : MonoBehaviour {
    Quaternion fromAngle;
    Quaternion toAngle;
    bool rotateCCW = false;
    float target = 0; 
    bool rotateCC = false;
    public bool paralyzed;
    public bool rotatable = false; 
    float rotationSpeed = 15f;//was 15f
    Game g;
    public Character[] characters; 
    Waypoint[] children;
    public RotateTile[] AllTiles;
    public int queue;
    public int category;
    public int type;
    public Color level_color; 


	// Use this for initialization
	void Start ()
	{
	    paralyzed = false;
	    level_color = GetComponent<SpriteRenderer>().color;
        ChangeVisuals();
        target = transform.eulerAngles.z;
        g = FindObjectOfType<Game>();
        characters = FindObjectsOfType<Character>();
        children = GetComponentsInChildren<Waypoint>();
	    foreach (Character c in characters)
	    {
	        c.initRotation = c.transform.rotation;
        }
	    AllTiles = Object.FindObjectsOfType<RotateTile>();
        queue = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    bool temp = false;
	    foreach (Character c in characters)
	    {
	        if (c.type == 3)
	        {
	            if (CharacterOnTile(c))
	            {
	                temp = true;
                    break;
	            }
	        }
	    }
	    if (temp != paralyzed)
	    {
	        paralyzed = temp;
	        ChangeVisuals();
	    }
	    RotationUpdate();
        
	}

  
    void OnMouseDown()
    {
        if (Game.change_selection)
        {
            Game.AdjustInput(category, Game.tiles);
        }
    }


    void ChangeVisuals()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (paralyzed)
        {
            sr.color = new Color(0.306f, 0.349f, 0.439f);
        }
        else
        {
            //sr.color = new Color(0.482f, 0.553f, 0.690f);
            sr.color = level_color; 
        }
    }
   

    void RotationUpdate()
    {
        if (rotatable)
        {
            if (true)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.X))
                {
                    if (queue < 1)
                    {
                        queue = queue + 1;
                    }

                }

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (queue > -1)
                    {
                        queue = queue - 1;
                    }

                }
            }
        }

        
        if (!rotateCC && !rotateCCW && queue != 0)
        {
            foreach (Character c in characters)
            {
                if (c.type == 3)
                {
                    if (CharacterOnTile(c))
                    {
                        queue = 0;

                        return; 
                    }
                }
            }

            foreach (Character c in characters)
            {
                if (c.type == 4 && CharacterOnTile(c))
                { 
                    c.frozen = !c.frozen;
                    c.updateCrabColor();
                }
            }

            foreach (Character c in characters)
            {
                c.rotating = false;
                if (c.to != null)
                {
                    foreach (Waypoint w in c.to.neighbors)
                    {
                        if (w != null && OnRotatingTile(w))
                        {
                            c.rotating = true;
                            //c.initRotation = c.transform.rotation;
                        }
                    }
                    if (OnRotatingTile(c.to) && (c.progress < c.MaxProgress / 2))
                    {
                        c.rotating = true;
                        //c.initRotation = c.transform.rotation;
                    }
                }

            }
            if (queue > 0) //rotate clockwise
            {
                foreach (Waypoint child in children)
                {
                    RemoveNeighbors(child);
                }
                rotateCC = true;
                /*
                foreach (Character c in characters)
                {
                    if (c.type == 3 && c.rotating == true)
                    {
                        rotateCC = false; 
                    }
                }
                */

                if (rotateCC)
                {
                    target -= 90;
                    queue = queue - 1;
                }
            }
            else //rotate counterclockwise
            {
                foreach (Waypoint child in children)
                {
                    RemoveNeighbors(child);
                }
                rotateCCW = true;
                /*
                foreach (Character c in characters)
                {
                    if (c.type == 3 && c.rotating == true)
                    {
                        rotateCCW = false;
                    }
                }
                */
                if (rotateCCW)
                {
                    target += 90;
                    queue = queue + 1;
                }
            }
            foreach (Character c in characters)
            {
                MoveWithTile(c);
            }
        }
        else if (rotateCC)
        {
            transform.Rotate(Vector3.back * rotationSpeed);
            if (transform.localRotation == Quaternion.Euler(0, 0, target))
            {
                foreach (Waypoint child in children)
                {
                    g.SetNeighbors(child);
                }
                rotateCC = false;
                transform.Rotate(Vector3.zero);
                foreach (Character p in characters)
                {
                    p.rotating = false;
                    p.transform.parent = null;

                    if (p.waiting)
                    {
                        p.ChooseDirection(p.to, p.direction, p.type);
                        p.progress = p.MaxProgress;
                        p.waiting = false;
                    }
                    
                    FindNewGoal(p);
                    
                }
            }
        }
        else if (rotateCCW)
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
            if (transform.localRotation == Quaternion.Euler(0, 0, target))
            {
                rotateCCW = false;
                transform.Rotate(Vector3.zero);
                foreach (Waypoint child in children)
                {
                    g.SetNeighbors(child);
                }
                foreach (Character p in characters)
                {             
                    p.rotating = false;
                    p.transform.parent = null;

                    if (p.waiting)
                    {
                        p.ChooseDirection(p.to, p.direction, p.type);
                        p.progress = p.MaxProgress;
                        p.waiting = false;
                    }
                    
                    FindNewGoal(p);
                    
                }
            }
        }



    }

    /*
    void LateUpdate()
    {
        foreach (Character c in characters)
        {
            c.transform.rotation = c.initRotation;
        }
        
    }
    */
    
    void RemoveNeighbors(Waypoint w)
    {
        for(int i = 0; i < 4; i++)
        {
            if(w.neighbors[i] != null)
            {
                w.neighbors[i].neighbors[(i + 2) % 4] = null;
                w.neighbors[i] = null;
            }
        }
    }

  
    public bool OnRotatingTile(Waypoint w)//wont work if multiple tiles rotate at once
    {
        if (w == null)
        {
            return false;
        }
        return (w.transform.parent == transform);
    }
    
    public bool CharacterOnTile(Character c)
    {
        if (c.to == null)
        {
            return OnRotatingTile(c.from);
        }
        if (OnRotatingTile(c.to) && OnRotatingTile(c.from))
        {
            return true; 
        }
        else if (OnRotatingTile(c.to))
        {
            if (c.progress <= c.MaxProgress/2)
            {
                return true; 
            }
        }
        else if (OnRotatingTile(c.from))
        {
            if (c.progress >= c.MaxProgress/2)
            {
                return true; 
            }
        }
        return false; 
    }



    public void MoveWithTile(Character p)
    {
        if ((p.progress <= (p.MaxProgress / 2) && p.waiting == false) && OnRotatingTile(p.to) || 
            (p.progress >= (p.MaxProgress / 2)) && OnRotatingTile(p.from))
        {
            // move with tile
            p.transform.parent = transform;
            if (rotateCC)
            {
                p.direction = Character.Right(p.direction);
            }
            else if (rotateCCW)
            {
                p.direction = Character.Left(p.direction);
            }
        }

        if ((p.progress > (p.MaxProgress / 2) && (OnRotatingTile(p.to) && !OnRotatingTile(p.from))) ||
        (p.progress >= (p.MaxProgress / 2) && (!OnRotatingTile(p.to) && OnRotatingTile(p.from))))
        {
            // p.progress = p.MaxProgress - p.progress;
            //p.to = p.from;
            //p.direction = Character.Behind(p.direction);
            p.stopped = true;
        }
    }

    public void FindNewGoal(Character p)//at some point should take a character as input
    {
        if (p.stopped && OnRotatingTile(p.to) != OnRotatingTile(p.from))
        {
            if (p.from.neighbors[p.direction] == null)
            {
                p.to = p.from;
                p.progress = p.MaxProgress - p.progress;
                p.direction = Character.Behind(p.direction);
                p.transform.Rotate(Vector3.back * 180);
                p.stopped = false;
            }
            else
            {
                p.to = p.from.neighbors[p.direction];
                p.stopped = false;
            }
        }
    }
}
