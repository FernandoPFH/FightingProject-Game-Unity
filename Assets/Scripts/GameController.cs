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

    static public GameController instance;

    void Awake()
    {
        instance = this;
    }
    public void FinishFight(string winner)
    {
        winText.text = $"{winner} Wins!!!";

        director.playableAsset = winScreen;
        director.RebuildGraph();
        director.time = 0.0;
        director.Play();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
}
