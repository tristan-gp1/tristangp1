using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //Creates a box to store info (position, rotation, scale)
    Transform player;
    PlayerManager playerManager;

    //Store if our boss is flipped or not setting the value to false
    public bool isFlipped = false;

    //Create a variable for the health of our boss
    public int bossHealth = 10;

    //Create a set of bools to check and set our bosses
    public bool phase2 = false;
    public bool phase3 = false;
    public bool isDead = false;

    //Create a public variable for attackRange
    public float attackRange = 2.5f;
    public float speed;

    //Reference the location for our position to create our projectile 
    public Transform shotLocation;
    public GameObject projectile;
    public GameObject projectile2;
    //Create a timer system to ensure that the boss is shooting regularly
    public float timer;
    public int waitingTime;
    //Create a function that will clone and create a version of our prefab projectile
    // Start is called before the first frame update
    void Start()
    {
        //Set our reference for our playerManager
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //Create a series of if else statements that will check to see if the boss is below 7 and above 3, below 3 and above 1, and is less or equal to 0
        //Phase 2
        if (bossHealth < 7 && bossHealth > 3)
        {
            phase2 = true;
            speed = 2f;
            attackRange = 6f;
            Debug.Log("phase2");
        }
        //Phase 3
        else if (bossHealth < 3 && bossHealth >= 1)
        {
            phase2 = false;
            phase3 = true;
            speed = 1f;
            attackRange = 8f;
            Debug.Log("phase3");
        }
        //Boss is defated
        else if (bossHealth <= 0) 
        {
            phase3 = false;
            isDead = true;
            Debug.Log("death");
        }
        timer += Time.deltaTime;
    }

    public void ProjectileShoot() 
    {
        if (timer > waitingTime) 
        {
            if (phase2)
            {
                //Clone and create our projectile
                GameObject clone = Instantiate(projectile, shotLocation.position, Quaternion.identity);
                //Reset our timer
                timer = 0f;
            }
            else if (phase3) 
            {
                //Clone and create our projectile
                GameObject clone = Instantiate(projectile2, shotLocation.position, Quaternion.identity);
                //Reset our timer
                timer = 0f;
            }
        }
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z = -1f;
        if (transform.position.x < player.position.x && isFlipped) 
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            playerManager.TakeDamage();
        }
    }
}
