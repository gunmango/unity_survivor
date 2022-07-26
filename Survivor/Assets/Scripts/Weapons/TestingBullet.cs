using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingBullet : MonoBehaviour
{
    private Rigidbody m_rigidbody;  //쓸모없을지도
    private int m_damage;
    private float m_speed;
    private Vector3 m_direction;
    private Shooter m_shooter;
    private float m_knockBackForce;
    
    private float m_diagDistance = 66; //플레이어와 화면모서리 거리

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_shooter = GameObject.FindObjectOfType<Shooter>();
        //if (m_shooter != null)
        //    Debug.Log("not null");
    }

    // Update is called once per frame
    void Update()
    {
        //이동
        transform.position += m_direction * m_speed * Time.deltaTime;

        //화면밖을 벗어나면
        float distance = Mathf.Sqrt((Mathf.Pow((transform.position.x - m_shooter.transform.position.x), 2)) + Mathf.Pow((transform.position.z - m_shooter.transform.position.z), 2));
        if(distance > m_diagDistance)
        {
            m_shooter.Reload(this);
        }

    }

    public void Fire(int damage, float speed, float knockBackForce, Vector3 pos, Vector3 direction)
    {
        m_damage = damage;
        m_speed = speed;
        transform.position = pos;
        m_knockBackForce = knockBackForce;
        m_direction = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if(damagable != null)
        {
            damagable.Damage(m_damage, m_knockBackForce);
            m_shooter.Reload(this);
        }
    }
}
