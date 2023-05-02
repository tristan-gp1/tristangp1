using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chase : StateMachineBehaviour
{
    //Creates a place to store the players transform information
    Transform player;
    //Creates a place to store the rigidbody
    Rigidbody2D rb;
    //Create a stroage location that will hold our boss behavior script
    BossBehavior bossBehavior;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Sets the refernce for our players location
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Set reference for my rigidbody
        rb = animator.GetComponent<Rigidbody2D>();

        //Set our reference so we can call our functions
        bossBehavior = animator.GetComponent<BossBehavior>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossBehavior.LookAtPlayer();
        //Finds the player as target and locks the boss on the y axis
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        //Sends our boss to move towards its target
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, bossBehavior.speed * Time.deltaTime);
        //Tell our rb to move to the newPos
        rb.MovePosition(newPos);

        //Find the distance between the boss and player
        float distance = Vector2.Distance(player.position, rb.position);

        //Phase 1
        if (distance < bossBehavior.attackRange && !bossBehavior.phase2 && !bossBehavior.phase3 && !bossBehavior.isDead) 
        {
            animator.SetTrigger("MeleeAttack");
        }
        //Phase 2
        else if (distance < bossBehavior.attackRange && bossBehavior.phase2 && !bossBehavior.phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("Phase2Attack");
        }
        //Phase 3
        else if (distance < bossBehavior.attackRange && !bossBehavior.phase2 && bossBehavior.phase3 && !bossBehavior.isDead)
        {
            animator.SetTrigger("Phase3Attack");
        }
        //Boss defeated
        else if (bossBehavior.isDead)
        {
            animator.SetTrigger("Death");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
