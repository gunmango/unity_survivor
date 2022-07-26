using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talisman : Weapon
{
    public float m_additionalLuck;
    private float m_percentage;

    public override void LevelUp()
    {
        Debug.Log("Talisman Level Up");

    }

    private void Awake()
    {
        m_weaponName = "Talisman";
        m_iconImage = Resources.Load<Sprite>("Images/Icon Talisman");
        m_additionalLuck = 10;
        m_percentage = 10;
        m_levelUpDescription = "Character gets " + m_percentage + "luckier";
    }
}
