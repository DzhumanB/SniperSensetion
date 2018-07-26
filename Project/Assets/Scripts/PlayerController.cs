using UnityEngine;


[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float lookSensitivity = 3.0f;


    [SerializeField]
    private float thrusterForceCoef = 10.0f;

    [Header("Sprint settings:")]
    //[SerializeField]
    //private JointDriveMode jointMode = JointDriveMode.Position;
    //[SerializeField]
    //private float jointSprint = 5.0f;
    //[SerializeField]
    //private float jointMaxForce = 2.0f;

    private PlayerMotor motor;
    //private ConfigurableJoint joint;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        //joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        //Calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //Final move vector
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        //Apply movement
        motor.Move(velocity);

        //Calculate rotation as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //Apply  rotation
        motor.Rotate(rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //Apply  rotation
        motor.RotateCamera(cameraRotation);


        //Calculate the thrusterforce based on player input
        Vector3 thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            thrusterForce = Vector3.up * thrusterForceCoef;
        }

        //Apply thruster force
        motor.ApplyThruster(thrusterForce);
    }
}

