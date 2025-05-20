using System.Collections;
using UnityEngine;

public class RepeatableClickable : Clickable
{
    public float timeBetweenClicksSlow = 0.2f;
    public float timeBetweenClicksFast = 0.1f;
    public float timeToFast = 0.5f;
    private Coroutine _autoclickCoroutine;

    protected override void Interact()
    {
        _autoclickCoroutine = StartCoroutine(AutoClick());
    }

    private IEnumerator AutoClick()
    {
        float timer = 0f;
        // Start with slow auto
        while (timer < timeToFast)
        {
            yield return new WaitForSeconds(timeBetweenClicksSlow);
            OnClick?.Invoke();
            timer += timeBetweenClicksSlow;
        }
        // Increase to fast auto after set time
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenClicksFast);
            OnClick?.Invoke();
        }
    }

    protected override void OnMouseExit()
    {
        base.OnMouseExit();
        if (_autoclickCoroutine != null)
        {
            StopCoroutine(_autoclickCoroutine);
        }
    }

    private void OnMouseUp()
    {
        if (_autoclickCoroutine != null)
        {
            StopCoroutine(_autoclickCoroutine);
        }
    }
}
