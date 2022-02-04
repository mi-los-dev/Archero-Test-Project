using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour, IGun
{
    [SerializeField] private float rechargeTime = 0.25f;
    [SerializeField] [Range(0, 100)] private int damage;
    private float currentRechargeTime;
    private BulletManager bulletManager;

    private void Awake()
    {
        bulletManager = FindObjectOfType<BulletManager>();
        currentRechargeTime = Random.Range(0, rechargeTime);
    }

    public bool TryShot(GameObject shooter, Vector3 start ,Vector3 forward)
    {
        if (currentRechargeTime > 0) return false;
        
        var bullet = bulletManager.GetBullet();
        var b = bullet.GetComponent<Bullet>();
        b.shooter = shooter;
        b.damage = damage;
        b.CanShotThroughObstacles = false;
        bullet.transform.position = start;
        bullet.transform.forward = forward;
        
        bullet = bulletManager.GetBullet();
        b = bullet.GetComponent<Bullet>();
        b.shooter = shooter;
        b.damage = damage;
        b.CanShotThroughObstacles = false;
        bullet.transform.position = start;
        bullet.transform.forward = forward + transform.right * 0.25f;
        
        bullet = bulletManager.GetBullet();
        b = bullet.GetComponent<Bullet>();
        b.shooter = shooter;
        b.damage = damage;
        b.CanShotThroughObstacles = false;
        bullet.transform.position = start;
        bullet.transform.forward = forward + -transform.right * 0.25f;
        
        currentRechargeTime = rechargeTime;

        return true;
    }
    
    public void Update()
    {
        if (currentRechargeTime >= 0)
        {
            currentRechargeTime -= Time.deltaTime;
        }
    }
}
