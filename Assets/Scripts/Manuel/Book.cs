using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Book : Focusable
{
    public bool open;
    
    public Animator animator;
    public Animator animator2;
    
    public List<Page> pages;

    protected override void Start()
    {
        base.Start();
    }
    

    public void StartBook()
    {
        Interact();
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].index = i;
            pages[i].DisablePage();
        }
        
    }
    
    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(OpenBook());
        open = true;
    }
    

    public override void LoseFocus()
    {
        base.LoseFocus();
        if (open)
        {
            StartCoroutine(CloseBook());
            open = false;  
        }
    }

     IEnumerator OpenBook()
    {
        animator.SetBool("Open", true);
        animator2.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Open", false);
        animator2.SetBool("Open", false);
    }

    IEnumerator CloseBook() 
    {
        animator.SetBool("Close", true);
        animator2.SetBool("Close", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Close", false);
        animator2.SetBool("Close", false);
    }
    
}
