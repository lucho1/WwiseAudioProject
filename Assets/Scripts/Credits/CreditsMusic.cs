using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CreditsMusic : MonoBehaviour
{
    AudioSource Credit_music;
    public AudioClip LoopClip;
    

    // Start is called before the first frame update
    void Start()
    {
        Credit_music = gameObject.GetComponent<AudioSource>();
        Debug.Log("Scene loaded");
    }

    // Update is called once per frame
    void Update()
    {

        if (Credit_music.isPlaying == false)
        {
            Credit_music.clip = LoopClip;
            Credit_music.loop = true;
            Credit_music.Play();

            
        }



    }

    //void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    //{

    //    Debug.Log("On Scene loaded:"+scene.name);
    //    Debug.Log("Mode:"+mode);

    //}



}
