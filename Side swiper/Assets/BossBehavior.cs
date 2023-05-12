using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    //creates a box to store information (position, rotation, scale
    Transform player;
    PlayerManager playerManager;
    public float speed = 6f;
    public float attackRange = .5f;
    //store if our boss is flipped or not setting the value to false
    public bool isFlipped = false;

    public int bossHealth = 10;
    public bool phase2 = false;
    public bool phase3 = false;
    public bool isDead = false;
    // Start is called before the first frame update

    public Transform shotLocation;
    public GameObject projectile;
    public GameObject projectile2;

    public float timer;
    public int waitingTime;

    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag  ("Player").GetComponent<PlayerManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }
    private void Update()
    {
        //create a series of if else statements that chec to see
        //if the boss is below 2 and above 3, below 3 and above 1,
        //and is less than or equal to 0
        if (bossHealth < 7 && bossHealth > 3)
        {
            phase2 = true;
            Debug.Log("Phase2");
              speed = 4f;
              attackRange = 6f;
}
        else if (bossHealth < 3 && bossHealth > 1)
        {
            phase2 = false;
            Debug.Log("Phase3");
            phase3 = true;
            speed = 1f;
            attackRange = 8f;
}
        else if (bossHealth <= 0)
        {
            phase3  = false;
            Debug.Log("isDead");
            isDead = true;
        }

        timer += Time.deltaTime;
    }

    public void ProjectileShoot()
    {
        if (timer > waitingTime)
        {
            if (phase2)
            {//clone and create our projectile
                GameObject clone = Instantiate(projectile2, shotLocation.position, Quaternion.identity);
                //reset our timer
               timer = 0f;
            }
            else if (phase3)
            {
                GameObject clone = Instantiate(projectile2, shotLocation.position, Quaternion.identity);
                timer = 0f;
            }
        }
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z = -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }

}
