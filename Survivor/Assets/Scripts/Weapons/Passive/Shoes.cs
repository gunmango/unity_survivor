using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : Weapon
{
    public float m_additionalSpeed;
    private float m_percentage;
    public override void LevelUp()
    {
        Debug.Log("Shoes Level Up");

    }

    private void Awake()
    {
        m_weaponName = "Shoes";
        m_iconImage = Resources.Load<Sprite>("Images/Icon Shoes");
        m_additionalSpeed = 10;
        m_percentage = 10;
        m_levelUpDescription = "Character moves " + m_percentage + "% faster.";

    }
}
