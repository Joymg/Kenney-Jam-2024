using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boat : MonoBehaviour
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

    public enum State
    {
        Intact, Hurt, Damaged, Sunk
    }

    [Header("References")]
    [SerializeField] protected GameObject boat;
    [SerializeField] protected Visuals visuals;
    [SerializeField] protected State state;
    [SerializeField] protected BoatDamageConfiguration visualsConfiguration;
    [SerializeField] protected Rigidbody2D rb;


    [Header("Fields")]
    [SerializeField] protected float health = 3;

    protected virtual void Awake()
    {

    }

}



