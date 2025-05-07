using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Page : Animatable
{
    public bool turned;

    public int index;

    [SerializeField] private Book _book;

    protected override void Start()
    {
        base.Start();
    }

    public void Update()
    {
        if (_book.open == false)
        {
            StartCoroutine(TurnRight());
        }
    }

    protected override void Interact()
    {
        if (!turned)
        {
            StartCoroutine(TurnLeft());
        }

        else
        {
            StartCoroutine(TurnRight());
        }
            
    }

    public virtual void DisablePage()
    {
        base.Disable();
    }
    
    IEnumerator TurnLeft() 
    {
        animator.SetBool("TurningLeft", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("TurningLeft", false);
        turned = true;
    }
    
    IEnumerator TurnRight() 
    {
        animator.SetBool("TurningRight", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("TurningRight", false);
        turned = false;
    }
}
