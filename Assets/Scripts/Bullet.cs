using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float timeAlive;

    private bool _initialized = false;
    private Vector3 _direction;
    private float _lifeTimer = 0.0f;

    private void Update()
    {
        _lifeTimer += Time.deltaTime;
        if (_lifeTimer >= timeAlive)
        {
            Destroy(gameObject);
        }

        if (_initialized)
        {
            transform.position += _direction.normalized * _speed * Time.deltaTime;
        }
    }
    public void Shoot(Vector3 direction)
    {
        _direction = direction;

        _initialized = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
