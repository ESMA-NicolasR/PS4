using System.Collections;
using UnityEngine;

public class Page : Animatable
{
    public bool turned;

    private int _index;
    private Book _book;
    public bool isTurning;

    public void Init(Book book, int index)
    {
        _index = index;
        _book = book;
    }
    
    protected override void Interact()
    {
        if (isTurning) return;
        
        if (!turned)
        {
            _book.TurnPageToLeft(_index);
        }
        else
        {
            _book.TurnPageToRight(_index);
        }
    }

    public virtual void DisablePage()
    {
        base.Disable();
    }
    
    public virtual void EnablePage()
    {
        base.Enable();
    }
    
    IEnumerator TurnLeft() 
    {
        isTurning = true;
        animator.SetBool("TurningLeft", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("TurningLeft", false);
        turned = true;
        isTurning = false;
    }
    
    IEnumerator TurnRight() 
    {
        isTurning = true;
        animator.SetBool("TurningRight", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("TurningRight", false);
        turned = false;
        isTurning = false;
    }

    public void StartTurnLeft()
    {
        StartCoroutine(TurnLeft());
    }
    
    public void StartTurnRight()
    {
        StartCoroutine(TurnRight());
    }
}
