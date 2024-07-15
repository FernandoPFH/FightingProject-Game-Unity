using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class MenuController : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TimelineAsset keyMenu;

    public void StartMenuKeys()
    {
        director.playableAsset = keyMenu;
        director.RebuildGraph();
        director.time = 0.0;
        director.Play();
    }

    public void StartFightScene()
    {
        SceneManager.LoadScene(1);
    }
}