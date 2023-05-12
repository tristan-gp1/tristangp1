using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chase : StateMachineBehaviour
{
    public float distance = 2f;
    
    //create a box to store our players position
    Transform player;

    //create a box to store our bosses rigidbody;
    Rigidbody2D rb;

    //create a storage location that will hold our boss behavior script
    BossBehavior bossBehavior;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //sets the referance for player
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //sets the referance for our boss rb
        rb = animator.GetComponent<Rigidbody2D>();

        //set our referances do we can call our functions
        bossBehavior = animator.GetComponent<BossBehavior>();
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossBehavior.LookAtPlayer();
        //declaring and setting the player to the target for our boss, locking the y axis
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        //Sets a new position for our boss to move towards
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, bossBehavior.speed * Time.deltaTime);
        //tell our rb to move to the newPos
        rb.MovePosition(newPos);
        
        float distance = Vector2.Distance(player.position, rb.position);
        if (distance < bossBehavior.attackRange && !bossBehavior.phase2 && !bossBehavior.phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("MeleeAttack");
        }
        else if (distance < bossBehavior.attackRange && !bossBehavior.phase2 && !bossBehavior.phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("Phase1Attack");
        }
        else if (distance < bossBehavior.attackRange && !bossBehavior.phase2 && !bossBehavior.phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("Phase2Attack");
        }
        else if (bossBehavior.isDead)
        {
            animator.SetTrigger("isDead");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
   
  
}
