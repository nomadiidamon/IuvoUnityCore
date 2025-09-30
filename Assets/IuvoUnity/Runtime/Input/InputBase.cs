using IuvoUnity.Interfaces;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using IuvoUnity.Debug;

namespace IuvoUnity
{
    namespace Inputs
    {
        public abstract class InputActionBase : MonoBehaviour, IEnableable
        {
            public string ActionName => inputAction?.name ?? "None";
            public UnityEvent OnPerformedUnity;
            public System.Action OnPerformed;
            public System.Action OnStarted;
            public System.Action OnCanceled;

            [SerializeField] protected InputActionReference inputActionReference;
            public InputAction inputAction => inputActionReference != null ? inputActionReference.action : null;
            public UnityEvent<InputAction.CallbackContext> onPerformedWithContext;
            public UnityEvent<InputAction.CallbackContext> onStartedWithContext;
            public UnityEvent<InputAction.CallbackContext> onCanceledWithContext;

            private bool isSubscribed = false;
            public bool IsEnabled { get; private set; } = false;

            protected virtual void Awake()
            {
                if (inputActionReference == null || inputActionReference.action == null)
                {
                    IuvoDebug.DebugLogWarning(name + ": InputActionReference or its action is not assigned!");
                }
            }

            protected virtual void Update()
            {
                // Optionally overridden
            }

            protected virtual void FixedUpdate()
            {
                // Optionally overridden
            }

            public void Enable()
            {
                if (!IsEnabled)
                {
                    OnEnable();
                }
            }

            public virtual void OnEnable()
            {
                var action = inputAction;
                if (action != null && !isSubscribed)
                {
                    isSubscribed = true;
                    action.started += OnInputStarted;
                    action.performed += OnInputPerformed;
                    action.canceled += OnInputCanceled;
                    action.Enable();
                    IsEnabled = true;
                }
            }

            public void Disable()
            {
                if (IsEnabled)
                {
                    OnDisable();
                }
            }

            public virtual void OnDisable()
            {
                var action = inputAction;
                if (action != null && isSubscribed)
                {
                    isSubscribed = false;
                    action.started -= OnInputStarted;
                    action.performed -= OnInputPerformed;
                    action.canceled -= OnInputCanceled;
                    action.Disable();
                    IsEnabled = false;
                }
            }

            protected virtual void OnInputPerformed(InputAction.CallbackContext context)
            {
                onPerformedWithContext?.Invoke(context);
                Perform();
            }

            protected virtual void OnInputStarted(InputAction.CallbackContext context)
            {
                onStartedWithContext?.Invoke(context);
                OnStarted?.Invoke();
            }
            protected virtual void OnInputCanceled(InputAction.CallbackContext context)
            {
                onCanceledWithContext?.Invoke(context);
                OnCanceled?.Invoke();
            }

            public void TriggerPerform() => Perform();

            protected virtual void Perform()
            {
                IuvoDebug.DebugLog($"{ActionName} action performed.");
                OnPerformedUnity?.Invoke();
                OnPerformed?.Invoke();
            }

            protected virtual void OnDestroy()
            {
                Disable();
            }
        }

        #region TapInputAction
        public class TapInputAction : InputActionBase
        {
            [SerializeField] float threshold = 0.5f;
            protected override void OnInputPerformed(InputAction.CallbackContext context)
            {
                if (context.duration != 0 && context.duration < threshold)
                    Perform();
            }
        }
        #endregion

        #region DoubleTapInputAction
        public class DoubleTapInputAction : InputActionBase
        {
            [SerializeField] private float doubleTapThreshold = 0.3f;
            private float lastTapTime = -1f;

            protected override void OnInputPerformed(InputAction.CallbackContext context)
            {
                float time = Time.time;
                if (time - lastTapTime <= doubleTapThreshold)
                {
                    Perform();
                    lastTapTime = -1f;
                }
                else
                {
                    lastTapTime = time;
                }
                
            }
            protected override void Update()
            {
                if (Time.time - lastTapTime > doubleTapThreshold)
                    lastTapTime = -1f;
            }
        }
        #endregion

        #region InputCooldownAction
        public class InputCooldownAction : InputActionBase
        {
            [SerializeField] private float cooldownTime = 1f;
            private float lastPerformedTime = -Mathf.Infinity;

            protected override void OnInputPerformed(InputAction.CallbackContext context)
            {
                if (Time.time - lastPerformedTime >= cooldownTime)
                {
                    lastPerformedTime = Time.time;
                    Perform();
                }
            }
            public override void OnDisable()
            {
                base.OnDisable();
                lastPerformedTime = -Mathf.Infinity;
            }
        }
        #endregion

        #region AxisInputAction
        public class AxisInputAction : InputActionBase
        {
            public Vector2 CurrentValue { get; private set; }

            protected override void Update()
            {
                if (inputAction != null && inputAction.enabled)
                    CurrentValue = inputAction.ReadValue<Vector2>();
            }

            protected override void OnInputPerformed(InputAction.CallbackContext context)
            {
                CurrentValue = context.ReadValue<Vector2>();
                Perform();
            }
        }
        #endregion

        #region DirectionalInputAction
        public class DirectionalInputAction : InputActionBase
        {
            [System.Flags]
            public enum Direction
            {
                None = 0,
                Forward = 1 << 0,
                Backward = 1 << 1,
                Left = 1 << 2,
                Right = 1 << 3,
                Up = 1 << 4,
                Down = 1 << 5
            }

            [Header("Threshold Settings")]
            [SerializeField] private float axisThreshold = 0.1f;
            [SerializeField] private float yForce = 1f;


            [Header("Y-Axis Controls")]
            [SerializeField] private InputActionReference inputUpY = new InputActionReference();    // e.g., ButtonSouth (hold)
            [SerializeField] private InputActionReference inputDownY = new InputActionReference();  // e.g., LeftStickButton (tap)
            private void OnUpPerformed(InputAction.CallbackContext ctx) => isUpPressed = true;
            private void OnUpCanceled(InputAction.CallbackContext ctx) => isUpPressed = false;
            private void OnDownPerformed(InputAction.CallbackContext ctx) => isDownPressed = true;
            private void OnDownCanceled(InputAction.CallbackContext ctx) => isDownPressed = false;


            [Header("Events")]
            public UnityEvent<Vector3> onMovementVectorGenerated;

            private Vector2 planarInput = Vector2.zero;
            private bool isUpPressed = false;
            private bool isDownPressed = false;


            public override void OnEnable()
            {
                base.OnEnable();
                if (inputUpY != null)
                {
                    inputUpY.action.performed += OnUpPerformed;
                    inputUpY.action.canceled += OnUpCanceled;
                    inputUpY.action.Enable();
                }
                if (inputDownY != null)
                {
                    inputDownY.action.performed += OnDownPerformed;
                    inputDownY.action.canceled += OnDownCanceled;
                    inputDownY.action.Enable();
                }
            }

            public override void OnDisable()
            {
                base.OnDisable();
                if (inputUpY != null)
                {
                    inputUpY.action.performed -= OnUpPerformed;
                    inputUpY.action.canceled -= OnUpCanceled;
                    inputUpY.action.Disable();
                }
                if (inputDownY != null)
                {
                    inputDownY.action.performed -= OnDownPerformed;
                    inputDownY.action.canceled -= OnDownCanceled;
                    inputDownY.action.Disable();
                }
            }

            protected override void OnInputPerformed(InputAction.CallbackContext context)
            {
                planarInput = context.ReadValue<Vector2>();
                GenerateDirectionVector();
                Perform();
            }

            private void GenerateDirectionVector()
            {
                Vector3 move = Vector3.zero;

                // X (Left / Right)
                if (planarInput.x > axisThreshold) move += Vector3.right;
                if (planarInput.x < -axisThreshold) move += Vector3.left;

                // Z (Forward / Backward)
                if (planarInput.y > axisThreshold) move += Vector3.forward;
                if (planarInput.y < -axisThreshold) move += Vector3.back;

                // Y (Up / Down)
                if (isUpPressed) move += Vector3.up * yForce;
                if (isDownPressed) move += Vector3.down * yForce * 2f;

                onMovementVectorGenerated?.Invoke(move);
            }

            public Vector3 GetCurrentDirectionVector()
            {
                Vector3 move = Vector3.zero;

                if (planarInput.x > axisThreshold) move += Vector3.right;
                if (planarInput.x < -axisThreshold) move += Vector3.left;

                if (planarInput.y > axisThreshold) move += Vector3.forward;
                if (planarInput.y < -axisThreshold) move += Vector3.back;

                if (isUpPressed) move += Vector3.up * yForce;
                if (isDownPressed) move += Vector3.down * yForce * 2f;

                return move;
            }
        }
        #endregion

        public class InputActionHandler<T> where T : InputActionBase
        {
            public T InputActionInstance { get; private set; }

            public event Action OnPerformed;
            public event Action OnStarted;
            public event Action OnCanceled;

            public InputActionHandler(T inputActionInstance)
            {
                InputActionInstance = inputActionInstance ?? throw new ArgumentNullException(nameof(inputActionInstance));

                // Subscribe to InputActionBase events
                InputActionInstance.OnPerformed += HandlePerformed;
                InputActionInstance.OnStarted += HandleStarted;
                InputActionInstance.OnCanceled += HandleCanceled;
            }

            private void HandlePerformed()
            {
                OnPerformed?.Invoke();
            }

            private void HandleStarted()
            {
                OnStarted?.Invoke();
            }

            private void HandleCanceled()
            {
                OnCanceled?.Invoke();
            }

            public void Enable()
            {
                InputActionInstance.Enable();
            }

            public void Disable()
            {
                InputActionInstance.Disable();
            }

            public string GetActionName()
            {
                return InputActionInstance.ActionName;
            }

            public void Perform()
            {
                InputActionInstance.TriggerPerform();
            }

            public TVal? ReadValue<TVal>() where TVal : struct
            {
                return InputActionInstance?.inputAction?.ReadValue<TVal>();
            }

            public Vector2? GetAxisValue()
            {
                if (InputActionInstance is AxisInputAction axisAction)
                    return axisAction.CurrentValue;
                return null;
            }

            public void Dispose()
            {
                if (InputActionInstance != null)
                {
                    InputActionInstance.OnPerformed -= HandlePerformed;
                    InputActionInstance.OnStarted -= HandleStarted;
                    InputActionInstance.OnCanceled -= HandleCanceled;

                    InputActionInstance.Disable();
                }
            }

        }
    }
}