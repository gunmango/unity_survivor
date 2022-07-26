using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveWeapon : Weapon
{
    private void Awake()
    {
        m_damage = 0;
        m_delayTime = 0;
        m_knockBackForce = 0;
        m_isDelay = false;
        m_level = 0;
    }

    public override void LevelUp()
    {

    }
}
