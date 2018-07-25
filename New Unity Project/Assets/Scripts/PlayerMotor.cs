using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour{

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Run every physics iterations
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    //Gets a movement vector
    public void Move(Vector3 velocityDiff)
    {
        velocity = velocityDiff;
    }

    //Gets a rotational vector
    public void Rotate(Vector3 rotationDiff)
    {
        rotation = rotationDiff;
    }

    //Gets a rotational vector for the camera
    public void RotateCamera(Vector3 cameraRotationDiff)
    {
        cameraRotation = cameraRotationDiff;
    }

        // Get force vector for our thrusters
    public void ApplyThruster(Vector3 thrusterForceDiff)
    {
        thrusterForce = thrusterForceDiff;
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);
        }

        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    //Perform rotate based on velocity variable
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
