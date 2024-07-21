using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironementDisplacement : MonoBehaviour
{
    [SerializeField] public float speed = 3;

    private void Update()
    {
        if (transform.position.y < 171f)
        {
            transform.position += speed * Time.deltaTime * -Vector3.up;
        }
    }
}
