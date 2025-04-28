using UnityEngine;

public class Animatable : Clickable
{
    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }
    protected override void Interact()
    {
        _animator.SetTrigger("Interact");
    }
}
