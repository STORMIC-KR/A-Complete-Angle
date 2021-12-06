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
    void Start()
    {
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

        HelpText();
    }

    public void HelpPanelOn()
    {
        helpPanel.SetActive(true);
    }

    public void HelpText()
    {
        if(Input.GetKeyDown(KeyCode.F1))
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
