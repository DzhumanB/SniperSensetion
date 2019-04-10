using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour {
    
    

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.EulerAngles(PickFiringDirection(this.transform.forward, 0.3f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    Vector3 PickFiringDirection(Vector3 muzzleForward, float spreadRadius)
    {
        Vector3 candidate = Random.insideUnitSphere * spreadRadius + muzzleForward;
        return candidate.normalized;
    }
}
