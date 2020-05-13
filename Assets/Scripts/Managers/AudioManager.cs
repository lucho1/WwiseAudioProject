using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public float MusicVol;
    public float MasterVol;

    public GameObject MusiclvlGO;
    AudioSource Musiclvl;

    // Start is called before the first frame update
    void Start()
    {
        Musiclvl = MusiclvlGO.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMusicVol(float volume)
    {


        MusicVol = volume;
        Musiclvl.volume = MusicVol;

    }

    public void UpdateMasterVol(float volume)
    {
        MasterVol = volume;
        UpdateGameObjectsVol();
    }
    

    void UpdateGameObjectsVol()
    {

        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        foreach (GameObject x in rootObjects)
        {
            if (x.GetComponent<AudioSource>() !=null)
            {
                x.GetComponent<AudioSource>().volume = MasterVol;

            }


        }

        rootObjects.Clear();

    }

    

}
