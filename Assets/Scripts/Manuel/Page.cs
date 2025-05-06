using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Page : Animatable
{
    public bool turned;

    protected override void Start()
    {
        base.Start();
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
