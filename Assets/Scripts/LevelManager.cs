using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    static int numberOfLevels = 7;
    public static bool[] isLevelOpened = { true, false, false, false, false, false, false };
    public static int currentLevel = 1;
    public static int state = 0;

    public static void Win()
    {
        isLevelOpened[currentLevel] = true;
        state = 1;
        SceneManager.LoadScene("LevelUI");
    }

    public static void GameOver()
    {
        state = -1;
        SceneManager.LoadScene("LevelUI");
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
    }
}
