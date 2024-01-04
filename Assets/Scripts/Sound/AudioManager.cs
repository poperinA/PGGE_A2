using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // to use sounds, use "AudioManager.Instance"
    // then call the playMusic, playSFX etc. functions,
    // followed by the specific sound clip you added
    // example: AudioManager.Instance.playSFX("WrongItem");

    private void Start()
    {
        playMusic("Theme");
    }

    public void playMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("sound not found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void playSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("sound not found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
