using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEventTrigger : MonoBehaviour
{
    public UnityEvent<Collider2D> OnOverlapEnter;
    public UnityEvent<Collider2D> OnOverlapExit;

    private void OnCollisionEnter2D(Collision2D collision) => OnOverlapEnter?.Invoke(collision.collider);
    private void OnCollisionExit2D(Collision2D collision) => OnOverlapExit?.Invoke(collision.collider);
}
