using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : ScriptableObject
{
    public abstract void Init(Enemy enemy);    //초기설정
    public abstract void Behaive(Enemy enemy);  //행동하기
}
