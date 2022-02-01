using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private UnityEvent allEnemiesDead;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemyQuantity;
    [SerializeField] private List<Transform> spawnPositions;
    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        for (var i = 0; i < enemyQuantity; i++)
        {
            if (spawnPositions.Count > 0)
            {
                var j = Random.Range(0, spawnPositions.Count);
                var p = spawnPositions[j];
                spawnPositions.RemoveAt(j);
                var e = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], p.position, Quaternion.Euler(0, 180, 0));
                e.GetComponent<Enemy>().Died.AddListener(EnemyDied);
                enemies.Add(e);
            }
        }
    }

    public void StartFight()
    {
        foreach (var e in enemies)
        {
            e.GetComponent<Enemy>().StartFight();
        }
    }
    
    public void PauseFight()
    {
        foreach (var e in enemies)
        {
            e.GetComponent<Enemy>().PauseFight();
        }
    }

    private void EnemyDied(Warrior enemy)
    {
        enemies.Remove(enemy.gameObject);
        if (enemies.Count < 1) allEnemiesDead.Invoke();
    }

    public GameObject GetNearestEnemy(Vector3 pos)
    {
        return enemies
            .Where(e => !e.GetComponent<Enemy>().IsDead)
            .OrderBy(e => Vector3.Distance(e.transform.position, pos))
            .FirstOrDefault();
    }
}
