using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : MonoBehaviour
{
    AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        winSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "SwingingBall")
        {
            //LevelManager.Win();
            StartCoroutine(WinSequence());
        }
    }

    private IEnumerator WinSequence()
    {
        Time.timeScale = 0;
        winSound.PlayOneShot(winSound.clip);
        yield return new WaitForSecondsRealtime(winSound.clip.length - 1.6f);
        Time.timeScale = 1;
        if (LevelManager.currentLevel == 7)
        {
            LevelManager.FinalScreen();
        }
        else
            LevelManager.Win();
    }
}
