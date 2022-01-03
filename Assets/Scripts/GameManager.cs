using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DiscordPresence;

public class GameManager : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject mobileController;
    public GameObject gameOverPanel;

    public Text deviceText;
    public Text versionText;
    public Text endingKillText;
    public Text endingWaveText;

    string detail;
    string state;

    string deviceType;

    public Player playerScript;
    public WaveSpawnSystem waveScript;

    void Start()
    {
        Screen.SetResolution(2048, 1536, true);

        switch(SystemInfo.deviceType)
        {
            case DeviceType.Desktop:
                deviceType = "DeskTop";
                break;
            case DeviceType.Console:
                deviceType = "Console";
                break;
            case DeviceType.Handheld:
                deviceType = "Handheld";
                break;
            case DeviceType.Unknown:
                deviceType = "Unknown";
                break;
        }

        if(deviceText != null)
        {
            deviceText.text = "Your Device Type : " + deviceType;
        }
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            PresenceManager.UpdatePresence(detail: "In The Title", state: "Playing");
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            PresenceManager.UpdatePresence(detail: "In Game", state: "Playing");
        }
        
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

        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                CtrlHelpPanel();
            }
        }
        
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(SystemInfo.deviceType == DeviceType.Desktop)
            {
                mobileController.SetActive(false);
            }
            else if(SystemInfo.deviceType == DeviceType.Handheld)
            {
                mobileController.SetActive(true);
            }
        }

        StartGameWithKey();
        if(versionText != null)
        {
            versionText.text = "Version : " + Application.version;
        }

        if(playerScript.cur_playerHealth <= 0)
        {
            gameOverPanel.SetActive(true);
        }

        endingKillText.text = "Kill : " + playerScript.killEnemyCount;        
        endingWaveText.text = "Wave : " + waveScript.waveNum;
    }

    public void StartGameWithKey()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void StartGame()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("Game");
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

    public void CtrlHelpPanel()
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
