using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_attack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private AIPath aiPath;
    public float animationTimeoffset = 0f;
    private enemyMeleeWeaponBehaviour weap;
    private float timeleft;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiPath = animator.GetComponentInParent<AIPath>();
        aiPath.canMove = false;
        timeleft = animationTimeoffset;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        weap = animator.gameObject.transform.parent.GetComponentInChildren<enemyMeleeWeaponBehaviour>();
        //weap.use();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeleft <= 0 ) {
            weap.use();
            timeleft = animationTimeoffset;
        }
        else
            timeleft -= Time.deltaTime;
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiPath.canMove = true;
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
