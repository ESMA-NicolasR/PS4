using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerFocus : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("The UI element that allows to unfocus")]
    public GameObject exitFocus;
    [Header("Dependencies")]
    [Tooltip("The camera parent that moves when focused")]
    public GameObject playerHead;
    [Tooltip("The original position of the head")]
    public Transform playerPov;
    [Tooltip("The position of the head when focused")]
    public Transform focusPov;
    [Header("Tweaking")]
    [Tooltip("Total time in seconds to (un)focus")]
    public float timeToFocus;
    [Tooltip("How the head moves to focus")]
    public AnimationCurve focusCurve;
    // Internal variables
    private bool _isFocused;
    // Internal components
    private CursorMoveCamera _cursorMoveCamera;
    //Delegates
    public static event Action OnLoseFocus;
    
    private void OnEnable()
    {
        Focusable.OnGainFocus += OnGainFocus;
    }

    private void OnDisable()
    {
        Focusable.OnGainFocus -= OnGainFocus;
    }

    private void Awake()
    {
        _cursorMoveCamera = GetComponent<CursorMoveCamera>();
    }

    private void Start()
    {
        exitFocus.SetActive(false);
    }

    private void OnGainFocus(Transform pov)
    {
        if (_isFocused) return;
        
        _isFocused = true;
        StartCoroutine(GainFocus(pov));
    }

    private IEnumerator GainFocus(Transform pov)
    {
        focusPov = pov;
        _cursorMoveCamera.canMove = false;
        yield return StartCoroutine(FocusTo(focusPov));
        exitFocus.SetActive(true);
    }

    public void LoseFocus()
    {
        if (!_isFocused) return;
        
        _isFocused = false;
        StartCoroutine(LoseFocusCoroutine());
    }

    private IEnumerator LoseFocusCoroutine()
    {
        OnLoseFocus?.Invoke();
        exitFocus.SetActive(false);
        yield return StartCoroutine(FocusTo(playerPov));
        _cursorMoveCamera.ResetCamera();
        _cursorMoveCamera.canMove = true;
    }

    private IEnumerator FocusTo(Transform to)
    {
        Cursor.visible = false;
        var initialPosition = playerHead.transform.position;
        var initialRotation = playerHead.transform.rotation;
        float ellapsedTime = 0;
        while (ellapsedTime < timeToFocus)
        {
            ellapsedTime += Time.deltaTime;
            float ratio = focusCurve.Evaluate(ellapsedTime / timeToFocus);
            playerHead.transform.position = Vector3.Lerp(initialPosition, to.position, ratio);
            playerHead.transform.rotation = Quaternion.Lerp(initialRotation, to.rotation, ratio);
            yield return new WaitForEndOfFrame();
        }
        // Snap to the desired coordinates
        playerHead.transform.position = to.position;
        playerHead.transform.rotation = to.rotation;
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
    }
}
