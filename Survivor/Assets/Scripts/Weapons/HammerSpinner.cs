using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSpinner : Weapon
{
    [SerializeField] private Hammer[] m_hammerList;
    private int m_totalHammerCount;
    [SerializeField] private int m_currentHammerCount;

    private float m_spinningTime; //도는시간
    private float m_spinningSpeed;

    [SerializeField] private float m_hammerY;

    private float m_radius;


    private void Awake()
    {
        m_weaponName = "Hammer";
        m_level = 1;
        m_damage = 5;
        m_knockBackForce = 10;
        m_delayTime = 5f;
        m_spinningTime = 5f;
        m_totalHammerCount = 5;
        m_currentHammerCount = 0;
        m_hammerY = 3f;
        m_isDelay = false;
        m_radius = 4f;
        m_iconImage = Resources.Load<Sprite>("Images/Icon Hammer");

        m_hammerList = new Hammer[5];

        for(int i=0; i < m_totalHammerCount; i++)
        {
            Hammer hammer = Instantiate(Resources.Load<Hammer>("Hammer"));
            hammer.transform.parent = this.transform;
            Vector3 hammerPos = new Vector3(0, i, -4);
            hammer.SetPosition(hammerPos);
            hammer.m_spinningTime = m_spinningTime;
            hammer.m_delayTime = m_delayTime;
            hammer.m_damage = m_damage;
            hammer.m_knockBackForce = m_knockBackForce;
        
            m_hammerList[i] = hammer;
            hammer.gameObject.SetActive(false);
        }

    }

    void AddHammer()
    {
        m_currentHammerCount++;

        if(m_currentHammerCount == 1)
        {
            m_hammerList[m_currentHammerCount - 1].gameObject.SetActive(true);

            Vector3 hammerPos = new Vector3(transform.position.x, m_hammerY, transform.position.z - 4);

            m_hammerList[m_currentHammerCount - 1].SetPosition(hammerPos);
            m_hammerList[m_currentHammerCount - 1].StartCoroutine(m_hammerList[m_currentHammerCount - 1].SpinCoroutine());
        }
        else
        {
            StartCoroutine(AddHammerCoroutine());
        }

    }

    IEnumerator AddHammerCoroutine()
    {
        //쉬는중이 아니면
        while (m_hammerList[0].m_isDelay == false)
        { 
            //무한루프
            yield return null;
        }

        //쉬는중이면
        //액티브키고
        m_hammerList[m_currentHammerCount - 1].gameObject.SetActive(true);

        for (int i =0; i<m_currentHammerCount; i++)
        {
            //위치조정
            m_hammerList[i].SetPosition(GetNewPos(i));
            m_hammerList[i].StopAllCoroutines();
            //다같이 다시시작
            m_hammerList[i].StartCoroutine(m_hammerList[i].SpinCoroutine());
        }
        


    }







    private void Update()
    {
        //랩업 나중에 바꾸기
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddHammer();
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
         //   m_hammerList[0].StopAllCoroutines();
        }
    }


    //망치늘어날때 위치조정
    private Vector3 GetNewPos(int num)
    {
        Vector3 newPos = new Vector3();
        newPos.y = m_hammerY;

        float angle;
        angle = (Mathf.PI * 2) / m_currentHammerCount * num;

        newPos.x = transform.position.x + Mathf.Cos(angle) * m_radius;
        newPos.y = transform.position.y + Mathf.Sin(angle) * m_radius;

        return newPos;
    }


    public override void LevelUp()
    {
        m_level++;
        AddHammer();
    }

}
