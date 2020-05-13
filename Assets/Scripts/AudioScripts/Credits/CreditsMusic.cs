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
    bool fading_start = true;


    // Start is called before the first frame update
    void Start()
    {
        Credit_music = gameObject.GetComponent<AudioSource>();
        Debug.Log("Scene loaded");
    }

    // Update is called once per frame
    void Update()
    {
        if (fading_start)
        {
            StartCoroutine(FadeIn(Credit_music, 1.0f, 1.0f));
            fading_start = false;
        }


        if (Credit_music.isPlaying == false)
        {
            Credit_music.clip = LoopClip;
            Credit_music.loop = true;
            Credit_music.Play();            
        }

        if(Credit_music.clip != FadeOutClip && playFadeOut)
        {
            StartCoroutine(FadeOut(Credit_music, 0.5f));
            Credit_music.clip = FadeOutClip;
            StartCoroutine(FadeIn(Credit_music, 0.5f, 1.0f));
            Credit_music.loop = false;
            Credit_music.Play();
        }
    }

    //void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    //{

    //    Debug.Log("On Scene loaded:"+scene.name);
    //    Debug.Log("Mode:"+mode);

    //}
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        yield break;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
