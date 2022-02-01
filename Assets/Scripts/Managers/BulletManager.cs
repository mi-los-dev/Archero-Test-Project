using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private List<GameObject> bullets = new List<GameObject>();

    public void PauseFlyAllBullet()
    {
        var activeBullets = bullets.FindAll(b => b.activeSelf);
        foreach (var bullet in activeBullets)
        {
            bullet.GetComponent<Bullet>().canFly = false;
        }
    }
    
    public void ContinueFlyAllBullet()
    {
        var activeBullets = bullets.FindAll(b => b.activeSelf);
        foreach (var bullet in activeBullets)
        {
            bullet.GetComponent<Bullet>().canFly = true;
        }
    }
    
    public GameObject GetBullet()
    {
        var disableBullets = bullets.FindAll(b => !b.activeSelf);
        if (disableBullets.Count == 0)
        {
            var bullet = Instantiate(bulletPrefab, transform);
            bullets.Add(bullet);
            return bullet;
        }
        else
        {
            var bullet = disableBullets.First();
            bullet.SetActive(true);
            return bullet;
        }
    }
}
