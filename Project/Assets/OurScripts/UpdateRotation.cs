using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRotation : MonoBehaviour {


    public GameObject Cam;
    public GameObject Holder;
    public GameObject Player;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 eulerRotationHolder = new Vector3(Cam.transform.eulerAngles.x, Cam.transform.eulerAngles.y, Cam.transform.eulerAngles.z);
        Vector3 eulerRotationPlayer = new Vector3(0, Cam.transform.eulerAngles.y, 0);

        Holder.transform.rotation = Quaternion.Euler(eulerRotationHolder);
        Player.transform.rotation = Quaternion.Euler(eulerRotationPlayer);

    }
}
