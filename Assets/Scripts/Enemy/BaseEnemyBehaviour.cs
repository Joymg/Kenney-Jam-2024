using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyBehaviour
{
    public Vector2 BehaviourDirection {  get; protected set; }

    public abstract void Tick();

}
