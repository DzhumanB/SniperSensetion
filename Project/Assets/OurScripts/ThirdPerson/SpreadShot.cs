using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
#pragma warning disable CS0618 // Il tipo o il membro è obsoleto
        transform.rotation = Quaternion.EulerAngles(PickFiringDirection(transform.forward, 0.3f));
#pragma warning restore CS0618 // Il tipo o il membro è obsoleto
    }

    Vector3 PickFiringDirection(Vector3 muzzleForward, float spreadRadius)
    {
        Vector3 candidate = Random.insideUnitSphere * spreadRadius + muzzleForward;
        return candidate.normalized;
    }
}
