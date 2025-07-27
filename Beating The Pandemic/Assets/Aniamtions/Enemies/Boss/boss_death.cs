using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_death : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    AIPath aiPath;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //spawn a bunch of particles here
        if (animator)
            aiPath = animator.GetComponentInParent<AIPath>();
        else
            Debug.LogWarning("not found animator");
        if(aiPath)
            aiPath.canMove = aiPath.canSearch = false;
        else
            Debug.LogWarning("not found aiPath");
        animator.GetComponentInParent<Collider2D>().enabled = false;

        animator.transform.rotation = Quaternion.Euler(0, 90, 90);
        GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setText("");
        GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setColor(Color.white);
    }





    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
