using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Behaviors/BeChase")] 
public class BeChase : EnemyBehavior
{
    public string m_targetString;

    private GameObject m_target;

    public override void Init(Enemy enemy)
    {
        m_target = GameObject.FindGameObjectWithTag(m_targetString);
    }


    public override void Behaive(Enemy enemy)
    {
        //움직이기
        Vector3 moveVec;
        moveVec = m_target.transform.position - enemy.transform.position;
        enemy.m_lookVec = moveVec.normalized;
        enemy.transform.position += moveVec.normalized * enemy.m_speed * Time.deltaTime;

        Animator anim = enemy.m_animator;
        if (anim == null)
            return;
        anim.SetBool("isMoving", moveVec != Vector3.zero);
    }
}
