using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour {

    public Animator animator;

    public GameObject scopeOverlay;

    public GameObject weaponCamera;

    public Camera mainCam;

    public float scopedFOV = 10.0f;
    private float normalFOV;

    private bool scope = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            scope = !scope;
            animator.SetBool("isScoped", scope);

            if (scope)
            {
                StartCoroutine(OnScoped());
            }
            else
            {
                OnUnScoped();
            }
        }   
	}

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFOV = mainCam.fieldOfView;
        mainCam.fieldOfView = scopedFOV;
    }

    void OnUnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        mainCam.fieldOfView = normalFOV;
    }
}
