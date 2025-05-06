using UnityEngine;

public class Animatable : Clickable
{
    
    public Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    protected override void Interact()
    {
        animator.SetTrigger("Interact");
    }
}
