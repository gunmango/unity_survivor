using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour, IDamagable
{
    [SerializeField] private int m_maxItemCount;
    private ItemBoxSpawner m_itemBoxSpawner;

    Item[] m_items = new Item[5];   //�ٲٱ�
    
    void Awake()
    {
        m_items[0] = Resources.Load<ItemCoin>("Coin Gold");
        m_items[1] = Resources.Load<ItemCoin>("Coin Silver");
        m_items[2] = Resources.Load<ItemHeart>("Item Heart");
        m_items[3] = Resources.Load<ItemMagnet>("Magnet_1");
        m_items[4] = Resources.Load<ItemCoin>("Coin Bronze");
    }

    //������
    public void Damage(float damageAmount, float knockBackForce)    //�¾�����
    {
        //������ذ����� ���ư�
        m_itemBoxSpawner.Reload(this);


        DropItem();

    }

    //������ ��ڽ�
    public void DropItem()
    {
        //ItemHeart itemHeart = Instantiate(Resources.Load<ItemHeart>("Item Heart"));
        //itemHeart.Initialize(transform.position);


        //ItemMagnet itemMagnet = Instantiate(Resources.Load<ItemMagnet>("Magnet_1"));
        //itemMagnet.Initialize(transform.position);

        Item item = Instantiate(m_items[RandomSelect()]);
        item.Initialize(transform.position);

    }

    //
    public void Initialize(Vector3 pos, ItemBoxSpawner itemBoxSpawner)
    {
        transform.position = pos;
        m_itemBoxSpawner = itemBoxSpawner;
    }



    //����Ȯ��
    private int RandomSelect()
    {
        int rand = Random.Range(0, 100);
        int cumulative = 0;

        for(int i=0; i<m_items.Length;i++)
        {
            cumulative += m_items[i].GetProbability();

            if(rand <= cumulative)
            {
                return m_items.Length - i;
            }
        }

        return 0;
    }


}
