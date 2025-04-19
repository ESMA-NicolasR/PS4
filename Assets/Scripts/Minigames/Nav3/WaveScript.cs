using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveScript : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField] private int percision = 40;

    public AnimationCurve ropeAnimationCurve;
    [SerializeField]
    float _waveSize = 1;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private ButtonScript _button1, _button2;

    private float _waving = 0.01f;
    private bool _isGoingUp;

    private void Start()
    {
        _lineRenderer.positionCount = percision;
        LinePointsToTarget();
        StartCoroutine("Waver");
    }

    private void LinePointsToTarget()
    {
        for (int i = 0; i < percision; i+=1)
        {
            _lineRenderer.SetPosition(i, gameObject.transform.position);
        }
    }

    public void ChangeWaving(float value)
    {
        _waving += value;
    }

    private void Update()
    {
        if(Input.GetKey("w") || _button1.IsPressed())
        {
            ChangeWaving(0.0001f);
        }
        if (Input.GetKey("s") || _button2.IsPressed())
        {
            ChangeWaving(-0.0001f);
        }

        if (_waving > 0.15f)
        {
            StartCoroutine("WinCheck");
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = (float)i / ((float)percision - 1f);
            Vector2 offset = Vector2.Perpendicular(new Vector2(1,1)).normalized * ropeAnimationCurve.Evaluate(delta) * _waveSize;
            Vector2 targetPosition = Vector2.Lerp(target.position, gameObject.transform.position, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(target.position, targetPosition, 1);

            _lineRenderer.SetPosition(i, currentPosition);
        }
    }

    private IEnumerator WinCheck()
    {
        yield return new WaitForSeconds(0.5f);
        if ((_waving > 0.15f) != true)
        {
            StopCoroutine("WinCheck");
        }
        yield return new WaitForSeconds(0.5f);
        if (_waving > 0.15f)
        {
            _waveSize = 0;
            print("cleared");
        }
    }

    private IEnumerator Waver()
    {
        if(_isGoingUp)
        {
            _waveSize += 0.1f;
            yield return new WaitForSeconds(_waving);
            DrawRopeWaves();
            if(_waveSize >= 2)
            {
                _isGoingUp = false;
            }
        }
        else
        {
            _waveSize -= 0.1f;
            yield return new WaitForSeconds(_waving);
            DrawRopeWaves();
            if (_waveSize <= -2)
            {
                _isGoingUp = true;
            }
        }
        StartCoroutine("Waver");
    }
}
