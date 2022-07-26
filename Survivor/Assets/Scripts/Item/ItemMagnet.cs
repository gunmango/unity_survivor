using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : Item
{
    [SerializeField] private float m_rotateSpeed = 10.0f;


    private void Awake()
    {
        m_target = GameObject.FindObjectOfType<Player>();

    }

    private void OnEnable()
    {
        m_speed = 12f;
    }

    public override void PickUp()
    {
        StartCoroutine(ToPlayer());

    }

    public override void Activate()
    {
        //throw new System.NotImplementedException();
        //Debug.Log("Activate");
        ExpManager.Instance.OnMagnetActivate();
    }



    private void FixedUpdate()
    {
        //ºù±Ûºù±Û
        gameObject.transform.Rotate(new Vector3(0, 3, 0) * m_rotateSpeed * Time.deltaTime);
    }


}
