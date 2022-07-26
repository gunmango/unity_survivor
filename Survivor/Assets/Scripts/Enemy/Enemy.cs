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
    protected float m_knockBackForce;  //맞았을때 넉백의 힘
    protected EnemyState m_enemyState;
        
    public Animator m_animator { get; set; }

    public Vector3 m_lookVec { get; set; }
    public int backNum { get; set; }    //디버깅용



    public void SetLocation(Vector3 pos)
    {
        transform.position = pos;
    }

    public abstract void Damage(float damageAmount, float knockBackForce);    //맞았을때

    protected abstract void AfterDeath();   //죽는애니메이션끝난후

    protected abstract void Death();    //죽었을때

   
}
