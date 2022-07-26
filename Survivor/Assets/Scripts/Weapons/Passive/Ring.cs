using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Weapon
{
    public float m_additionalRange;

    public override void LevelUp()
    {
        Debug.Log("Ring Level Up");
    }
    private void Awake()
    {
        m_weaponName = "Ring";
        m_iconImage = Resources.Load<Sprite>("Images/Icon Ring");
        m_additionalRange = 5;
        m_levelUpDescription = "Character pickups items from further away";
    }
}
