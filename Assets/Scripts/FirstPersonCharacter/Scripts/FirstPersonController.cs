using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using EZCameraShake;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] public bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] public float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        public float m_GravityMultiplier;
        [SerializeField] public MouseLook m_MouseLook;
        [SerializeField] public bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        
        

        private Camera m_Camera;
        public bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        public Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        public bool m_Jumping;
        private AudioSource m_AudioSource;

        //Add by Théo
        [Header("Custom variable")]
        public Camera m_gunCamera;
        public bool m_Active = true;

        public int lastCheckPoint = 0;
        private Quaternion lastRotation;
        public float slideSpeed = 2f;

        public float m_distanceFootDetection = 2f;
        public TrailRenderer m_trail;

        private Rigidbody m_rigibody;
        private CameraShaker[] m_CameraShake;
        private GameObject LastTouch;
        private Vector3 LastMovePos;
        private Transform m_platform;
        private float SaveJumpPower;

        [Header("Sound")]
        [Range(0f, 1f)] public float volume_deaths;
            public AudioClip[] deaths;
        [Range(0f, 1f)] public float volume_FootStep;
            [SerializeField] private float m_StepInterval;            // time between to foorsteps
            [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [Range(0f, 1f)] public float volume_Jump;
            [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [Range(0f, 1f)] public float volume_Landing;
            [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_rigibody = GetComponent<Rigidbody>();
            m_CameraShake = GetComponentsInChildren<CameraShaker>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
            SaveJumpPower = m_JumpSpeed;

            //Init Checkpoint
            lastCheckPoint = 1;
            lastRotation = Quaternion.identity;
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            m_MouseLook.m_Active = m_Active;

            if(m_CharacterController.isGrounded){
                m_JumpSpeed = SaveJumpPower;
            }

            if(m_Active){
                // the jump state needs to read here to make sure it is not missed
                if (!m_Jump && !m_Jumping)
                {
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                }

                if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
                {
                    StartCoroutine(m_JumpBob.DoBobCycle());
                    PlayLandingSound();
                    m_MoveDir.y = 0f;
                    m_Jumping = false;
                }
                if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
                {
                    m_MoveDir.y = 0f;
                }

                m_PreviouslyGrounded = m_CharacterController.isGrounded;
            }

        }


        private void PlayLandingSound()
        {
            m_AudioSource.volume = volume_Landing;
            m_AudioSource.PlayOneShot(m_LandSound);
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            if(m_Active){
                float speed;
                GetInput(out speed);
                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

                // get a normal for the surface that is being touched to move along it
                RaycastHit hitInfo;
                Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                                m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

                m_MoveDir.x = desiredMove.x*speed;
                m_MoveDir.z = desiredMove.z*speed;


                if (m_CharacterController.isGrounded)
                {
                    m_MoveDir.y = -m_StickToGroundForce;

                    if (m_Jump)
                    {
                        m_MoveDir.y = m_JumpSpeed;
                        PlayJumpSound();
                        m_Jump = false;
                        m_Jumping = true;
                    }
                }
                else
                {
                    m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
                }

                m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

                ProgressStepCycle(speed);
                UpdateCameraPosition(speed);

                m_MouseLook.UpdateCursorLock();


                
            }

            //Check if the player is on a movingPlatform
            RaycastHit hitinfo;
            LayerMask m_mask = LayerMask.NameToLayer("Travelling");

            if(Physics.Raycast(transform.position, - transform.up, out hitinfo ,m_distanceFootDetection)){
                //Accorche le perso au plateform mouvante
                if( hitinfo.transform.gameObject.layer == m_mask.value )
                {
                    SetPlatform( hitinfo.transform );    
                }
                else
                {
                    RemovePlatform( m_platform );
                }

                //Fait glisser le perssonnage
                if(hitinfo.collider.tag == "Rampe" && hitinfo.collider.GetComponent<Pente>()){
                    slideSpeed = hitinfo.collider.GetComponent<Pente>().penteValue;
                    Vector3 hitNormal = hitinfo.normal;
                    Vector3 moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                    moveDirection *= slideSpeed;
                    m_CharacterController.Move(moveDirection);
                }
            }
            else{
                RemovePlatform( m_platform );
            }
        }


        private void SetPlatform( Transform parent )
        {
            if( parent != m_platform )
            {
                m_platform = parent;
                transform.SetParent( m_platform, true );
                m_MouseLook.ComputeCurrentRot (transform, m_Camera.transform);
            }
        }
        private void RemovePlatform( Transform parent )
        {
            if( (parent == m_platform) && (parent != null) )
            {
                m_platform = null;
                transform.SetParent( m_platform, true );
                m_MouseLook.ComputeCurrentRot (transform, m_Camera.transform);
            }
        }
        
        public void JumpWithPropullseur(float JumpPower){
            m_JumpSpeed = JumpPower;
			
			m_MoveDir -= Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
			m_MoveDir.y = JumpPower;
			m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
        }

        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.volume = volume_Jump;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.volume = volume_FootStep;
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            if(m_CharacterController.isGrounded){
                m_IsWalking = !Input.GetButton("Running");
            }
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {

            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
            
        }

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Checkpoint"){
                if(other.GetComponent<Checkpoint_Script>().CheckPoint_value > lastCheckPoint){
                    //Position
                    lastCheckPoint = other.GetComponent<Checkpoint_Script>().CheckPoint_value;
                    lastRotation = other.GetComponent<Checkpoint_Script>().rotation;
                }
            }
            
            if(other.tag == "DeathZone"){
                //Play Sound
                int index = Random.Range(0, deaths.Length);
                m_AudioSource.Stop();
                m_AudioSource.volume = volume_deaths;
                m_AudioSource.clip = deaths[index];
                m_AudioSource.Play();

                foreach (var checkpoins in GameObject.FindGameObjectsWithTag("Checkpoint"))
                {
                    int value = checkpoins.GetComponent<Checkpoint_Script>().CheckPoint_value;
                    if(value == lastCheckPoint){
                        m_trail.time = 0;
                        transform.position = checkpoins.transform.position;
                        m_MouseLook.m_CharacterTargetRot = lastRotation;
                        m_MouseLook.m_CameraTargetRot = Quaternion.identity;

                        //Camera Shake
                        
            
                        foreach (var CameraShake in m_CameraShake)
                        {
                            CameraShaker cam = CameraShaker.GetInstance(CameraShake.gameObject.name);
                            if(cam.name == "CameraShaker"){
                                CameraShakeInstance m_CameraPresset = new CameraShakeInstance(7f, 10f);
                                cam.ShakeOnce(m_CameraPresset.Magnitude , m_CameraPresset.Roughness , 0f, 1.2f);
                            }
                            if(cam.name == "GunCamera"){
                                CameraShakeInstance m_CameraPresset = new CameraShakeInstance(15f, 20f);
                                cam.ShakeOnce(m_CameraPresset.Magnitude , m_CameraPresset.Roughness , 0.5f, 1.5f);
                            }
                        }
                        m_trail.time = 4;
                        break;
                    }
                }
            }
        }

        public void CameraShake(){
            foreach (var CameraShake in m_CameraShake)
            {
                CameraShaker cam = CameraShaker.GetInstance(CameraShake.gameObject.name);
                if(cam.name == "CameraShaker"){
                    CameraShakeInstance m_CameraPresset = new CameraShakeInstance(9f, 10f);
                    cam.ShakeOnce(m_CameraPresset.Magnitude , m_CameraPresset.Roughness , 1.5f, 1.5f);
                }
                if(cam.name == "GunCamera"){
                    CameraShakeInstance m_CameraPresset = new CameraShakeInstance(20f, 20f);
                    cam.ShakeOnce(m_CameraPresset.Magnitude , m_CameraPresset.Roughness , 1.5f, 1.5f);
                }
            }
        }
    }
}
