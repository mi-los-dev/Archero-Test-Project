using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : Warrior
{
    [SerializeField] private UnityEvent PlayerWined;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private FloatingJoystick joystick;

    protected override IEnumerator UpdateLifeLoop()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            var x = joystick.Horizontal;
            var z = joystick.Vertical;
            rigidbody.velocity = new Vector3(x, 0, z).normalized * moveSpeed;

            if (x == 0 && z == 0)
            {
                animator.SetBool("IsRun", false);
                var currentEnemy = enemyManager.GetNearestEnemy(transform.position);
                if (currentEnemy == null) continue;
            
                var direction = (currentEnemy.transform.position - transform.position).normalized;
                direction.y = 0f;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed);
            
                var heading = currentEnemy.transform.position - transform.position;
                var distance = heading.magnitude;
                direction = heading / distance;
                var startPos = transform.position;
                startPos.y += 1.25f;
            
                var canShot = gun.TryShot(gameObject, startPos, direction);
                if (canShot) animator.SetTrigger("Shot");
            }
            else
            {
                animator.SetBool("IsRun", true);
                var angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
    }
    
    protected new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag(Tags.FINISH))
        {
            PlayerWined.Invoke();
        }
        if (other.gameObject.CompareTag(Tags.COIN))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.ENEMY))
        {
            Life -= 10;
        }
    }
}
