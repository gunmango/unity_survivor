using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxSpawner : MonoBehaviour
{
    private int m_totalItemCount;
    [SerializeField] Item[] m_items;

    //
    Queue<ItemBox> m_itemBoxQueue = new Queue<ItemBox>();


    public float m_delayTime;
    public bool m_isDelay;

    private float m_diagDistance = 82; 

    void Awake()
    {
        m_totalItemCount = 20;
        m_items = new Item[m_totalItemCount];

        for (int i = 0; i < 10; i++)
        {
            ItemBox itemBox = Instantiate(Resources.Load<ItemBox>("ItemBox"));
            itemBox.gameObject.SetActive(false);

            m_itemBoxQueue.Enqueue(itemBox);
        }

    }

    void Update()
    {
        if (!m_isDelay)
        {
            Spawn();
            m_isDelay = true;
            StartCoroutine(SpawnDelay());
        }
    }

    void Spawn()
    {
        if (m_itemBoxQueue.Count <= 0)
            return;
        ItemBox itemBox = m_itemBoxQueue.Dequeue();

        itemBox.Initialize(setRandPosition(),this);
        itemBox.gameObject.SetActive(true);
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(m_delayTime);
        m_isDelay = false;
    }

    private Vector3 setRandPosition()
    {
        Vector3 randVec;
        randVec.y = 1.2f;


        float randomX = Random.Range(-m_diagDistance, m_diagDistance);   //·£´ý x ÁÂÇ¥
        float randomSign = Random.Range(-1f, 1f) < 0 ? -1 : 1;          //zÁÂÇ¥¿¡ ¾µ ·£´ý - or +

        randVec.x = transform.position.x + randomX;
        randVec.z = transform.position.z + (randomSign * Mathf.Sqrt(Mathf.Pow(m_diagDistance, 2) - Mathf.Pow(randomX, 2)));


        return randVec;
    }

    public void Reload(ItemBox itemBox)
    {
        itemBox.gameObject.SetActive(false);
        m_itemBoxQueue.Enqueue(itemBox);
    }
}
