using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, IPickUpable
{
    protected Player m_target;

    protected float m_speed;
    
    public abstract void Activate();

    public abstract void PickUp();

    [SerializeField] int m_probability;   //»ÌÈúÈ®·ü
    public int GetProbability()
    {
        return m_probability;
    }


    protected IEnumerator ToPlayer()
    {
        while (true)
        {
            //¿òÁ÷ÀÌ±â
            Vector3 moveVec;
            moveVec = m_target.transform.position - transform.position;
            transform.position += moveVec.normalized * m_speed * Time.deltaTime;
            m_speed += 0.2f;

            yield return null;
        }
    }

    public void Initialize(Vector3 pos)
    {
        gameObject.transform.position = pos;

    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopCoroutine(ToPlayer());
            Activate();
            Destroy(gameObject);
        }
    }
}
