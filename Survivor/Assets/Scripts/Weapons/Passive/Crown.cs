using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : Weapon
{
    public float m_additionalExp;

    public override void LevelUp()
    {
        Debug.Log("Crown Level Up");
        m_level++;

        switch(m_level)
        {
            case 1:
                break;
            case 2:
                break;

        }

    }

    private void Awake()
    {
        m_weaponName = "Crown";
        m_additionalExp = 5;
        m_iconImage = Resources.Load<Sprite>("Images/Icon Crown");
        m_level = 0;
        m_levelUpDescription = "Charater gains "+m_additionalExp+ "% more experience";
    }
}
