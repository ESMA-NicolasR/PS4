using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prise : MonoBehaviour
{
    public GameObject Light;
    public GameObject Hook;

    public Drag dragScript;

    public bool inPrise;

    private void Update()
    {
        if (inPrise)
        {
            Light.SetActive(true);
        }

        else
        {
            Light.SetActive(false);
        }
    }

    public void Attache()
    {
        
    }
}
