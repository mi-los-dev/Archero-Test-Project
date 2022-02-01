using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemy : Enemy
{
    protected override IEnumerator Move(float time)
    {
        var t = 0f;
        while (!IsDead && !player.IsDead && t < time && Vector3.Distance(transform.position, playerTransform.position) > 3f)
        {
            t += Time.deltaTime;
            animator.SetBool("IsRun", true);
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            transform.LookAt(playerTransform);
            yield return new WaitForFixedUpdate();
        }
    }
}
