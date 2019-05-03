using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonPlayerController : MonoBehaviour
    {
        //+---------------------------------------------+
        //| anm - stay for ANIMATOR
        //| s - stay for STATIC / SERIALIZED
        //| p - stay for PLAYER
        //+---------------------------------------------+

        [SerializeField] float s_MovingTurnSpeed = 360;
        [SerializeField] float s_StationaryTurnSpeed = 180;
        [SerializeField] float s_JumpPower = 12f;
        [Range(1f, 4f)] [SerializeField] float s_GravityMultiplier = 2f;
        [SerializeField] float s_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float s_MoveSpeedMultiplier = 1f;
        [SerializeField] float s_AnimSpeedMultiplier = 1f;
        [SerializeField] float s_GroundCheckDistance = 0.1f;
        
        private Vector3 m_Move;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        bool m_Crouching;

        // ThirdPerson's variables for the cam
        private Transform p_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera

        // ThirdPerson's variables for movement
        public float speed = 5;
        bool isOnGround;

        // ThirdPerson's variables for support movement
        private ThirdPersonCharacter p_Character; // A reference to the ThirdPersonCharacter on the object
        CapsuleCollider p_Capsule;
        private bool p_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        Rigidbody p_Rigidbody;
        Vector3 p_CapsuleCenter;
        float p_CapsuleHeight;
        
        // ThirdPersonPlayerAnimator's parameters
        Animator p_Animator;
        bool anm_Crouch;
        bool anm_Prone;
        bool anm_Jump;
        bool anm_Walk;
        bool anm_Idle;
        bool anm_Run;


        private void Start()
        {
            // Take components for piece of player controller
            p_Animator = GetComponent<Animator>();
            p_Rigidbody = GetComponent<Rigidbody>();
            p_Capsule = GetComponent<CapsuleCollider>();
            // and take capsule infos for change while player change positions 
            p_CapsuleHeight = p_Capsule.height;
            p_CapsuleCenter = p_Capsule.center;

            p_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_OrigGroundCheckDistance = s_GroundCheckDistance;

            // get the transform of the main camera
            if (Camera.main != null)
            {
                p_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            p_Character = GetComponent<ThirdPersonCharacter>();
        }

        private void FixedUpdate()
        {
            // Call function for player movement
            PlayerMovement();

            if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            {
                p_Animator.SetBool("Walk", true);
            }

            if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
            {
                p_Animator.SetBool("Walk", false);
            }

            // If player PRESS space, I set animator for make him jump
            if (Input.GetKeyDown("space"))
            {
                p_Animator.SetBool("Jump", true);
            }

            // If player PRESS CTRL, I set animator for make him crouch
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                p_Animator.SetBool("Crouch", true);
            }

            // If player is in jump, set bool to false
            if (p_Animator.GetBool("Jump")==true)
            {
                p_Animator.SetBool("Jump", false);
            }

            // If player is in crouch, set bool to false
            if (p_Animator.GetBool("Crouch")==true && Input.GetKeyDown(KeyCode.LeftControl))
            {
                p_Animator.SetBool("Crouch", false);
            }
        }

        // Fixed update is called in sync with physics
        //private void FixedUpdate()
        //{

        //}

        void PlayerMovement()
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");
            Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }

        void ScaleCapsuleForCrouching(bool crouch)
        {
            if (isOnGround && crouch)
            {
                if (m_Crouching) return;
                p_Capsule.height = p_Capsule.height / 2f;
                p_Capsule.center = p_Capsule.center / 2f;
                m_Crouching = true;
            }
            else
            {
                Ray crouchRay = new Ray(p_Rigidbody.position + Vector3.up * p_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = p_CapsuleHeight - p_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, p_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;
                    return;
                }
                p_Capsule.height = p_CapsuleHeight;
                p_Capsule.center = p_CapsuleCenter;
                m_Crouching = false;
            }
        }
    }
}
