using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeart : Item
{
    [SerializeField] private int m_healAmount;
    [SerializeField] private float m_rotateSpeed = 10.0f;
 

    private void Awake()
    {
        //m_target = GameObject.FindGameObjectWithTag("Player");
        m_target = GameObject.FindObjectOfType<Player>();
        m_healAmount = 50;
    }

    private void OnEnable()
    {
        m_speed = 12f;
    }

    private void FixedUpdate()
    {
        //ºù±Ûºù±Û
        gameObject.transform.Rotate(new Vector3(0, 3,0) * m_rotateSpeed * Time.deltaTime);
    }

    public override void PickUp()
    {
        StartCoroutine(ToPlayer());

    }

    public override void Activate()
    {

        m_target.Heal(m_healAmount);
    }

}
