using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    [Header("Punch")]
    [SerializeField] GameObject punchHitbox;
    [SerializeField] float punchDelay = 0.1f;
    float? punchLastTime = 0.0f;
    float? punchTime;
    [Header("Kick")]
    [SerializeField] GameObject kickHitbox;
    [SerializeField] float kickDelay = 0.1f;
    float? kickLastTime = 0.0f;
    float? kickTime;
    [Header("Power")]
    [SerializeField] GameObject basicPowerPrefab;
    [SerializeField] float basicPowerDelay = 3f;
    [Header("Animator")]
    [SerializeField] Animator _animator;
    float? _basicPowerCounter;

    // Update is called once per frame
    void Update()
    {
        basicAttacks();

        powerAttacks();
    }

    void basicAttacks()
    {
        if (Input.GetKeyDown(KeyCode.N) && punchTime == null && punchLastTime > punchDelay)
        {
            punchLastTime = 0f;
            punchTime = 0f;
            punchHitbox.SetActive(true);
            _animator.SetTrigger("Punch");
        }

        if (Input.GetKeyDown(KeyCode.M) && kickLastTime > kickDelay)
        {
            kickLastTime = 0f;
            kickTime = 0f;
            kickHitbox.SetActive(true);
            _animator.SetTrigger("Kick");
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
        if (Input.GetKeyDown(KeyCode.H) && _basicPowerCounter == null)
        {
            _basicPowerCounter = 0f;
            _animator.SetTrigger("Magic");
            Instantiate(basicPowerPrefab, transform.position + Vector3.right * .6f + Vector3.up * .5f, Quaternion.identity);
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