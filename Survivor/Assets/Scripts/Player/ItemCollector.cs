using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    CapsuleCollider m_campsuleCollider;

    private void Awake()
    {
        m_campsuleCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickUpable pickupable = other.GetComponent<IPickUpable>();
        if (pickupable != null)
        {
            pickupable.PickUp();
        }
    }

    private void LevelUp()
    {
    }

}
