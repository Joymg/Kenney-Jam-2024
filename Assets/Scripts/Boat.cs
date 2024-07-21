using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Boat : MonoBehaviour, IDamageable
{
    [System.Serializable]
    public struct Visuals
    {
        [field: SerializeField] public Sprite Base { get; private set; }
        [field: SerializeField] public Sprite LargeSail { get; private set; }
        [field: SerializeField] public Sprite Nest { get; private set; }
        [field: SerializeField] public Sprite Flag { get; private set; }
        [field: SerializeField] public Sprite SmallNest { get; private set; }
    }

    [System.Serializable]
    public struct SpriteRenderers
    {
        [field: SerializeField] public SpriteRenderer Base { get; private set; }
        [field: SerializeField] public SpriteRenderer LargeSail { get; private set; }
        [field: SerializeField] public SpriteRenderer Nest { get; private set; }
        [field: SerializeField] public SpriteRenderer Flag { get; private set; }
        [field: SerializeField] public SpriteRenderer SmallNest { get; private set; }

        public void SetVisuals(Visuals newVisuals)
        {
            Base.sprite = newVisuals.Base;
            LargeSail.sprite = newVisuals.LargeSail;
            Nest.sprite = newVisuals.Nest;
            Flag.sprite = newVisuals.Flag;
            SmallNest.sprite = newVisuals.SmallNest;
        }
    }

    public enum State
    {
        Intact, Hurt, Damaged, Sunk
    }

    [Header("References")]
    [SerializeField] protected GameObject boat;
    [SerializeField] protected State state;
    [SerializeField] protected SpriteRenderers visuals;
    [SerializeField] protected BoatDamageConfiguration visualsConfiguration;
    [SerializeField] protected Rigidbody2D rb;


    [Header("Fields")]
    [SerializeField] protected float health = 3;

    protected virtual void Awake()
    {
        SetVisuals();
    }

    public virtual void GetDamaged()
    {
        Debug.Log($"{gameObject.name}: Damaged");
        health--;
        switch (health)
        {
            case 2:
                state = State.Hurt;
                break;
            case 1:
                state = State.Damaged;
                break;
            case 0:
                state = State.Sunk;
                break;
        }

        SetVisuals();
    }

    private void SetVisuals()
    {
        visuals.SetVisuals(visualsConfiguration.boatVisualsByDamageStates.First(kvp => kvp.State == state).Visuals);
    }
}



