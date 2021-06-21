using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public GameObject WinPanel;

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
        LevelManager.currentLevel++;
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPlayer>().SetMusic(LevelManager.currentLevel);
        SceneManager.LoadScene($"Level{LevelManager.currentLevel}");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
