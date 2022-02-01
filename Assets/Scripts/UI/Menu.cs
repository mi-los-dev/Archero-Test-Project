using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public UnityEvent GameStarted;
    public UnityEvent GamePaused;
    public UnityEvent GameContinued;
    [SerializeField] private Text countdownView3;
    [SerializeField] private Text countdownView2;
    [SerializeField] private Text countdownView1;
    [SerializeField] private Text countdownView0;
    [SerializeField] private Transform playButton;
    private Image panel;
    private bool isPause = false;

    private void Awake()
    {
        panel = GetComponent<Image>();
    }

    public void PlayButtonClick()
    {
        countdownView3.transform.DOLocalMoveY(0, 1);
        playButton.DOLocalMoveY(-1000, 1);
        StartCoroutine(Countdown());
        StartCoroutine(MakeTransparentForPanel());
    }

    public void PauseButtonClick()
    {
        GamePaused.Invoke();
        
        isPause = true;
        panel.enabled = true;
        
        var c = panel.color;
        c.a = 0.5f;
        panel.color = c;
        
        countdownView0.transform.localPosition = new Vector3(0, 1000, 0);
        countdownView1.transform.localPosition = new Vector3(0, 1000, 0);
        countdownView2.transform.localPosition = new Vector3(0, 1000, 0);
        countdownView3.transform.localPosition = new Vector3(0, 1000, 0);
        playButton.transform.localPosition = new Vector3(0, 1000, 0);
        playButton.DOLocalMoveY(0, 1);
    }

    private IEnumerator MakeTransparentForPanel()
    {
        yield return new WaitForSeconds(1);
        var currentTime = 3f;
        var c = panel.color;
        while (currentTime > 0)
        {
            c.a = Mathf.Lerp(0, 0.5f, currentTime / 3);
            panel.color = c;
            currentTime -= Time.deltaTime;
            yield return null;
        }
        panel.enabled = false;
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        countdownView3.transform.DOLocalMoveY(-1000, 1);
        countdownView2.transform.DOLocalMoveY(0, 1);
        yield return new WaitForSeconds(1);
        countdownView2.transform.DOLocalMoveY(-1000, 1);
        countdownView1.transform.DOLocalMoveY(0, 1);
        yield return new WaitForSeconds(1);
        countdownView1.transform.DOLocalMoveY(-1000, 1);
        countdownView0.transform.DOLocalMoveY(0, 1);
        yield return new WaitForSeconds(1);
        countdownView0.transform.DOLocalMoveY(-1000, 1);

        if (isPause)
        {
            GameContinued.Invoke();
            isPause = false;
        }
        else
        {
            GameStarted.Invoke();
        }
    }
}
