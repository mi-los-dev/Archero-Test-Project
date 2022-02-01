using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InfantrymanEnemy : Enemy
{
    private NavMeshAgent agent;

    private new void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }
    
    protected override IEnumerator Move(float time)
    {
        agent.speed = moveSpeed;
        
        var t = 0f;
        while (!IsDead && !player.IsDead && t < time && Vector3.Distance(transform.position, playerTransform.position) > 3f)
        {
            t += Time.deltaTime;
            animator.SetBool("IsRun", true);
            agent.SetDestination(playerTransform.position);
            yield return new WaitForFixedUpdate();
        }

        agent.speed = 0;
    }
}
