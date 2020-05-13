using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CreditsMusic : MonoBehaviour
{
    AudioSource Credit_music;
    public AudioClip LoopClip;
    public AudioClip FadeOutClip;
    public bool playFadeOut = false;
    bool INIT_fading_start = true;
    bool INIT_fading_ended = false;

    // Start is called before the first frame update
    void Start()
    {
        Credit_music = gameObject.GetComponent<AudioSource>();
        Debug.Log("Scene loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (INIT_fading_start)
        {
            FadeIn(Credit_music, 1.0f, 1.0f, INIT_fading_ended);

            if (INIT_fading_ended)
            {
                INIT_fading_start = false;
                INIT_fading_ended = false;
            }            
        }

        if (Credit_music.isPlaying == false)
        {
            Credit_music.clip = LoopClip;
            Credit_music.loop = true;
            Credit_music.Play();            
        }

        if(Credit_music.clip != FadeOutClip && playFadeOut)
        {
            bool emptyBool = true;
            FadeOut(Credit_music, 0.2f, emptyBool);
            Credit_music.clip = FadeOutClip;
            Credit_music.loop = false;
            Credit_music.Play();
        }
    }

    public void FadeOut(AudioSource audioSource, float FadeTime, bool fadeBool)
    {
        if (audioSource.volume > 0)
            audioSource.volume -= Time.deltaTime / FadeTime;
        else
            fadeBool = true;
    }

    public void FadeIn(AudioSource audioSource, float duration, float targetVolume, bool fadeBool)
    {
        if (audioSource.volume < targetVolume)
            audioSource.volume += Time.deltaTime/ duration;
        else
            fadeBool = true;
    }
}
