using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{ 
    public float m_health;
    public float m_speed;
    public int m_AtkDamage;
    protected int m_expPoint;

    public List<EnemyBehavior> m_behaviors;

    protected Rigidbody m_rigidbody;
    protected float m_knockBackForce;  //�¾����� �˹��� ��
    protected EnemyState m_enemyState;
        
    public Animator m_animator { get; set; }

    public Vector3 m_lookVec { get; set; }
    public int backNum { get; set; }    //������



    public void SetLocation(Vector3 pos)
    {
        transform.position = pos;
    }

    public abstract void Damage(float damageAmount, float knockBackForce);    //�¾�����

    protected abstract void AfterDeath();   //�״¾ִϸ��̼ǳ�����

    protected abstract void Death();    //�׾�����

   
}
