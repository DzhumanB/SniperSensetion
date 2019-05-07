using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement8 : MonoBehaviour {

    private float timeCounter = 0;
    //private int x;
    //private int y;
    // private bool r_or_l = true;
    // public int numbe = 0;
    private Vector3 startPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        /* https://gamedev.stackexchange.com/questions/43691/how-can-i-move-an-object-in-an-infinity-or-figure-8-trajectory */
        timeCounter += Time.deltaTime;
        float tmp_pos = 200 / (3 - Mathf.Cos(2 * timeCounter));
        float x = tmp_pos * Mathf.Cos(timeCounter);
        float y = tmp_pos * Mathf.Sin(2 * timeCounter) / 2;
        transform.position = new Vector3(x, y, 0) + startPos;
    }

    private void FixedUpdate()
    {
        
    }
}

/* FUNZIONA CON LA LOGICA DEL SIN E COS (ma non è addatto al mio problema)
timeCounter += Time.deltaTime; //* speed;
        if (r_or_l == true && this.transform.position.x >= 0 && this.transform.position.x <= 0.0010 )
        {
    numbe = 1 - 2;
    r_or_l = false;

}
        if (r_or_l == false && this.transform.position.x >= 0 && this.transform.position.x <= 0.0010)
        {
    numbe = 1;
    r_or_l = true;
}
        float x = Mathf.Cos(timeCounter) + numbe;
        float y = Mathf.Sin(timeCounter);
        float z = 5.48f;
        */