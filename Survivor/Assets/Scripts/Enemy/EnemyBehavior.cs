using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : ScriptableObject
{
    public abstract void Init(Enemy enemy);    //�ʱ⼳��
    public abstract void Behaive(Enemy enemy);  //�ൿ�ϱ�
}
