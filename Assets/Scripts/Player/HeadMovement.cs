using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
}
