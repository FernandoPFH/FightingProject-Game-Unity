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
    [SerializeField] private Image damageBar;
    [SerializeField] private Animator _animator;
    [SerializeField] private VisualEffect _visualEffect_Blood;
    [SerializeField] private AudioSource _audioSource_Hit;
    [SerializeField] private VisualEffect _visualEffect_Block;
    [SerializeField] private AudioSource _audioSource_Block;
    [SerializeField] private KeyCode _defenseKey = KeyCode.A;
    public float health { get; private set; } = 100f;
    private float _maxHealth = 100f;
    private bool _isDefending = false;
    private float _fillMountLife = 1f;
    private float _fillMountDamage = 1f;
    private bool _drainDamage = false;
    private float _targetFillMountLife = 1f;
    private Coroutine _drainDamageCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(_defenseKey))
            _isDefending = true;

        if (Input.GetKeyUp(_defenseKey))
            _isDefending = false;

        if (_fillMountLife < _targetFillMountLife)
            _fillMountLife = _targetFillMountLife;
        else if (_fillMountLife > _targetFillMountLife)
            _fillMountLife -= 0.01f;

        healthBar.fillAmount = _fillMountLife;

        if (_drainDamage)
        {
            if (_fillMountDamage < _targetFillMountLife)
                _fillMountDamage = _targetFillMountLife;
            else if (_fillMountDamage > _targetFillMountLife)
                _fillMountDamage -= 0.02f;

            damageBar.fillAmount = _fillMountDamage;
        }
    }

    IEnumerator HideDamage()
    {
        _drainDamage = false;

        yield return new WaitForSeconds(1f);

        _drainDamage = true;
    }

    void UpdateHealthBar()
    {
        if (_drainDamageCoroutine != null)
            StopCoroutine(_drainDamageCoroutine);

        float relativeHealth = health / _maxHealth;

        if (relativeHealth != 0)
            _targetFillMountLife = relativeHealth;
        else
        {
            healthBar.fillAmount = relativeHealth;
            damageBar.fillAmount = relativeHealth;
        }

        _drainDamageCoroutine = StartCoroutine(HideDamage());
    }

    public void Hit(float damage)
    {
        if (health == 0)
            return;

        if (_isDefending)
        {
            _audioSource_Block.Play();
            _animator.SetTrigger("Block");
            _visualEffect_Block.Play();
            return;
        }

        health = Mathf.Clamp(health - damage, 0, _maxHealth);

        _audioSource_Hit.Play();
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