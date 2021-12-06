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


    #region InGame
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
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
    #endregion
    
    void Start()
    {
        Screen.SetResolution(2048, 1536, true);

        if(helpText != null)
        {
            helpText.SetActive(false);
            helpTextColor = helpText.GetComponent<Text>().color;
            helpTextColor = new Color(1,1,1,1);
        }
    }
    void Update()
    {
        QuitGame();

        if(Input.GetKeyDown(KeyCode.F1))
        {
            CtrlHelpPanel();
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if(helpText != null)
            {
                if(Input.GetKeyDown(KeyCode.F1))
                {
                    HelpText();
                }
            }
        }
    }

    public void QuitGame()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    public void CtrlHelpPanel()
    {
        if(helpPanel.activeSelf == true)
        {
            helpPanel.SetActive(false);
        }
        else
        {
            helpPanel.SetActive(true);
        }
    }

    public void HelpText()
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
