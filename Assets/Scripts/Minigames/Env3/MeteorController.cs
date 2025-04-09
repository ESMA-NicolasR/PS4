using System.Collections;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private int _hp = 3;
    private void Update()
    {
        if(_hp <= 0)
        {
            Destroy(gameObject);
        }
        if (transform.localScale.x >= 4.0f)
        {
            print("boum");
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.localScale = transform.localScale * 1.005f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Aim"))
        {
            StartCoroutine("TakingDamage");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Aim"))
        {
            StopCoroutine("TakingDamage");
        }
    }
    private IEnumerator TakingDamage()
    {
        while (_hp > 0)
        {
            _hp -= 1;
            yield return new WaitForSeconds(1f);
        }
    }
}
