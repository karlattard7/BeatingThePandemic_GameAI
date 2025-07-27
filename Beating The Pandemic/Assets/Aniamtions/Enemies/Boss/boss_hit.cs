using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class boss_hit : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private AIPath aiPath;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aiPath = animator.GetComponentInParent<AIPath>();
        if(!animator.GetBool("enraged"))
            aiPath.canMove = false;
        
    }


  
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("enraged"))
            aiPath.canMove = true;
        
    }
}
