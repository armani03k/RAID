using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {

    public AudioSource VictorySource;
    public AudioSource BackgroundMusic;
    bool victory;
	// Use this for initialization
	void Start () {
        LevelEventHandler.StartListening("Victory", Victory);
        gameObject.SetActive(false);
	}
	
    void Victory()
    {
        BackgroundMusic.Pause();
        gameObject.SetActive(true);
        VictorySource.Play();
        victory = true;
    }

	// Update is called once per frame
	void Update () {
		if(victory && !VictorySource.isPlaying)
        {
            BackgroundMusic.Play();
        }
	}
}
