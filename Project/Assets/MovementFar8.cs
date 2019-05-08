using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFar8 : MonoBehaviour
{

    private float timeCounter = 0;
    //private int x;
    //private int y;
    // private bool r_or_l = true;
    // public int numbe = 0;
    private Vector3 startPos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        startPos = transform.position;
        /* https://gamedev.stackexchange.com/questions/43691/how-can-i-move-an-object-in-an-infinity-or-figure-8-trajectory */
        timeCounter += Time.deltaTime;
        float tmp_pos = 1f / (3 - Mathf.Cos(2 * timeCounter));
        float x = tmp_pos * Mathf.Cos(timeCounter);
        float y = tmp_pos * Mathf.Sin(2 * timeCounter) / 2;
        transform.position = new Vector3(x, y, 0) + startPos;
    }
}