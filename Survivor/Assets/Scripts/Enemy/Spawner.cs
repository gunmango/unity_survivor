using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    Queue<Enemy> m_enemyQueue = new Queue<Enemy>();


    public float m_delayTime;
    public bool m_isDelay;

    private float m_diagDistance = 62; //플레이어와 화면모서리 거리

    void Start()
    {

        for (int i = 0; i < 10; i++)
        {
            Enemy enemy = Instantiate(Resources.Load<Enemy>("GreenEnemy"));
            enemy.backNum = i;
            enemy.gameObject.SetActive(false);

            m_enemyQueue.Enqueue(enemy);
            //Debug.Log("여긴가1");
        }
    }

    void Update()
    {
        if(!m_isDelay)
        {
            Spawn();
            m_isDelay = true;
            StartCoroutine(SpawnDelay());
        }
    }

    void Spawn()
    {
        if (m_enemyQueue.Count <= 0)
            return;
        Enemy enemy = m_enemyQueue.Dequeue();
        //Debug.Log("여긴가2");

        enemy.SetLocation(setRandPosition());
        //Debug.Log("number: " + enemy.backNum);
        enemy.gameObject.SetActive(true);
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(m_delayTime);
        m_isDelay = false;
    }

    private Vector3 setRandPosition()
    {
        Vector3 randVec;
        randVec.y = 0;
        

        float randomX = Random.Range(-m_diagDistance, m_diagDistance);   //랜덤 x 좌표
        float randomSign = Random.Range(-1f, 1f) < 0 ? -1 : 1;          //z좌표에 쓸 랜덤 - or +
        

        //Debug.Log("랜덤숫자: " + random);
        //Debug.Log("랜덤싸인: " + randomSign);

        randVec.x = transform.position.x + randomX;
        randVec.z = transform.position.z + (randomSign * Mathf.Sqrt(Mathf.Pow(m_diagDistance,2) - Mathf.Pow(randomX,2)));

        //Debug.Log("z축: " + randVec.z);

        return randVec;
    }

    public void Reload(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        m_enemyQueue.Enqueue(enemy);
    }
}
