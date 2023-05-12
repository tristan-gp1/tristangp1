using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //refrence all waypoints in our enemy path
    public List<Transform> points;
    //the next innt value for the next index positioninn our list
    public int nextId;
    //create a int that will help us change our world
    private int idChangeValue = 1;
    //float set to speed of our enemy
    public float speed = 2;
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void MoveToNextPoint()
    {
        //create a variable to set the our goalpoint based off our list
        Transform goalPoint = points[nextId];
        //flip the enemy so it is looking at its current goalPoint
        if(goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //move towards our goalPoint 
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);
        //check the distance between enemy and goalpoint to trigger the next point
        if (Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            //check if we are at the end of the line make the change to -1
            if (nextId == points.Count - 1)
            {
                idChangeValue = -1;
            }
            //check if we are at the start of the line make the change to +1
            if ( nextId == 0)
            {
                idChangeValue = +1;
            }
            nextId += idChangeValue;
        }
    }
}
