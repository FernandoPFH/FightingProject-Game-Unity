using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{
    [Header("Punch")]
    [SerializeField] GameObject punchHitbox;
    float? punchTime;
    [Header("Kick")]
    [SerializeField] GameObject kickHitbox;
    float? kickTime;
    [Header("Power")]
    [SerializeField] GameObject basicPowerPrefab;
    [SerializeField] float basicPowerDelay = 3f;
    float? _basicPowerCounter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        basicAttacks();

        powerAttacks();
    }

    void basicAttacks()
    {
        if (Input.GetKeyDown(KeyCode.N) && punchTime == null)
        {
            punchTime = 0f;
            punchHitbox.SetActive(true);
        }


        if (Input.GetKeyDown(KeyCode.M) && kickTime == null)
        {
            kickTime = 0f;
            kickHitbox.SetActive(true);
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
    }

    void powerAttacks()
    {
        if (Input.GetKeyDown(KeyCode.H) && _basicPowerCounter == null)
        {
            _basicPowerCounter = 0f;
            Instantiate(basicPowerPrefab, transform.position + Vector3.right * 1f, Quaternion.identity);
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
}