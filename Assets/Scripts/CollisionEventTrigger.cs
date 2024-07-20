using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEventTrigger : MonoBehaviour
{
    public LayerMask layerMask;
    public UnityEvent<Collider2D> OnOverlapEnter;
    public UnityEvent<Collider2D> OnOverlapExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boat"))
            OnOverlapEnter?.Invoke(collision.collider);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 1 << layerMask)
            OnOverlapExit?.Invoke(collision.collider);
    }

}
