using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFocus : MonoBehaviour
{
    public GameObject exitFocus;
    public GameObject playerCamera;
    public Transform playerPov;
    public Transform focusPov;
    public float timeToFocus;
    private bool _isFocused;
    public AnimationCurve focusCurve;
    
    private CursorMoveCamera _cursorMoveCamera;
    
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

    public void OnLoseFocus()
    {
        if (!_isFocused) return;
        
        _isFocused = false;
        StartCoroutine(LoseFocus());
    }

    private IEnumerator LoseFocus()
    {
        exitFocus.SetActive(false);
        yield return StartCoroutine(FocusTo(playerPov));
        _cursorMoveCamera.ResetCamera();
        _cursorMoveCamera.canMove = true;
    }

    private IEnumerator FocusTo(Transform to)
    {
        Cursor.visible = false;
        var initialPosition = _cursorMoveCamera.playerCamera.transform.position;
        var initialRotation = _cursorMoveCamera.playerCamera.transform.rotation;
        float ellapsedTime = 0;
        while (ellapsedTime < timeToFocus)
        {
            ellapsedTime += Time.deltaTime;
            float ratio = focusCurve.Evaluate(ellapsedTime / timeToFocus);
            playerCamera.transform.position = Vector3.Lerp(initialPosition, to.position, ratio);
            playerCamera.transform.rotation = Quaternion.Lerp(initialRotation, to.rotation, ratio);
            yield return new WaitForEndOfFrame();
        }
        // Snap to the desired coordinates
        playerCamera.transform.position = to.position;
        playerCamera.transform.rotation = to.rotation;
        Cursor.visible = true;
        Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));

    }
}
