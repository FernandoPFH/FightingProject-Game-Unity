using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _winCamera;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] PlayerMoves _playerMoves;
    [SerializeField] PlayerMoviment _playerMoviment;

    public void PoseForFight()
    {
        _animator.SetTrigger("FightPose");
    }

    public void StartFight()
    {
        _playerHealth.enabled = true;
        _playerMoves.enabled = true;
        _playerMoviment.enabled = true;
    }

    void DisableAll()
    {
        _playerHealth.Disable();
        _playerMoves.Disable();
        _playerMoviment.Disable();
    }

    public void Win()
    {
        DisableAll();
        _animator.SetTrigger("Victory");
        _winCamera.SetActive(true);
    }

    public void Lose()
    {
        DisableAll();
        _animator.SetTrigger("Die");
    }
}
