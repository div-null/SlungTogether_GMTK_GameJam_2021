using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    static int numberOfLevels = 8;
    public static bool[] isLevelOpened = { true, false, false, false, false, false, false, false };
    public static int currentLevel = 1;
    public static int state = 0;

    public static void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public static void CompleteLevel()
    {
        isLevelOpened[currentLevel] = true;
        state = 1;
        
        SceneManager.LoadScene("CompletedLevel");
    }

    public static void FinalScreen()
    {
        SceneManager.LoadScene("FinalScreen");
    }
}
