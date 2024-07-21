using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightEnemyBehavior : BaseEnemyBehaviour
{
    public override void Tick()
    {
        BehaviourDirection = -Vector2.up;
    }
}
