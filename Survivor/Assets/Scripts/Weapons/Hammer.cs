using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public GameObject m_player;
    private Transform m_spinnerTransform;
    [SerializeField] private float m_angle;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_shrinkSpeed;
    public int m_damage { get; set; }
    public float m_knockBackForce { get; set; }

    [SerializeField] public float m_spinningTime { get; set; }
    [SerializeField] public float m_delayTime { get; set; }

    public bool m_isDelay { get; set; }

    [SerializeField] public bool m_isSpinning { get; set; }
    public GameObject m_mesh;

    public enum HammerState
    {
        Spinning,
        Shrinking,
        Delaying,
    }

    [SerializeField] public HammerState m_hammerState { get; set; }


    void Awake()
    {
        m_spinnerTransform = gameObject.GetComponentInParent<Transform>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        
        m_angle = 7;
        m_speed = 10;
        m_shrinkSpeed = 2f;
        //Vector3 vector3 = new Vector3(0, 4, -4);
        //transform.position = vector3;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Transform>().RotateAround(m_spinnerTransform.position, new Vector3(0, 1, 0), m_angle * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Transform>().RotateAround(m_player.transform.position, new Vector3(0, 1, 0), m_angle * m_speed * Time.deltaTime);

    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }


    public IEnumerator SpinCoroutine()
    {
        m_isDelay = false;
        //돌다가
        yield return new WaitForSeconds(m_spinningTime);
        //작아지기시작
        StartCoroutine(ShrinkCoroutine());
    }


    public IEnumerator ShrinkCoroutine()
    {
        while(true)
        {
            //작아지다
            Vector3 newScale = new Vector3(transform.localScale.x - m_shrinkSpeed * Time.deltaTime, transform.localScale.y - m_shrinkSpeed * Time.deltaTime, transform.localScale.z - m_shrinkSpeed * Time.deltaTime);
            
            //너무작아지면
            if (newScale.z <= 0.01)
            {
                //무기꺼
                StartCoroutine(DelayCoroutine());
                break;
            }
            else
            {
                transform.localScale = newScale;
            }
            yield return null;
        }
    }

    IEnumerator DelayCoroutine()
    {
        //다꺼
        GetComponent<BoxCollider>().enabled = false;    //맞게 한건지 확인필요
        m_mesh.SetActive(false);
        m_isDelay = true;

        //기다렸다
        yield return new WaitForSeconds(m_delayTime);

        //딜레이끝나면 다시 원래대로
        GetComponent<BoxCollider>().enabled = true;
        m_mesh.SetActive(true);
        Vector3 scale = new Vector3(1, 1, 1);
        transform.localScale = scale;
        StartCoroutine(SpinCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.Damage(m_damage, m_knockBackForce);
        }
    }



}
