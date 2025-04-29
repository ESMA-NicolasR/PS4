using System;
using System.Collections;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private PlayerTravel _playerTravel;
    private Coroutine _coroutine;

    void Start()
    {
        _animator = GetComponent<Animator>();
        PlayerTravel.OnTravelStart += OnTravelStart;
        PlayerTravel.OnDestinationReached += OnDestinationReached;
    }

    private void OnTravelStart()
    {
        _animator.SetBool("IsMoving", true);
        _coroutine = StartCoroutine(CoUpdate());
    }
    private void OnDestinationReached(Station _)
    {
        _animator.SetBool("IsMoving", false);
        StopCoroutine(_coroutine);
    }

    private IEnumerator CoUpdate()
    {
        // Behaves like an Update, but must not always be active
        while (true)
        {
            _animator.SetFloat("MoveSpeed", _playerTravel.currentSpeed);
            _animator.SetFloat("TimeToArrive", _playerTravel.timeToArrive);
            yield return new WaitForEndOfFrame(); 
        }
    }
}
