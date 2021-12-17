using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject helpPanel;
    public Text deviceText;
    public GameObject mobileController;

    Color helpTextColor;

    public string deviceType;

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
