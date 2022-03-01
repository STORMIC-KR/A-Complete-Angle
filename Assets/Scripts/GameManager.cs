using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DiscordPresence;

public class GameManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject helpPanel;
    public GameObject gameOverPanel;

    [Header("Texts")]
    public Text versionText;
    public Text endingKillText;
    public Text endingWaveText;

    [Header("Discord")]
    string detail;
    string state;

    [Header("Scripts")]
    public Player playerScript;
    public WaveSpawnSystem waveScript;

    void Start()
    {
        Screen.SetResolution(2048, 1536, true);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(helpPanel != null)
            {
                if(SceneManager.GetActiveScene().buildIndex == 0)
                {
                    if(helpPanel.activeSelf == false)
                    {
                        Application.Quit();
                    }
                }

                if(helpPanel.activeSelf == true)
                {
                    helpPanel.SetActive(false);
                }
            }
        }

        if(gameOverPanel != null)
        {
            if(playerScript.cur_playerHealth <= 0)
            {
                Invoke("FinishGame", 0.1f);
            }
        }

        StartGameWithKey();
        CtrlHelpPanel();
        CtrlVersionText();
        ManageDiscordPresence();

        if(endingKillText != null && endingWaveText != null)
        {
            endingKillText.text = "Kill : " + playerScript.killEnemyCount;        
            endingWaveText.text = "Wave : " + waveScript.waveNum;
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

    public void StartGameWithKey()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene("Game_InfiniteMode");
            }
        }
    }

    public void StartGame()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("Game_InfiniteMode");
        }
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void FinishGame()
    {
        gameOverPanel.SetActive(true);
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
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                if(helpPanel != null)
                {
                    if(helpPanel.activeSelf == false)
                    {
                        helpPanel.SetActive(true);
                    }
                    else
                    {
                        helpPanel.SetActive(false);
                    }
                }
            }
        }
    }
}
