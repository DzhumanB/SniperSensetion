using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeartBeatCalibrationScript : MonoBehaviour {

    public Text currentBMP;
    public Text Results;

    public int BPM;
    public int minBPM;
    public int avgBPM;
    public int maxBPM;
    public List<int> listBPM = new List<int>();

    public bool calibrationInProgress;

    public float timeTest = 30.0f;
    public float interval = 0.5f;
    public float chronometer = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(TCPServer.getvalueBPM() != 0 && calibrationInProgress == false)
        {
            StartCoroutine(Statistic());
            calibrationInProgress = true;
            chronometer = 0;
        }

        chronometer += Time.deltaTime;
	}

    private IEnumerator Statistic()
    {
        chronometer = 0;
        while (true)
        {
            if(chronometer < timeTest)
            {
                BPM = TCPServer.getvalueBPM();
                currentBMP.text = BPM.ToString();
                listBPM.Add(BPM);
                yield return new WaitForSeconds(interval);
            }
            else
            {
                avgBPM = Convert.ToInt32(listBPM.Average());
                minBPM = listBPM.Min();
                maxBPM = listBPM.Max();
                Results.text = ("Minima = " + minBPM.ToString() + 
                               "\nMedia = " + avgBPM.ToString() +
                             "\nMassima = " + maxBPM.ToString());
                yield return null;
            }
        } 
    }
}
