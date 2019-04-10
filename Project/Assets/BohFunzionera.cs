using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BohFunzionera : MonoBehaviour {
    /*
    public const int r = 4;
    public const double PI = 3.14159265358979;

    poi
    public const Area = PI*r*r;
    */

    private float timeCounter = 0;

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
        timeCounter += Time.deltaTime;
        float tmp_pos = 2 / (3 - Mathf.Cos(2 * timeCounter));
        float x = (tmp_pos * Mathf.Cos(timeCounter)) *10;
        float y = (tmp_pos * Mathf.Sin(2 * timeCounter) / 2) *10;

        float x1 = (tmp_pos * Mathf.Sin(2 * timeCounter) / 2) * 20;
        float y2 = (tmp_pos * Mathf.Cos(timeCounter)) * 20;

        transform.rotation = Quaternion.Euler(x1,y2,0);
    }
}
