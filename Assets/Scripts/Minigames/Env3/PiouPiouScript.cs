using System.Collections;
using UnityEngine;

public class PiouPiouScript : MonoBehaviour
{
    [SerializeField]
    PiouScript _piou;
    void Start()
    {
        StartCoroutine("PiouSpammer");
    }

    private IEnumerator PiouSpammer()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(_piou, gameObject.transform);
        StartCoroutine("PiouSpammer");
    }
}
