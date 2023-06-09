using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandController : MonoBehaviour
{
    [SerializeField] private Vector2 JoystickSize = new Vector2(200, 200);
    public joyStick Joystick;
    private Finger MovementFinger;
    public Vector2 MovementAmount;
    public float speed;
    Rigidbody _rb;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        _rb = gameObject.transform.GetComponent<Rigidbody>();
    }

    private void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 1f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance(
                currentTouch.screenPosition,
                Joystick.joyStickObj.anchoredPosition
            ) > maxMovement)
            {
                knobPosition = (
                                   currentTouch.screenPosition - Joystick.joyStickObj.anchoredPosition
                               ).normalized
                               * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.joyStickObj.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleFingerDown(Finger touchedFinger)
    {
        if (MovementFinger == null && touchedFinger.screenPosition.x <= Screen.width)
        {
            MovementFinger = touchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.joyStickObj.sizeDelta = JoystickSize;
            Joystick.joyStickObj.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
        }
    }

    private void HandleLoseFinger(Finger lostFinger)
    {
        if (lostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < JoystickSize.x / 2)
        {
            startPosition.x = JoystickSize.x / 2;
        }

        if (startPosition.y < JoystickSize.y / 2)
        {
            startPosition.y = JoystickSize.y / 2;
        }
        else if (startPosition.y > Screen.height - JoystickSize.y / 2)
        {
            startPosition.y = Screen.height - JoystickSize.y / 2;
        }
        return startPosition;
    }

    void Update()
    {
        Vector3 scaledMovement = new Vector3(MovementAmount.x, 0, MovementAmount.y);

        _rb.MovePosition(transform.position + scaledMovement * Time.deltaTime * -speed);
    }
}
