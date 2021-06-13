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

    }

    public void StartGame()
    {
        LevelManager.currentLevel = 1;
        SceneManager.LoadScene("Level1");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
