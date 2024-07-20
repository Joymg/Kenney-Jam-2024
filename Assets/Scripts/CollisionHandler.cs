using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : MonoBehaviour
{
    public CollisionEventTrigger meleeCollision;
    public CollisionEventTrigger projectileCollision;

    public UnityEvent OnMeleeOverlapEnterEvent;
    public UnityEvent OnMeleeOverlapExitEvent;
    public UnityEvent OnProjectileOverlapEnterEvent;
    public UnityEvent OnProjectileOverlapExitEvent;

    private void Awake()
    {
        meleeCollision.OnOverlapEnter.AddListener(OnMeleeOverlapEnter);
        meleeCollision.OnOverlapExit.AddListener(OnMeleeOverlapExit);

        projectileCollision.OnOverlapEnter.AddListener(OnProjectileOverlapEnter);
        projectileCollision.OnOverlapExit.AddListener(OnProjectileOverlapExit);
    }

    private void OnProjectileOverlapExit(Collider2D arg0)
    {
        throw new NotImplementedException();
    }

    private void OnMeleeOverlapExit(Collider2D arg0)
    {
        throw new NotImplementedException();
    }

    private void OnMeleeOverlapEnter(Collider2D arg0)
    {

    }

    private void OnProjectileOverlapEnter(Collider2D arg0)
    {

    }

}
