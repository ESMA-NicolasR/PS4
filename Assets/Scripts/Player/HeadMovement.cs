using System;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private PlayerTravel _playerTravel;

    void Start()
    {
        _animator = GetComponent<Animator>();
        PlayerTravel.OnTravelStart += OnTravelStart;
        PlayerTravel.OnDestinationReached += OnDestinationReached;
    }

    private void OnTravelStart()
    {
        _animator.SetBool("IsMoving", true);
    }
    private void OnDestinationReached(Station _)
    {
        _animator.SetBool("IsMoving", false);
    }

    private void Update()
    {
        _animator.SetFloat("MoveSpeed", _playerTravel.currentSpeed);
    }
}
