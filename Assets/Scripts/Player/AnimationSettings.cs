using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class AnimationSettings : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float BattleCrySpeed = 0.7f;
    [SerializeField] float runningSpeedMultiplier;
    [SerializeField] GameObject rollTarget;

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
        float runningSpeedToAnimator = agent.velocity.magnitude * runningSpeedMultiplier;
        animator.SetFloat("SpeedForAnimation", runningSpeedToAnimator);
        animator.SetFloat("ActionSpeed", BattleCrySpeed);
        BattleCry();
        Punch();
        //Roll();
    }

    private void BattleCry()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Battlecry");
            agent.ResetPath();
        }
    }

    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            animator.SetTrigger("Roll");
            agent.ResetPath();
            agent.Move(rollTarget.transform.position);
        }
    }

    private void Punch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Punch");
            agent.ResetPath();
        }
    }
}
