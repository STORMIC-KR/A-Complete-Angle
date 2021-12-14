using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTimer : MonoBehaviour
{
    public Text timerText;
    public GameObject player;
    public float time;
    private float selectCountDown;

    void Start()
    {
        selectCountDown = time;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Floor(selectCountDown) <= 0)
        {
            timerText.gameObject.SetActive(false);
            player.GetComponent<Player>().enabled = true;
        }
        else
        {
            player.GetComponent<Player>().enabled = false;
            selectCountDown -= Time.deltaTime;
            timerText.text = "Game will start in...\n" + Mathf.Floor(selectCountDown).ToString();
        }
    }
}
