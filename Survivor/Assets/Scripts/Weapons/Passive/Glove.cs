using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : Weapon
{

    public int m_additionalDamage;

    public override void LevelUp()
    {
        Debug.Log("Glove Level Up");
    
    }

    private void Awake()
    {
        m_weaponName = "Glove";
        m_additionalDamage = 10;
        m_iconImage = Resources.Load<Sprite>("Images/Icon Glove");
        m_levelUpDescription = "Raises inflicted damage by " + m_additionalDamage + "%";
    }


}
