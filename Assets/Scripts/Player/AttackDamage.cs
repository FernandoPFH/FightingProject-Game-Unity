using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private bool destroyOnImpact = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.Hit(damage);
        }

        if (destroyOnImpact)
            Destroy(gameObject);
    }
}
