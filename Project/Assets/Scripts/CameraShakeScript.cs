using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Shake( float intensity)
    {
        Vector3 originalPos = transform.localPosition;

        float x = Random.Range(-1.0f, 1.0f) * intensity;
        float y = Random.Range(-1.0f, 1.0f) * intensity;

        transform.localPosition = new Vector3(x, y, originalPos.z);
        yield return null;
    }
}
