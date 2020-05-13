using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMEMUSIC_TYPES { NONE, EVIL_HEAD };

public class MusicMngr : MonoBehaviour
{

    public AudioClip EvilHeadClip = null;

    GAMEMUSIC_TYPES current_music_type = GAMEMUSIC_TYPES.NONE;
    AudioSource AmbientSoundGObj = null;
    AudioSource CurrentSoundGObj = null;    
    AudioClip NextSoundGObj = null;

    bool fading = false;
    bool fadingIn = true;
    bool fadingOut = false;
    bool musicChange = false;

    // Start is called before the first frame update
    void Start()
    {
        AmbientSoundGObj = GameObject.Find("AmbienceSound").GetComponent<AudioSource>();
        CurrentSoundGObj = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fading)
            HandleFade();

        if(musicChange && !CurrentSoundGObj.isPlaying) //Faltaría hacer que el ambience baje/suba
        {
            CurrentSoundGObj.clip = NextSoundGObj;
            CurrentSoundGObj.loop = true;
            CurrentSoundGObj.Play();
            musicChange = false;
        }
    }

    public void ChangeSceneMusic(GAMEMUSIC_TYPES musicType)
    {
        switch(musicType)
        {
            case GAMEMUSIC_TYPES.EVIL_HEAD:
                NextSoundGObj = EvilHeadClip;
                current_music_type = musicType;
                break;
            case GAMEMUSIC_TYPES.NONE:
                NextSoundGObj = null;
                current_music_type = GAMEMUSIC_TYPES.NONE;
                break;
            default:
                NextSoundGObj = null;
                current_music_type = GAMEMUSIC_TYPES.NONE;
                break;
        }

        musicChange = true;
    }

    void HandleFade()
    {
        if(fadingIn)
        {
            FadeIn(CurrentSoundGObj, 2.0f, 1.0f, fadingIn);
            if(!fadingIn) fadingOut = true;
        }

        if(fadingOut) FadeOut(CurrentSoundGObj, 2.0f, fadingOut);
        if(!fadingIn && !fadingOut) fading = false;
    }

    public void FadeOut(AudioSource audioSource, float FadeTime, bool fadeBool)
    {
        if (audioSource.volume > 0)
            audioSource.volume -= Time.deltaTime / FadeTime;
        else
            fadeBool = false;
    }

    public void FadeIn(AudioSource audioSource, float duration, float targetVolume, bool fadeBool)
    {
        if (audioSource.volume < targetVolume)
            audioSource.volume += Time.deltaTime/ duration;
        else
            fadeBool = false;
    }
}
