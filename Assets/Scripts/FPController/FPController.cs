using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine.Events;

namespace Artemis
{
    [RequireComponent(typeof(CharacterController))]
    public class FPController : MonoBehaviour
    {
        public bool IsPaused = false;

        [Header("Movement Parameters")]
        public float MaxSpeed => SprintInput ? SprintSpeed : WalkSpeed;
        public float Acceleration = 15f;

        [SerializeField] float WalkSpeed = 3.5f;
        [SerializeField] float SprintSpeed = 8f;

        [Space(15)]
        [Tooltip("This is how high the character can jump")]
        [SerializeField] float JumpHeight = 20f;

        private int timesJumped = 0;

        [SerializeField] bool CanDoubleJump = true;

        public bool Sprinting
        {
            get
            {
                return SprintInput && CurrentSpeed > 0.1f;
            }
        }

        [Header("Looking Parameters")]
        public Vector2 LookSensitivity = new Vector2(0.1f, 0.1f);

        public float PitchLimit = 85f;

        [SerializeField] float currentPitch = 0f;

        public float CurrentPitch
        {
            get => currentPitch;

            set
            {
                currentPitch = Mathf.Clamp(value, -PitchLimit, PitchLimit);
            }
        }

        [Header("Camera Parameters")]
        [SerializeField] float CameraNormalFOV = 60f;
        [SerializeField] float CameraSprintFOV = 80f;
        [SerializeField] float CameraFOVSmoothing = 1f;

        float TargetCameraFOV
        {
            get
            {
                return Sprinting ? CameraSprintFOV : CameraNormalFOV;
            }
        }

        [Header("Phycics Parameters")]
        [SerializeField] float GravityScale = 3f;

        public float VerticalVelocity = 0f;

        public Vector3 CurrentVelocity { get; private set; }
        public float CurrentSpeed { get; private set; }

        private bool wasGrounded = false;

        public bool IsGrounded => characterController.isGrounded;

        [Header("Input")]
        public Vector2 MoveInput;
        public Vector2 LookInput;
        public bool SprintInput;

        [Header("Components")]
        [SerializeField] CinemachineCamera fpCamera;
        [SerializeField] CharacterController characterController;

        [Header("Events")]
        public UnityEvent Landed;

        #region Unity Methods
        void OnValidate()
        {
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }

        }

        void Update()
        {
            if (IsPaused) return;

            MoveUpdate();
            LookUpdate();
            CameraUpdate();

            if (!wasGrounded && IsGrounded)
            {
                timesJumped = 0;
                Landed?.Invoke();
            }

            wasGrounded = IsGrounded;
        }

        #endregion

        #region Controller Methods

        public void TryJump()
        {
            if (IsGrounded == false)
            {
                if (CanDoubleJump && timesJumped < 2 && VerticalVelocity > 0.01f)
                {
                    return;
                }
                if (!CanDoubleJump || timesJumped >= 2)
                {
                    return;
                }
            }

            VerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y * GravityScale);

            timesJumped++;
        }

        void MoveUpdate()
        {
            Vector3 motion = transform.forward * MoveInput.y + transform.right * MoveInput.x;
            motion.y = 0f;
            motion.Normalize();

            if (motion.sqrMagnitude >= 0.01f)
            {
                CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, motion * MaxSpeed, Acceleration * Time.deltaTime);

            }
            else
            {
                CurrentVelocity = Vector3.MoveTowards(CurrentVelocity, Vector3.zero, Acceleration * Time.deltaTime);
            }

            if (IsGrounded && VerticalVelocity <= 0.01f)
            {
                VerticalVelocity = -3f;
            }
            else
            {
                VerticalVelocity += Physics.gravity.y * 20f * Time.deltaTime;
            }

            Vector3 fullVelocity = new Vector3(CurrentVelocity.x, VerticalVelocity, CurrentVelocity.z);

            CollisionFlags flags = characterController.Move(fullVelocity * Time.deltaTime);

            if ((flags & CollisionFlags.Above) != 0 && VerticalVelocity > 0.01f)
            {
                VerticalVelocity = 0f;
            }

            CurrentSpeed = CurrentVelocity.magnitude;
        }

        void LookUpdate()
        {
            Vector2 input = new Vector2(LookInput.x * LookSensitivity.x, LookInput.y * LookSensitivity.y);
            CurrentPitch -= input.y;

            fpCamera.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);

            transform.Rotate(Vector3.up * input.x);
        }

        void CameraUpdate()
        {
            float targetFOV = CameraNormalFOV;

            if (Sprinting)
            {
                float speedRatio = CurrentSpeed / SprintSpeed;

                targetFOV = Mathf.Lerp(CameraNormalFOV, CameraSprintFOV, speedRatio);
            }
            fpCamera.Lens.FieldOfView = Mathf.Lerp(fpCamera.Lens.FieldOfView, targetFOV, CameraFOVSmoothing * Time.deltaTime);

        }

        #endregion

        public void PauseController()
        {
            IsPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ResumeController()
        {
            IsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
}
