using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class boss_walk : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    public AIPath aIPath;
    public float offset;
    public bossBehaviour bossBH;
    public float attackSpeed = 1f;
    public bool waitOnattack;

    private float nextAttack = -1;

    public float startEndDistance;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aIPath = animator.GetComponentInParent<AIPath>();
        if (!aIPath)
            Debug.LogError("no aiPath found!");
        bossBH = animator.GetComponentInParent<bossBehaviour>();
        startEndDistance = bossBH.startEndD;

        //Debug.Log("start!");
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossBH.nextAttackIsRanged())        
            aIPath.endReachedDistance = 6;        
        else        
            aIPath.endReachedDistance = startEndDistance;
        


        if (aIPath.reachedEndOfPath)
        {
            if (waitOnattack && nextAttack == -1)
                nextAttack = Time.time + attackSpeed;
            else if (Time.time >= nextAttack)
            {
                animator.GetComponentInParent<bossBehaviour>().attack();
                nextAttack = Time.time + attackSpeed;
            }

        }
        else
        {
            Vector3 desiredPos = new Vector3(aIPath.desiredVelocity.x, aIPath.desiredVelocity.y, 0f);
            //Vector3 diff =  desiredPos - animator.transform.position;        
            Vector3 diff = desiredPos;
            float angle = Mathf.Atan2(diff.x, diff.y) * Mathf.Rad2Deg;
            aIPath.gameObject.transform.rotation = Quaternion.Euler(0f, angle + offset, 0f);
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
