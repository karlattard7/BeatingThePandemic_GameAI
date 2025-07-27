using JetBrains.Annotations;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class boss_enraging : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private AIPath aiPath;
    public float speedMultiplier = 2f;
    public float newSpeed;
    public bool useMultiplier = true;
    public Texture enragedMaterial;

    public float riseByAmount = 0f;
    public float riseSpeed = 0.01f;
    private Vector3 startPos;
    private Quaternion startRot;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("enraged", true);
        aiPath = animator.GetComponentInParent<AIPath>();
        aiPath.canMove = false;
        if(useMultiplier)
            aiPath.maxSpeed *= speedMultiplier;
        else
            aiPath.maxSpeed = newSpeed;
        GameObject bossH = GameObject.FindGameObjectWithTag("boss_head");
        bossH.GetComponent<Renderer>().material.mainTexture = enragedMaterial;

        
        startPos = animator.transform.position;
        startRot = animator.transform.rotation;
        animator.transform.rotation = Quaternion.Euler(0, -180, 0);


        GameObject.FindGameObjectWithTag("directionalText").GetComponent<directionalTextUpdater>().setColor(Color.red);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.transform.position.y - startPos.y < riseByAmount)
        {
            animator.transform.position = new Vector3(animator.transform.position.x, animator.transform.position.y + (riseSpeed * Time.deltaTime), animator.transform.position.z);
        }        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = startPos;
        animator.transform.rotation = startRot;


        //animator.transform.rotation = Quaternion.Euler(0, -90, 0);
        //aiPath.desiredVelocity = new Vector3(0, 0, 0);


        aiPath.canMove = true;
        animator.ResetTrigger("hit");
        animator.ResetTrigger("attack");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("potionThrow");

        animator.GetComponentInParent<bossBehaviour>().startTextFlash();
        
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
