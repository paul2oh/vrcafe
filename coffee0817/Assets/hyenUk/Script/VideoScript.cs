using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour {
    
    //비디오 플레이어
    private VideoPlayer Vplayer;
    private AudioSource Asource;

    // Use this for initialization
    void Start () {
        Asource = GetComponent<AudioSource>();
        Vplayer = GetComponent<VideoPlayer>();
        Vplayer.SetTargetAudioSource(0, Asource);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
