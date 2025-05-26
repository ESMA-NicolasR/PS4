using System.Collections;
using TMPro;
using UnityEngine;

public class MissionText : MonoBehaviour
{
    public TextMeshPro textMesh;
    public float timeBetweenChars;

    public void DisplayText(string text)
    {
        textMesh.text = text;
        StartCoroutine(DisplayTextCharByChar());
    }

    private IEnumerator DisplayTextCharByChar()
    {
        textMesh.maxVisibleCharacters = 0;
        while (textMesh.maxVisibleCharacters < textMesh.text.Length)
        {
            textMesh.maxVisibleCharacters++;
            yield return new WaitForSeconds(timeBetweenChars);
        }
    }
}
