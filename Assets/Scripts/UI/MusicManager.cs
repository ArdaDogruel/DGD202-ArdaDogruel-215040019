using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicOnStart;
    
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Start()
    {
        Play(musicOnStart, true);
    }

    AudioClip switchTo; 

    public void Play(AudioClip music, bool intrupt = false)
    {
        if (intrupt == true) 
        {
            volume = 0.4f;
            audioSource.volume = volume;
            audioSource.clip = music;
            audioSource.Play();
        }
        else
        {
            switchTo = music;
            StartCoroutine(SmoothSwitchMusic());
        }
        

        
    }

    float volume;
    [SerializeField] float timeToSwitch;

    IEnumerator SmoothSwitchMusic()
    {
        volume = 1f;


        while (volume > 0f)
        {
            volume -= Time.deltaTime / timeToSwitch;
            if (volume < 0f) { volume  = 0f; }
            audioSource.volume = volume;
            yield return new WaitForEndOfFrame();
        }

        Play(switchTo, true);


    }

}
