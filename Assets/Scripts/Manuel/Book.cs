using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Book : Focusable
{
    public bool open;
    public int currentPage;
    
    public Animator animator;
    public Animator animator2;
    
    public List<GameObject> pagesGameObjects;
    public List<Page> pages;

    public List<Collider> helperColliders;

    [SerializeField] private FeedbackSound _openBookFeedback;
    [SerializeField] private FeedbackSound _turnPageFeedback;
    

    protected override void Start()
    {
        base.Start();
        pages = new List<Page>();
        
        for (int i = 0; i < pagesGameObjects.Count; i++)
        {
            if (pagesGameObjects[i].TryGetComponent<Page>(out var page))
            {
                page.DisablePage();
                page.Init(this, i);
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
        int pagesToOpen = currentPage;
        pages[0].EnablePage();
        for(int i=0; i<pagesToOpen; i++)
            TurnPageToLeft(i, false);
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
            foreach (Page page in pages)
            {
                page.StartTurnRight();
                page.DisablePage();
            }
        }
    }

     IEnumerator OpenBook()
    {
        _openBookFeedback.PlayMySound();
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
        _openBookFeedback.PlayMySound();
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

    public void TurnPageToRight(int index)
    {
        _turnPageFeedback.PlayMySound();
        pages[index].StartTurnRight();
        if (index > 0)
        {
            pages[index - 1].EnablePage();
        }
        if (index < pages.Count-1)
        {
            pages[index+1].DisablePage();
        }

        currentPage = index;
    }

    public void TurnPageToLeft(int index, bool playSound = true)
    {
        if(playSound)
            _turnPageFeedback.PlayMySound();
        pages[index].StartTurnLeft();
        if (index < pages.Count - 1)
        {
            pages[index + 1].EnablePage();
        }

        if (index > 0)
        {
            pages[index - 1].DisablePage();
        }

        currentPage = index + 1;
    }
}
