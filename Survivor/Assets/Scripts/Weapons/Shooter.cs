using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Weapon
{
    
    //private int m_projectileNum;
    private float m_projectileSpeed;

    private Queue<TestingBullet> m_bulletList = new Queue<TestingBullet>();

    public Player m_Player;


    void Awake()
    {
        for(int i=0; i<20; i++)
        {
            TestingBullet bullet = Instantiate(Resources.Load<TestingBullet>("TestingBullet"));
            bullet.gameObject.SetActive(false);
            m_bulletList.Enqueue(bullet);
        }
        m_weaponName = "Shooter";
        m_delayTime = 1.5f;
        //m_projectileNum = 1;
        m_damage = 5;
        m_projectileSpeed = 30f;
        m_isDelay = false;
        m_knockBackForce = 10f;
        m_iconImage = Resources.Load<Sprite>("Images/Icon Bullet2");

    }

    void Update()
    {
        if (!m_isDelay)
        {
            m_isDelay = true;
            StartCoroutine(ShootDelay());
            Shoot();
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(m_delayTime);
        m_isDelay = false;
    }

    void Shoot()
    {
        if (m_bulletList.Count <= 0)
            return;
        TestingBullet bullet = m_bulletList.Dequeue();
        bullet.Fire(m_damage, m_projectileSpeed, m_knockBackForce, transform.position,m_Player.m_playerLookVec);
        bullet.gameObject.SetActive(true);
    }

    public void Reload(TestingBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        m_bulletList.Enqueue(bullet);
    }



    public override void LevelUp()
    {
        m_level++;
    }
}
