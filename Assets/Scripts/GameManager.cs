using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DiscordPresence;

public class GameManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject gameMenuPanel;
    public GameObject helpPanel;
    public GameObject playerStats;

    [Header("Texts")]
    public Text versionText;
    public Text endingKillText;
    public Text endingWaveText;
    public Text startText;

    [Header("Discord")]
    string detail;
    string state;

    [Header("Scripts")]
    public Player playerScript;
    public WaveSpawnSystem waveScript;
    public StartTimer timerScript;

    void Start()
    {
        Screen.SetResolution(2048, 1536, true);
    }
    void Update()
    {        
        if(timerScript != null)
        {
            if(Mathf.Floor(timerScript.selectCountDown) > 0)
            {
                playerStats.SetActive(false);
            }
            else if(Mathf.Floor(timerScript.selectCountDown) <= 0)
            {
                playerStats.SetActive(true);
            }
        }

        if(endingKillText != null && endingWaveText != null)
        {
            endingKillText.text = "Kill : " + playerScript.killEnemyCount;        
            endingWaveText.text = "Wave : " + waveScript.waveNum;
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameMenuPanel.activeSelf == true)
            {
                gameMenuPanel.SetActive(false);
            }

            if(helpPanel.activeSelf == true)
            {
                helpPanel.SetActive(false);
            }
        }

        CtrlVersionText();
        ManageDiscordPresence();
    }

    public void TurnOnGameMenu()
    {
        if(gameMenuPanel != null)
        {
            gameMenuPanel.SetActive(true);
        }
    }

    public void StartInfinityGame()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("Game_WaveMode");
        }
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Title");
    }

    public void CtrlVersionText()
    {
        if(versionText != null)
        {
            versionText.text = "Version : " + Application.version;
        }
    }

    public void CtrlHelpPanel()
    {
        if(helpPanel != null)
        {
            helpPanel.SetActive(true);
        }
    }

    public void ManageDiscordPresence()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            PresenceManager.UpdatePresence(detail: "In The Title", state: "Playing");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            PresenceManager.UpdatePresence(detail: "In Game", state: "Playing");
        }
    }
}
