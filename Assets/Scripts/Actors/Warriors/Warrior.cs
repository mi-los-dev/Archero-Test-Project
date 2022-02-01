using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Warrior : MonoBehaviour
{
    public UnityEvent<Warrior> Died;
    [SerializeField] [Range(0, 100)] private int life;
    private LifelineView lifelineView;
    protected IGun gun;
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected float rotateSpeed = 10;
    protected Animator animator;
    protected new Rigidbody rigidbody;
    public bool IsDead { get; private set; } = false;

    protected int Life
    {
        set
        {
            life = value;
            lifelineView.Refresh(life);
            if (life < 1)
            {
                StopAllCoroutines();
                moveSpeed = 0;
                animator.SetTrigger("Die");
                IsDead = true;
                rigidbody.isKinematic = true;
                lifelineView.Hide();
                GetComponent<CapsuleCollider>().enabled = false;
                Invoke("Destroy", 3f);
            }
        }
        get => life;
    }

    protected void Awake()
    {
        gun = GetComponentInChildren<IGun>();
        lifelineView = GetComponentInChildren<LifelineView>();
        lifelineView.Init(life);
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }
    
    public void StartFight()
    {
        StartCoroutine(UpdateLifeLoop());
    }
    
    public void PauseFight()
    {
        StopAllCoroutines();
    }
    
    protected virtual IEnumerator UpdateLifeLoop()
    {
        yield return null;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.BULLET))
        {
            var bullet = other.gameObject.GetComponent<Bullet>();

            if (bullet.shooter != gameObject)
            {
                Life -= bullet.damage;
            }
        }
    }

    private void Destroy()
    {
        Died.Invoke(this);
        GameObject.Destroy(gameObject);
    }
}
