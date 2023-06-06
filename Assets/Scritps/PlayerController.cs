using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField] private Vector2 JoystickSize = new Vector2(200, 200);
    public joyStick Joystick;
    public NavMeshAgent playerNavMeshAgent;
    private Finger MovementFinger;
    public Vector2 MovementAmount;
    public Animator playerAnimator;
    public GameObject foam;
    public GameObject tray;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

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
        Vector3 scaledMovement = playerNavMeshAgent.speed * Time.deltaTime * new Vector3(MovementAmount.x, 0, MovementAmount.y);

        playerNavMeshAgent.Move(scaledMovement);

        playerNavMeshAgent.transform.LookAt(playerNavMeshAgent.transform.position + scaledMovement, Vector3.up);

        playerAnimator.SetFloat("moveX", MovementAmount.x);
        playerAnimator.SetFloat("moveZ", MovementAmount.y);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            playerAnimator.SetLayerWeight(1, 0);
            foam.SetActive(true);
            playerAnimator.SetBool("IsSwim", true);
            gameObject.GetComponent<NavMeshAgent>().baseOffset = -22f;
            gameObject.GetComponent<NavMeshAgent>().speed = 3.5f;
            tray.SetActive(false);
        }
        if (other.CompareTag("Walk"))
        {
            playerAnimator.SetLayerWeight(1, 0);
            foam.SetActive(false);
            playerAnimator.SetBool("IsSwim", false);
            gameObject.GetComponent<NavMeshAgent>().baseOffset = 0f;
            gameObject.GetComponent<NavMeshAgent>().speed = 6f;
            tray.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bar"))
        {
            playerAnimator.SetLayerWeight(1, 1);
            tray.SetActive(true);

            StartCoroutine(CloseTray());
        }

        if (other.CompareTag("OutBar"))
        {
            playerAnimator.SetLayerWeight(1, 0);
            tray.SetActive(false);
        }
    }

    IEnumerator CloseTray()
    {
        yield return new WaitForSeconds(20);
        playerAnimator.SetLayerWeight(1, 0);
        tray.SetActive(false);
    }
}
