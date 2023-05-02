using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Reference all waypoints in our enemy path
    public List<Transform> points;
    //The next int value for the next index position in our list
    public int nextID;
    //Create an int that will help us change our nextId
    private int idChangeValue = 1;
    //Float to set our speed of our enemy
    public float speed = 2;

    public Transform player;
    public float detectionDistance;

    SpriteRenderer enemyColor;

    private void Start()
    {
        enemyColor = GetComponent<SpriteRenderer>();
        enemyColor.color = Color.white;
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < detectionDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            enemyColor.color = Color.red;
        }
        else
        {
            MoveToNextPoint();
            enemyColor.color = Color.white;
        }
    }
    void MoveToNextPoint() 
    {
        //Create a variable to set our goalpoint based off our list
        Transform goalpoint = points[nextID];
        //Flip the enemy so it is looiking at its current goalPoint
        if (goalpoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else 
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Move towards our goalpoint
        transform.position = Vector2.MoveTowards(transform.position, goalpoint.position, speed * Time.deltaTime);
        //Checks the distance between enemy and goalpoint to trigger the next point
        if (Vector2.Distance(transform.position, goalpoint.position) < 1f) 
        {
            //Check if we are at the end of te line to make the change to -1
            if (nextID == points.Count - 1) 
            {
                idChangeValue = -1;
            }
            //Check if we are at the end of te line to make the change to +1
            if (nextID == 0)
            {
                idChangeValue = +1;
            }
            nextID += idChangeValue;
        }
    }
}
