using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject helpText;
    public GameObject helpPanel;

    Color helpTextColor;

    public string deviceType;

    void Start()
    {
        Screen.SetResolution(2048, 1536, true);
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            deviceType = "DeskTop";
        }

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

        if(helpText != null)
        {
            helpText.SetActive(false);
            helpTextColor = helpText.GetComponent<Text>().color;
            helpTextColor = new Color(1,1,1,1);
        }
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

        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                CtrlHelpPanel();
            }
        }

        StartGameWithKey();

        HelpText();
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
            if(deviceType == "DeskTop")
            {
                SceneManager.LoadScene("Game");
            }
            else if(deviceType == "HandHeld")
            {
                SceneManager.LoadScene("");
            }
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

    public void HelpText()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(helpText != null)
            {
                if(SceneManager.GetActiveScene().buildIndex != 0)
                {
                    if(helpText.activeSelf == true)
                    {
                        helpText.SetActive(false);
                    }
                    else
                    {
                        helpText.SetActive(true);
                    }
                }
            }
        }
    }
}
