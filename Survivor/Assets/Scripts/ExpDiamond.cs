using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDiamond : MonoBehaviour, IPickUpable
{
    [SerializeField] private float m_rotateSpeed = 10.0f;
    private float m_speed;
    public int m_expPoint;
    GameObject m_target;

    private void Awake()
    {
        //GetComponent<MeshRenderer>().materials[0].
        m_target = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        m_speed = 12f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 3) * m_rotateSpeed * Time.deltaTime);
    }


    public void Initialize(int expPoint, Vector3 pos)
    {
        m_expPoint = expPoint;
        gameObject.transform.position = pos;

    }

    IEnumerator ToPlayer()
    {
        while(true)
        {
            //움직이기
            Vector3 moveVec;
            moveVec = m_target.transform.position - transform.position;
            transform.position += moveVec.normalized * m_speed * Time.deltaTime;
            m_speed += 0.2f;

            yield return null;
        }
    }

    public void PickUp()
    {
        StartCoroutine(ToPlayer());
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StopCoroutine(ToPlayer());
            //리스트에 다시넣어야함
            ExpManager.Instance.Reload(this);
        }
    }




}
