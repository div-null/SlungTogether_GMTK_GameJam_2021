using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public GameObject LosePanel;

    public GameObject WinPanel;

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
        LosePanel.SetActive(false);
    }

    public void ShowLosePanel()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
        LevelManager.currentLevel++;
        SceneManager.LoadScene($"Level{LevelManager.currentLevel}");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (LevelManager.state == 1)
            ShowWinPanel();
        else if (LevelManager.state == -1)
            ShowLosePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
