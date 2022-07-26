using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCube : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject m_target;
    [SerializeField] float m_speed;
    void Start()
    {
       m_target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 moveVec;
       moveVec = m_target.transform.position - transform.position;
       transform.position += moveVec.normalized * m_speed * Time.deltaTime;
    }

    public void SetLocation(Vector3 pos)
    {
        transform.position = pos;
    }
}
