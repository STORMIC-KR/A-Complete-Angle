using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public Text timerText;
    public float time;
    private float selectCountDown;

    void Start()
    {
        selectCountDown = time;
    }

    void Update()
    {
        if(Mathf.Floor(selectCountDown) <= 0)
        {
            timerText.gameObject.SetActive(false);
        }
        else
        {
            selectCountDown -= Time.deltaTime;
            timerText.text = "Game will start in...\n" + Mathf.Floor(selectCountDown).ToString();
        }
    }
}
