using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    private float _health = 100f;
    private float _maxHealth = 100f;

    void UpdateHealthBar()
    {
        float relativeHealth = _health / _maxHealth;

        healthBar.fillAmount = relativeHealth;
    }

    public void Hit(float damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        UpdateHealthBar();
    }
}
