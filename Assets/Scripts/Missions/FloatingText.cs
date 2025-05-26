using System;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private Animator _animator;
    public TextMeshProUGUI textMesh;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void DisplayText(string text)
    {
        textMesh.text = text;
        _animator.SetTrigger("Show");
    }
}
