using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : Weapon
{

    [SerializeField] private int m_grenadeNum;
    [SerializeField] private float m_throwSpeed;

    private int m_totalGrenadeCount = 6;

    private Queue<Grenade> m_grenadeList = new Queue<Grenade>();
    private Transform m_playerTransform;

    public float m_throwRadius;


    private void Awake()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        for (int i = 0; i < m_totalGrenadeCount; i++)
        {
            Grenade grenade = Instantiate(Resources.Load<Grenade>("Grenade"));
            grenade.gameObject.SetActive(false);
            m_grenadeList.Enqueue(grenade);
        }

        m_weaponName = "Grenade";
        m_delayTime = 2.5f;
        m_grenadeNum = 1;
        m_damage = 10;
        m_isDelay = false;
        m_knockBackForce = 10f;
        m_throwRadius = 10f;
        m_level = 0;
        m_iconImage = Resources.Load<Sprite>("Images/Icon Bomb");
    }

    void Update()
    {
        //µô·¹ÀÌÁßÀÌ ¾Æ´Ï°í ·¾0ÀÌ ¾Æ´Ò¶§ ´øÁ®
        if (!m_isDelay && m_level != 0)
        {
            for(int i=m_grenadeNum; i>0; i--)
            {
                Throw();
            }
            m_isDelay = true;
            StartCoroutine(ThrowDelay());
        }
    }

    IEnumerator ThrowDelay()
    {
        yield return new WaitForSeconds(m_delayTime);
        m_isDelay = false;
    }

    void Throw()
    {
        if (m_grenadeList.Count <= 0)
            return;
        Grenade grenade = m_grenadeList.Dequeue();
        grenade.gameObject.SetActive(true);
        grenade.Throw(m_damage, m_knockBackForce, transform.position, SetRandom());
    }

    public void Reload(Grenade grenade)
    {
        grenade.gameObject.SetActive(false);
        m_grenadeList.Enqueue(grenade);
    }



    private Vector3 SetRandom()
    {
        Vector3 randVec;
        randVec.y = 10;

        float randomX = Random.Range(-m_throwRadius, m_throwRadius);   //·£´ý x ÁÂÇ¥
        float randomSign = Random.Range(-1f, 1f) < 0 ? -1 : 1;          //zÁÂÇ¥¿¡ ¾µ ·£´ý - or +
       
        randVec.x = transform.position.x + randomX;
        randVec.z = transform.position.z + (randomSign * Mathf.Sqrt(Mathf.Pow(m_throwRadius, 2) - Mathf.Pow(randomX, 2)));

        randVec = randVec - transform.position;
        randVec.y = 10;
        
        return randVec;
    }

    public override void LevelUp()
    {
        m_level++;
        
        if(m_level ==2 || m_level ==4)
        {
            m_grenadeNum++;
        }
    }

}
