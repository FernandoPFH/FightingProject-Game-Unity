using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    [Header("Punch")]
    [SerializeField] GameObject punchHitbox;
    [SerializeField] float punchDelay = 0.1f;
    [SerializeField] private AudioSource _audioSource_Punch;
    float? punchLastTime = 0.0f;
    float? punchTime;
    [Header("Kick")]
    [SerializeField] GameObject kickHitbox;
    [SerializeField] float kickDelay = 0.1f;
    [SerializeField] private AudioSource _audioSource_Kick;
    float? kickLastTime = 0.0f;
    float? kickTime;
    [Header("Power")]
    [SerializeField] GameObject basicPowerPrefab;
    [SerializeField] float basicPowerDelay = 3f;
    [SerializeField] private AudioSource _audioSource_Power;
    [Header("Animator")]
    [SerializeField] Animator _animator;
    [Header("Direction")]
    [SerializeField] bool _isRight = true;
    [Header("Keys")]
    [SerializeField] KeyCode _punchKey = KeyCode.N;
    [SerializeField] KeyCode _kickKey = KeyCode.M;
    [SerializeField] KeyCode _powerKey = KeyCode.H;
    float? _basicPowerCounter;

    // Update is called once per frame
    void Update()
    {
        basicAttacks();

        powerAttacks();
    }

    void basicAttacks()
    {
        if (Input.GetKeyDown(_punchKey) && punchTime == null && punchLastTime > punchDelay)
        {
            punchLastTime = 0f;
            punchTime = 0f;
            punchHitbox.SetActive(true);
            _animator.SetTrigger("Punch");
            _audioSource_Punch.Play();
        }

        if (Input.GetKeyDown(_kickKey) && kickLastTime > kickDelay)
        {
            kickLastTime = 0f;
            kickTime = 0f;
            kickHitbox.SetActive(true);
            _animator.SetTrigger("Kick");
            _audioSource_Kick.Play();
        }

        if (punchTime != null)
        {
            punchTime += Time.deltaTime;

            if (punchTime > 0.1f)
            {
                punchHitbox.SetActive(false);
                punchTime = null;
            }
        }

        if (kickTime != null)
        {
            kickTime += Time.deltaTime;

            if (kickTime > 0.1f)
            {
                kickHitbox.SetActive(false);
                kickTime = null;
            }
        }

        punchLastTime += Time.deltaTime;
        kickLastTime += Time.deltaTime;
    }

    void powerAttacks()
    {
        if (Input.GetKeyDown(_powerKey) && _basicPowerCounter == null)
        {
            _basicPowerCounter = 0f;
            _animator.SetTrigger("Magic");
            _audioSource_Power.Play();
            PowerBasicMovement power = Instantiate(basicPowerPrefab, transform.position + transform.right * .6f + transform.up * .5f, Quaternion.identity).GetComponent<PowerBasicMovement>();
            power.isRight = _isRight;
            power.gameObject.layer = gameObject.layer;
        }

        if (_basicPowerCounter != null)
        {
            _basicPowerCounter += Time.deltaTime;

            if (_basicPowerCounter > basicPowerDelay)
            {
                _basicPowerCounter = null;
            }
        }
    }

    public void Disable()
    {
        this.enabled = false;
        punchHitbox.SetActive(false);
        kickHitbox.SetActive(false);
    }
}