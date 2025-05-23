using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Book : Focusable
{
    public bool open;
    
    public Animator animator;
    public Animator animator2;
    
    public List<GameObject> pagesGameObjects;
    public List<Page> pages;

    public List<Collider> helperColliders;
    

    protected override void Start()
    {
        base.Start();
        pages = new List<Page>();
        
        for (int i = 0; i < pagesGameObjects.Count; i++)
        {
            if (pagesGameObjects[i].TryGetComponent<Page>(out var page))
            {
                page.index = i;
                page.DisablePage();
                pages.Add(page);
            }
        }

    }

    public void StartBook()
    {
        Interact();
        foreach (Page page in pages)
        {
            page.DisablePage();
        }
        pages[0].EnablePage();
    }
    
    
    
    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(OpenBook());
        open = true;
        clickableSound.PlayMySound();
    }
    

    public override void LoseFocus()
    {
        base.LoseFocus();
        if (open)
        {
            StartCoroutine(CloseBook());
            open = false;
            foreach (Page page in pages)
            {
                page.StartTurnRight();
                page.DisablePage();
            }
        }
    }

     IEnumerator OpenBook()
    {
        foreach (var collider in helperColliders)
        {
            collider.enabled = false;
        }
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
        foreach (var collider in helperColliders)
        {
            collider.enabled = true;
        }
    }
    
}
