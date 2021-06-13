using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button[] LevelsButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < LevelsButtons.Length; i++)
        {
            if (LevelManager.isLevelOpened[i] == false)
                LevelsButtons[i].interactable = false;
            else
                LevelsButtons[i].interactable = true;
        }
    }

    public void StartLevel1()
    {
        LevelManager.currentLevel = 1;
        SceneManager.LoadScene("Level1");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
