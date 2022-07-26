using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : Weapon
{
    private void Awake()
    {
        m_weaponName = "Test";
        m_iconImage = Resources.Load<Sprite>("Images/Icon Wand");
    }


    public override void LevelUp()
    {
        m_level++;
    }
}
