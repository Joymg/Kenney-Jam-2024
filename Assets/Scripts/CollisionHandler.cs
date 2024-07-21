using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    public UnityEvent OnMeleeOverlapEnterEvent;
    public UnityEvent OnMeleeOverlapExitEvent;
    public UnityEvent OnProjectileOverlapEnterEvent;
    public UnityEvent OnProjectileOverlapExitEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            OnMeleeOverlapEnterEvent?.Invoke();

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") && gameObject.layer != LayerMask.NameToLayer("Enemy"))
            OnMeleeOverlapEnterEvent?.Invoke();
    }
}
