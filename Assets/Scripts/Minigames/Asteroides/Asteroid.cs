using System;
using UnityEngine;
using System.Collections;


public class Asteroid : MonoBehaviour
{
    private MinigameAsteroids _minigameAsteroids;
    public float timeToBeDestroyed;
    public float timeToDestroy;
    public float speed;
    private Vector3 _newPosition;
    private bool _isDestroying = false;
    public float growUp;

    private void OnEnable()
    {
        _minigameAsteroids = GameObject.Find("Asteroids").GetComponent<MinigameAsteroids>();
        StartCoroutine(DestroyShip());
        
        _newPosition = new Vector3(_minigameAsteroids.cursorStep,_minigameAsteroids.cursorStep,0);
    }

    private void FixedUpdate()
    {
        if (transform.localPosition.x > _minigameAsteroids.cursorStep*_minigameAsteroids.cursorMaxX)
        {
            _newPosition = new Vector3(-_minigameAsteroids.cursorStep,_newPosition.y,0);
        }
        if (transform.localPosition.x < -_minigameAsteroids.cursorStep*_minigameAsteroids.cursorMaxX)
        {
            _newPosition = new Vector3(_minigameAsteroids.cursorStep,_newPosition.y,0);
        }
        if (transform.localPosition.y > _minigameAsteroids.cursorStep*_minigameAsteroids.cursorMaxY)
        {
            _newPosition = new Vector3(_newPosition.x,-_minigameAsteroids.cursorStep,0);
        }
        if (transform.localPosition.y < -_minigameAsteroids.cursorStep*_minigameAsteroids.cursorMaxY)
        {
            _newPosition = new Vector3(_newPosition.x,_minigameAsteroids.cursorStep,0);
        }
        if (speed != 0)
        {
            Moving();
        }
        transform.localScale = new Vector3(transform.localScale.x+(growUp*0.001f)*Time.deltaTime,transform.localScale.y+(growUp*0.001f)*Time.deltaTime,transform.localScale.z);
    }

    private IEnumerator DestroyAsteroids(GameObject cursor)
    {
        yield return new WaitForSeconds(timeToBeDestroyed);
        if (_isDestroying)
        {   
            _minigameAsteroids.ChangeAsteroidCount(1);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyShip()
    {
        yield return new WaitForSeconds(timeToDestroy);
        print("kaboom");
    }
    
    private void Moving()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, transform.localPosition+_newPosition, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        _isDestroying = true;
        StartCoroutine(DestroyAsteroids(_minigameAsteroids.cursor));
    }
    private void OnTriggerExit(Collider other)
    {
        _isDestroying = false;
        StopCoroutine(DestroyAsteroids(_minigameAsteroids.cursor));
    }
}