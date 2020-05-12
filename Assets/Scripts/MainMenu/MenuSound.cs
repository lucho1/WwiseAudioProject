using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSound : MonoBehaviour
{
    AudioSource Main_menu_music;
    public AudioClip LoopClip;

    // Start is called before the first frame update
    void Start()
    {
        Main_menu_music = gameObject.GetComponent<AudioSource>();
    
    }

    // Update is called once per frame
    void Update()
    {

        if (Main_menu_music.isPlaying==false)
        {
            Main_menu_music.clip = LoopClip;
            Main_menu_music.loop = true;
            Main_menu_music.Play();


        }



    }
}
