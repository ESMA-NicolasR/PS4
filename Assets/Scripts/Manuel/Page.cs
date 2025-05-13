using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Page : Animatable
{
    public bool turned;

    [SerializeField] public int index;

    [SerializeField] private Book _book;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Interact()
    {
        if (!turned)
        {
            StartTurnLeft();
        }

        else
        {
            StartTurnRight();
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

       protected virtual void EnablePagesRight()
    {
        if (index < _book.pages.Count-1)
        {
            _book.pages[index + 1].EnablePage();
        }
        Debug.Log("Active a Droite" + index);
        if (index > 0)
        {
            _book.pages[index-1].DisablePage();
        }

    }

         protected virtual void EnablePagesLeft()
    {
        if (index > 0)
        {
            _book.pages[index - 1].EnablePage();
        }
        Debug.Log("Active a Gauche" + index);
        if (index < _book.pages.Count-1)
        {
            _book.pages[index+1].DisablePage();
        }
    }
    
    IEnumerator TurnLeft() 
    {
        animator.SetBool("TurningLeft", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("TurningLeft", false);
        turned = true;
        EnablePagesRight();
    }
    
    IEnumerator TurnRight() 
    {
        animator.SetBool("TurningRight", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("TurningRight", false);
        turned = false;
        EnablePagesLeft();
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
