﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScopeInfinityMovement : MonoBehaviour {
    /*
    public const int r = 4;
    public const double PI = 3.14159265358979;

    poi
    public const Area = PI*r*r;
    */

    private float timeCounter = 0;
    private bool scope;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        /* Randomizzazioni di un quaternione, dopodichè la trasformazione da Quaternion a Euler e viceversa
         
        Quaternion randomRotation = Random.rotation;
        Vector3 inEuler = randomRotation.eulerAngel;
        Quaternion inQuaternio = Quaternion.Euler(inEuler);
         */
        scope = GameObject.Find("ScopeOverlay").activeSelf;
        timeCounter += Time.deltaTime;
        float tmp_pos = 2 / (3 - Mathf.Cos(2 * timeCounter));
        float x = (tmp_pos * Mathf.Cos(timeCounter)) * 10;
        float y = (tmp_pos * Mathf.Sin(2 * timeCounter) / 2) * 10;

        if (scope)
        {
            transform.localRotation = Quaternion.Euler(x, y, 0);
        }
    }
}
