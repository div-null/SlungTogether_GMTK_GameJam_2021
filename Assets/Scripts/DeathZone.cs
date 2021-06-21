using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    AudioSource loseSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FreezedBall" || collision.gameObject.tag == "SwingingBall")
        {
            StartCoroutine(LoseSequence());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loseSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator LoseSequence()
    {
        Time.timeScale = 0;
        loseSound.PlayOneShot(loseSound.clip);
        yield return new WaitForSecondsRealtime(loseSound.clip.length - 1.5f);
        Time.timeScale = 1;
        LevelManager.ReloadScene();
    }
}
