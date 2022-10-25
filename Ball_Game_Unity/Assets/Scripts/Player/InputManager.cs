using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance; // singleton

    #region Events

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    public delegate void StartPrimaryTouchEvent(Vector2 position, float time);
    public event StartPrimaryTouchEvent OnStartPrimaryTouch;
    public delegate void EndPrimaryTouchEvent(Vector2 position, float time);
    public event EndPrimaryTouchEvent OnEndPrimaryTouch;

    #endregion

    private TouchControlls touchControls;
    private Camera mainCamera;


    private void Awake()
    {
        Instance = this;
        touchControls = new TouchControlls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
        EnhancedTouchSupport.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPressed.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPressed.canceled += ctx => EndTouch(ctx);

        touchControls.Touch.PrimaryTouch.started += ctx => StartPrimaryTouch(ctx);
        touchControls.Touch.PrimaryTouch.canceled += ctx => EndPrimaryTouch(ctx);
    }

    private void StartPrimaryTouch(InputAction.CallbackContext context)
    {
        if (OnStartPrimaryTouch != null) OnStartPrimaryTouch(Utils.ViewportToScreenPoint(mainCamera, touchControls.Touch.PrimaryPosision.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndPrimaryTouch(InputAction.CallbackContext context)
    {
        if (OnEndPrimaryTouch != null) OnEndPrimaryTouch(Utils.ViewportToScreenPoint(mainCamera, touchControls.Touch.PrimaryPosision.ReadValue<Vector2>()), (float)context.startTime);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ViewportToScreenPoint(mainCamera, touchControls.Touch.PrimaryPosision.ReadValue<Vector2>());
    }





    private void StartTouch(InputAction.CallbackContext context)
    {
        //print(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (OnStartTouch != null) OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        //print(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (OnEndTouch != null) OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time); // onend touch - i will asfasfasfasfasffasasfasfasfsfasfasf
                                                                                                                         // no wonder it was null, cos it wasn't checking if it was null, but im the guru of unity, i assended the gods and became...
    }

    private void FingerDown(Finger finger)
    {
        if (OnStartTouch != null) OnStartTouch(finger.screenPosition, Time.time);
    }

    void Update()
    {
        //Debug.Log(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches);
        //foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
        //{
        //    Debug.Log(touch.phase == UnityEngine.InputSystem.TouchPhase.Began);
        //}
    }
}
