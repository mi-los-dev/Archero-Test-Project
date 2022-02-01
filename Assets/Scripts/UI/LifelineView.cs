using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifelineView : MonoBehaviour
{
    [SerializeField] private Image line;
    private int startlife;

    public void Init(int startlife)
    {
        this.startlife = startlife;
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Refresh(int currentlife)
    {
        var value = (float)currentlife / startlife;

        if (value >= 0.5f)
        {
            line.color = Color.Lerp(Color.yellow, Color.green, value - 0.5f);
        }
        else
        {
            line.color = Color.Lerp(Color.red, Color.yellow, value);
        }

        line.fillAmount = value;
    }
}
