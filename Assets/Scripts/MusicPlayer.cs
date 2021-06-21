using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip forestMusic;
    [SerializeField]
    private AudioClip treeMusic;
    [SerializeField]
    private AudioSource _audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        SetMusic(LevelManager.currentLevel);
    }

    public void SetMusic(int song)
    {
        AudioClip currentClip = song < 4 ? forestMusic : treeMusic;
        if (_audioSource.clip != currentClip)
        {
            _audioSource.Stop();
            _audioSource.clip = currentClip;
            _audioSource.Play();
        }
    }
}
