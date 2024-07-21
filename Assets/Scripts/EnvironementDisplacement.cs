using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironementDisplacement : MonoBehaviour
{
    [SerializeField] public float speed = 3;

    private bool _finishGame = false;

    private void Update()
    {
        if (transform.position.y > -170f)
        {
            transform.position += speed * Time.deltaTime * -Vector3.up;
        }
        else
        {
            if (!_finishGame)
            {
                _finishGame = true;
                GameManager.OnVictory?.Invoke();
            }
        }
    }
}
