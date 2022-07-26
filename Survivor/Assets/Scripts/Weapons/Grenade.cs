using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody m_rigidbody;  
    [SerializeField] private int m_damage;
    [SerializeField] private Vector3 m_direction;
    private float m_speed;
    private GrenadeThrower m_thrower;
    private float m_knockBackForce;
    private float m_turnSpeed;
    [SerializeField] private float m_radius;     

    public ParticleSystem m_explosionEffect;
    public GameObject m_meshObj;

    private void Awake()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_thrower = GameObject.FindObjectOfType<GrenadeThrower>();
        m_turnSpeed = 10;
        m_radius = 13f;
    }

    private void OnEnable()
    {
        m_meshObj.SetActive(true);

        m_explosionEffect.gameObject.SetActive(false);

    }

    public void Throw(int damage, float knockBackForce,Vector3 position, Vector3 direction)
    {
        m_damage = damage;
        m_knockBackForce = knockBackForce;
        transform.position = position;
        m_direction = direction;

        m_rigidbody.AddForce(direction, ForceMode.Impulse);
        m_rigidbody.AddTorque(Vector3.back * m_turnSpeed, ForceMode.Impulse);

        StartCoroutine(Explode());
        StartCoroutine(AfterExplosion());

    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(3.2f);
        //∏ÿ√Á≥ı∞Ì
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = Vector3.zero;
        m_meshObj.SetActive(false);
        m_explosionEffect.gameObject.SetActive(true);

        m_explosionEffect.transform.localScale = new Vector3(m_radius * 0.1f, m_radius * 0.1f, m_radius * 0.1f);

        DamageEnemy();

    }

    IEnumerator AfterExplosion()
    {
        yield return new WaitForSeconds(5.1f);
        m_explosionEffect.Play();
        m_explosionEffect.gameObject.SetActive(false);
        m_thrower.Reload(this);

    }

    private void DamageEnemy()
    {
        RaycastHit[] raycastHits =
            Physics.SphereCastAll(transform.position, m_radius, Vector3.up, 0f);
        for(int i=0; i<raycastHits.Length; i++)
        {
            IDamagable damagable = raycastHits[i].collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.Damage(m_damage, m_knockBackForce);
            }
        }
    }

}
