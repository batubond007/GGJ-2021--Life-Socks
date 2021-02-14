using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaBehaviour : StateMachineBehaviour
{
    Vector3 startPosition;
    GameObject umbrella;
    GameObject parent;
    Vector3 ParentPos1, ParentPos2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var hand in animator.GetComponentsInChildren<Transform>(true))
            if (hand.CompareTag("Glider"))
            {
                startPosition = hand.position;
                umbrella = hand.gameObject;
                umbrella.SetActive(true);
                parent = umbrella.GetComponentInParent<HandDefiner>().gameObject;
                ParentPos1 = parent.transform.localPosition;
                ParentPos1.x = .434f;
                ParentPos2 = parent.transform.localPosition;
                ParentPos2.x = -.434f;
            }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<SpriteRenderer>().flipX)
            parent.transform.localPosition = ParentPos2;
        else
            parent.transform.localPosition = ParentPos1;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var hand in animator.GetComponentsInChildren<Transform>(true))
            if (hand.CompareTag("Glider"))
            {
                umbrella.transform.position = startPosition;
                umbrella.SetActive(false);
            }
                
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
