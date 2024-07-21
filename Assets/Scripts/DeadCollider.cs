using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<BoatController>(out _))
        {
            GameManager.OnGameOver?.Invoke();
        }
    }
}
