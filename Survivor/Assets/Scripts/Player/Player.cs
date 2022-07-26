using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private int m_level;

    private int m_maxHealth;
    private int m_health;

    public float m_speed;   //�̵��ӵ�
    public float m_rotateSpeed; //���ư��� �ӵ�
    float m_horizontal; //�Է°�
    float m_vertical;   //�Է°�
    Vector3 m_moveVec;

    Animator m_anim;

    Rigidbody m_rigidbody; //�÷��̾� ������ٵ�

    public int m_gold;

    public Vector3 m_playerLookVec { get; set; }    //���⿡�� �ٹ������

    // Start is called before the first frame update
    void Awake()
    {
        m_anim = GetComponentInChildren<Animator>();
        m_speed = 10;
        m_rotateSpeed = 5;
        m_maxHealth = 100;
        m_health = 100;

        m_level = 1;

        m_rigidbody = GetComponent<Rigidbody>();
        m_playerLookVec = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontal = Input.GetAxisRaw("Horizontal");
        m_vertical = Input.GetAxisRaw("Vertical");
        m_moveVec = new Vector3(m_horizontal, 0, m_vertical).normalized;
        transform.position += m_moveVec * m_speed * Time.deltaTime;
      
        m_anim.SetBool("isRun", m_moveVec != Vector3.zero);

        Turn();

        m_playerLookVec = transform.forward; //���Ⱑ�� ��������
    }

    void Turn()
    {
        if (m_horizontal == 0 && m_vertical == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(m_moveVec);

        m_rigidbody.rotation = Quaternion.Slerp(m_rigidbody.rotation, newRotation, m_rotateSpeed * Time.deltaTime);


    }


    public void Damage(int damage)
    {
        m_health -= damage;
        UIManager.Instance.SetHealth(m_maxHealth, m_health);
    }

    public void Heal(int healAmount)
    {
        m_health += healAmount;
        if (m_health > m_maxHealth)
            m_health = m_maxHealth;
        UIManager.Instance.SetHealth(m_maxHealth, m_health);
    }

}
