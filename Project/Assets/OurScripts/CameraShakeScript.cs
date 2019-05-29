using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour {

    private float timeCounter = 0;

    public float speed=0;
    public float width=0;
    public float height=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime; //* speed;

        float x = Mathf.Cos(timeCounter);
        float y = Mathf.Sin(timeCounter);
        float z = 0;

        transform.position = new Vector3(x, y, z);
		
	}

   /* public IEnumerator Shake( float intensity)
    {
        Vector3 originalPos = transform.localPosition;

        float x = Random.Range(-1.0f, 1.0f) * intensity;
        float y = Random.Range(-1.0f, 1.0f) * intensity;

        transform.localPosition = new Vector3(x, y, originalPos.z);
        yield return null;
    }*/ 
}
