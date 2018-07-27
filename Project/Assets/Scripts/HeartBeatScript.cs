using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatScript : MonoBehaviour {

    public int heartBeatOnStart = 0;
    public int heartBeatCurrent = 0;
    private float multiplier = 1.5f;

    public bool Shaking;
    public CameraShakeScript cameraShake;


    // This function starts befor the start
    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(cameraShake.Shake(0.15f));
    }

    //
    void FixedUpdate()
    {
        heartBeatOnStart = 50;
        heartBeatCurrent = 60;
        int tmp = heartBeatCurrent - heartBeatOnStart;
        if (tmp < 0)
        {

        }
        else
        {
            //StartCoroutine(cameraShake.Shake(1555.0f));
        }
    }
}