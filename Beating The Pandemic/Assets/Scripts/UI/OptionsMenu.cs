using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    private AudioSource audioSource;

    bool mute = false;
    float initialVolume;

    public static bool isClick = true;

    private GameObject musicGameObject;

    void Start()
    {
        musicGameObject = GameObject.FindGameObjectWithTag("music");
        audioSource = musicGameObject.GetComponent<AudioSource>();
        initialVolume = audioSource.volume;
        
    }

    void LateUpdate()
    {
        //Muting the audio if volume is 0. Otherwise, unmute.
        if(mute)
            audioSource.volume = 0;

        else
            audioSource.volume = initialVolume;

    }


    public void SetVolume(float volume)
    {
        if (volume == 0)
            mute = true;
        else
            mute = false;

        audioMixer.SetFloat("Volume", volume);
        
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetShootOption(bool flag)
    {
        isClick = flag;
    }


    /*public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }*/
}
