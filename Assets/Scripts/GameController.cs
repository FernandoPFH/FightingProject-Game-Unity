using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TimelineAsset winScreen;
    [SerializeField] private TextMeshProUGUI winText;
    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    static public GameController instance;

    void Awake()
    {
        instance = this;
    }

    public void PlayersPoseForFight()
    {
        player1.GetComponent<PlayerControll>().PoseForFight();
        player2.GetComponent<PlayerControll>().PoseForFight();
    }

    public void StartFight()
    {
        TimerCountDown.instance.StartContDown();

        player1.GetComponent<PlayerControll>().StartFight();
        player2.GetComponent<PlayerControll>().StartFight();
    }

    void PlayWinScreen()
    {
        director.playableAsset = winScreen;
        director.RebuildGraph();
        director.time = 0.0;
        director.Play();
    }

    public void FinishFight()
    {
        TimerCountDown.instance.StopContDown();

        PlayerHealth player1_health = player1.GetComponent<PlayerHealth>();
        PlayerHealth player2_health = player2.GetComponent<PlayerHealth>();

        GameObject winner;
        GameObject loser;

        if (player1_health.health > player2_health.health)
        {
            winner = player1;
            loser = player2;
        }
        else if (player2_health.health > player1_health.health)
        {
            winner = player2;
            loser = player1;
        }
        else
        {
            winText.text = $"Draw!!!";
            PlayWinScreen();
            return;
        }

        winText.text = $"{winner.name} Wins!!!";
        PlayWinScreen();
        winner.GetComponent<PlayerControll>().Win();
        loser.GetComponent<PlayerControll>().Lose();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
