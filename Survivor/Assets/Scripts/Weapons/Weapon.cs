using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string m_weaponName { get; set; }
    public Sprite m_iconImage { get; protected set; }
    public string m_levelUpDescription { get; protected set; }

    protected int m_level;
    protected int m_damage;
    protected float m_knockBackForce;
    protected float m_delayTime;
    protected bool m_isDelay;



    public abstract void LevelUp();
}
