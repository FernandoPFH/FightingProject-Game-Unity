using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Animator _animator;
    [SerializeField] private VisualEffect _visualEffect_Blood;
    [SerializeField] private VisualEffect _visualEffect_Block;
    [SerializeField] private KeyCode _defenseKey = KeyCode.A;
    public float health { get; private set; } = 100f;
    private float _maxHealth = 100f;
    private bool _isDefending = false;

    void Update()
    {
        if (Input.GetKeyDown(_defenseKey))
            _isDefending = true;

        if (Input.GetKeyUp(_defenseKey))
            _isDefending = false;
    }

    void UpdateHealthBar()
    {
        float relativeHealth = health / _maxHealth;

        healthBar.fillAmount = relativeHealth;
    }

    public void Hit(float damage)
    {
        if (health == 0)
            return;

        if (_isDefending)
        {
            _animator.SetTrigger("Block");
            _visualEffect_Block.Play();
            return;
        }

        health = Mathf.Clamp(health - damage, 0, _maxHealth);

        _animator.SetTrigger("Hit");
        _visualEffect_Blood.Play();

        UpdateHealthBar();

        if (health == 0)
            GameController.instance.FinishFight();
    }

    public void Disable()
    {
        this.enabled = false;
    }
}