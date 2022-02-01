using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject effectPrefab;
    [HideInInspector] public GameObject shooter;
    public float speed = 5;
    public int damage = 5;
    [HideInInspector] public bool canFly = true;

    public bool CanShotThroughObstacles
    {
        get => canShotThroughObstacles;
        set
        {
            canShotThroughObstacles = value;
            meshRenderer.material =
                canShotThroughObstacles ? shotThroughObstaclesMaterial : notShotThroughObstaclesMaterial;
        }
    }
    private bool canShotThroughObstacles;
    [SerializeField] private Material shotThroughObstaclesMaterial;
    [SerializeField] private Material notShotThroughObstaclesMaterial;
    private Coroutine coroutine;
    private MeshRenderer meshRenderer;
    
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (!canFly) return;
        
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!shooter.Equals(other.gameObject) && !other.CompareTag(Tags.BULLET))
        {
            if (other.gameObject.CompareTag(Tags.OBSTACLE))
            {
                if (!CanShotThroughObstacles)
                {
                    Instantiate(effectPrefab, transform.position, Quaternion.identity);
                    gameObject.SetActive(false);
                }
            }
            else
            {
                Instantiate(effectPrefab, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }
}
