using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    //singleton instance to manage the audio
    public static AudioManager Instance;

    //arrays of sound clips for music and SFX
    public Sound[] musicSounds, sfxSounds;

    //audio sources for music and SFX
    public AudioSource musicSource, sfxSource;

    //ensure only one instance of AudioManager exists
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

    // to use sounds in scripts, use "AudioManager.Instance"
    // then call the playMusic, playSFX etc. functions,
    // followed by the specific sound clip you added
    // example: AudioManager.Instance.playSFX("WrongItem");

    private void Start()
    {
        //playMusic("Theme"); when there is bg song
    }

    //plays music by the specified music name
    public void playMusic(string name)
    {
        //finds the music name that the play input and its associated sound clip
        Sound s = Array.Find(musicSounds, x => x.name == name);

        //if the name/clip is not found, debug
        if (s == null)
        {
            Debug.Log("sound not found");
        }

        //plays the clip
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    //plays music by the specified SFX name
    public void playSFX(string name)
    {
        //finds the SFX name that the play input and its associated sound clip
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        //if the name/clip is not found, debug
        if (s == null)
        {
            Debug.Log("sound not found");
        }

        //plays the clip one shot
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
