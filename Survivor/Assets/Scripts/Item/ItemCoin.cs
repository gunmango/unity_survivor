using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCoin : Item
{
    [SerializeField] private int m_gold;
    [SerializeField] private float m_rotateSpeed = 11.0f;

    private void Awake()
    {
        //m_target = GameObject.FindGameObjectWithTag("Player");
        m_target = GameObject.FindObjectOfType<Player>();
        
    }

    private void OnEnable()
    {
        m_speed = 12f;
    }

    private void FixedUpdate()
    {
        //���ۺ���
        gameObject.transform.Rotate(new Vector3(0, 3, 0) * m_rotateSpeed * Time.deltaTime);
    }


    public override void Activate()
    {
        //uimanager �Ἥ ���ø���
    }

    public override void PickUp()
    {
        StartCoroutine(ToPlayer());
    }



}
