using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Warrior
{
    [SerializeField] protected GameObject coinsPrefab;
    protected Transform playerTransform;
    protected Player player;

    protected new void Awake()
    {
        base.Awake();
        playerTransform = GameObject.Find("Player").transform;
        player = playerTransform.GetComponent<Player>();
    }

    protected void Start()
    {
        Died.AddListener(DropCoins);
    }

    protected override IEnumerator UpdateLifeLoop()
    {
        while (true)
        {
            yield return StartCoroutine(Shot(Random.Range(2.5f, 5f)));
            yield return StartCoroutine(Move(Random.Range(1f, 2.5f)));
        }
    }

    private void DropCoins(Warrior w)
    {
        var pos = transform.position;
        pos.y = 0.6f;
        Instantiate(coinsPrefab, pos, Quaternion.identity);
    }
    
    protected virtual IEnumerator Move(float time)
    {
        yield return null;
    }
    
    protected virtual IEnumerator Shot(float time)
    {
        var t = 0f;
        while (t < time)
        {
            animator.SetBool("IsRun", false);
            t += Time.deltaTime;
            
            yield return new WaitForFixedUpdate();
            if (playerTransform == null) continue;
            
            var direction = (playerTransform.transform.position - transform.position).normalized;
            direction.y = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed);
            
            var heading = playerTransform.transform.position - transform.position;
            var distance = heading.magnitude;
            direction = heading / distance;
            var startPos = transform.position;
            startPos.y += 1.25f;
            
            var canShot = gun.TryShot(gameObject, startPos, direction);
            if (canShot) animator.SetTrigger("Shot");
        }
    }
    
    
}
