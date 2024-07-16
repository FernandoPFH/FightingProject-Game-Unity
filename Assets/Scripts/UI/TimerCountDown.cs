using System.Collections;
using TMPro;
using UnityEngine;

public class TimerCountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private Coroutine coroutine;

    static public TimerCountDown instance;

    void Awake()
    {
        instance = this;
    }

    public void StartContDown()
    {
        coroutine = StartCoroutine(CountDown());
    }

    public void StopContDown()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator CountDown()
    {
        int count = int.Parse(timerText.text);

        while (count != 0)
        {
            yield return new WaitForSeconds(1f);
            timerText.text = (--count).ToString();
        }

        StopContDown();

        GameController.instance.FinishFight();
    }
}
