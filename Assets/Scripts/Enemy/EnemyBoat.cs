using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyBoat : Boat
{
    public enum EnemyType { Small, Medium, Heavy, Boss };
    public enum BehaviourType { None, Straight, Path, Kamikaze }
    [SerializeField] private BehaviourType behaviourType;
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Collider2D _collider;

    [SerializeField] private SplineAnimate splineAnimator;

    private BaseEnemyBehaviour behaviour;
    private bool justHit;
    private float hitElapsedTime;

    public SplineAnimate SplineAnimator { get => splineAnimator; set => splineAnimator = value; }
    public BehaviourType CurrenBehaviour { get => behaviourType; set => behaviourType = value; }

    public void SetBehaviour(BehaviourType behaviourType)
    {
        this.behaviourType = behaviourType;
        switch (behaviourType)
        {
            case BehaviourType.None:
                behaviour = null;
                break;
            case BehaviourType.Straight:
                behaviour = new StraightEnemyBehavior();
                break;
            case BehaviourType.Path:
                behaviour = new PathEnemyBehavior(SplineAnimator);
                SplineAnimator.enabled = true;
                break;
            case BehaviourType.Kamikaze:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (behaviour == null)
            return;

        behaviour.Tick();
        rb.velocity = behaviour.BehaviourDirection * 5f;
    }

    public override void GetDamaged()
    {
        base.GetDamaged();
        if (health <= 0)
        {
            _collider.enabled = false;
            behaviour = null;
            splineAnimator.enabled = false;
            StartCoroutine(WaitToDestroy());
        }
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
