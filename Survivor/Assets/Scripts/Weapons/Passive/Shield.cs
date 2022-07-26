using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Weapon
{
    public float m_additionalDefense;
    private float m_percentage; 

    public override void LevelUp()
    {
        Debug.Log("Shield Level Up");

    }

    private void Awake()
    {
        m_weaponName = "Shield";
        m_iconImage = Resources.Load<Sprite>("Images/Icon Shield");

    }
}
