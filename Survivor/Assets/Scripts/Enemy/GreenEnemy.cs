using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemy : Enemy
{

    //private bool m_isDead;
    private Spawner m_spawner;

    //enemy에 집어넣을지 고려
    private float m_timer;
    private float m_hitTimeLimit;
    private bool m_onHit;


    private void Awake()
    {
        for (int i = 0; i < m_behaviors.Count; i++)
        {
            m_behaviors[i].Init(this);
        }
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponentInChildren<Animator>();
        m_spawner = GameObject.FindObjectOfType<Spawner>();
        m_enemyState = EnemyState.Chase;
        m_health = 10;
        m_AtkDamage = 10;
        m_expPoint = 50;
        m_lookVec = new Vector3(0, 0, 0);

        m_hitTimeLimit = 3;
        m_onHit = false;
        m_timer = 0;
    }

    private void OnEnable()
    {
        m_enemyState = EnemyState.Chase;
        m_health = 10;
    }

    void Update()
    {
        //맞았으면
        if (m_enemyState == EnemyState.Damaged)
            return;

        //죽었으면
        if (m_enemyState == EnemyState.Dead) 
        {
            //죽는 애니메이션 끝났으면
            if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                if (m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    AfterDeath();
                }
            }
            return; 
        }


        //살았으면
        for (int i = 0; i < m_behaviors.Count; i++)
        {
            m_behaviors[i].Behaive(this);
        }
 
    }
    private void FixedUpdate()
    {
        //가는방향으로 바라보게하기
        Quaternion newRotation = Quaternion.LookRotation(m_lookVec);
        m_rigidbody.rotation = Quaternion.Slerp(m_rigidbody.rotation, newRotation, 5f * Time.deltaTime);
    }

    protected override void Death()
    {
        m_animator.SetTrigger("isDead");
        m_enemyState = EnemyState.Dead;
    }

    protected override void AfterDeath()
    {
        //경험치 싸고
        ExpManager.Instance.DropExpDiamond(m_expPoint, gameObject.transform.position);

        //큐에 돌려넣고
        m_spawner.Reload(this);
    }

    public override void Damage(float damageAmount, float knockBackForce)
    {
        m_health -= damageAmount;

        if (m_health <= 0)
        {
            Death();
        }
        else
        {
            m_enemyState = EnemyState.Damaged;
            m_knockBackForce = knockBackForce;
            StartCoroutine(KnockBack());
        }
    }

    IEnumerator KnockBack()
    {
        while(m_knockBackForce >= 0)
        {
            m_rigidbody.AddForce(Vector3.back * m_knockBackForce * Time.deltaTime);
            m_knockBackForce -= 0.2f;
            yield return null;
        }
        m_enemyState = EnemyState.Chase;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            collision.gameObject.GetComponent<Player>().Damage(m_AtkDamage);
            m_onHit = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        
        m_timer += Time.deltaTime;
        if(m_timer > m_hitTimeLimit)
        {
            collision.gameObject.GetComponent<Player>().Damage(m_AtkDamage);
            m_timer = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_timer = 0;
        m_onHit = false;
    }
}
