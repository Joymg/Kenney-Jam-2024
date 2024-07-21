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
            if (Base) Base.sprite = newVisuals.Base;
            if (LargeSail) LargeSail.sprite = newVisuals.LargeSail;
            if (Nest) Nest.sprite = newVisuals.Nest;
            if (Flag) Flag.sprite = newVisuals.Flag;
            if (SmallNest) SmallNest.sprite = newVisuals.SmallNest;
        }

        public void SetAlpha(float alpha)
        {
            if (Base) Base.color = new Color(Base.color.r, Base.color.g, Base.color.b, alpha);
            if (LargeSail) LargeSail.color = new Color(LargeSail.color.r, LargeSail.color.g, LargeSail.color.b, alpha);
            if (Nest) Nest.color = new Color(Nest.color.r, Nest.color.g, Nest.color.b, alpha);
            if (Flag) Flag.color = new Color(Flag.color.r, Flag.color.g, Flag.color.b, alpha);
            if (SmallNest) SmallNest.color = new Color(SmallNest.color.r, SmallNest.color.g, SmallNest.color.b, alpha);
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

    [field: SerializeField] public CollisionHandler collisionHandler { get; private set; }


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
        AudioManager.Instance.Play(AudioManager.SFX.Hit);
    }

    protected void SetVisuals()
    {
        visuals.SetVisuals(visualsConfiguration.boatVisualsByDamageStates.First(kvp => kvp.State == state).Visuals);
    }

}



