using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    [SerializeField] private float damage = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.Hit(damage);
        }
    }
}
