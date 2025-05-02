using System.Collections;
using UnityEngine;

public class Book : MonoBehaviour
{
    public bool open;

    public bool canClick;

    public GameObject obj1;

    public GameObject obj2;
    private void OnMouseDown()
    {
        if (!open && canClick)
        {
            StartCoroutine(OpenBook());
            canClick = false;
        }

        else if ((canClick))
        {
            StartCoroutine(CloseBook());
            canClick = false;
        }

    }

    IEnumerator OpenBook()
    {
        obj1.transform.Rotate(0, 0, 90);
        yield return new WaitForSeconds(0.5f);
        obj2.transform.Rotate(0, 0, 90);
        yield return new WaitForEndOfFrame();
        canClick = true;
    }

    IEnumerator CloseBook()
    {
        obj1.transform.Rotate(0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        obj2.transform.Rotate(0, 0, 0);
        yield return new WaitForEndOfFrame();
        canClick = true;
    }
}
