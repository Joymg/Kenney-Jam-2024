using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Dictionary", menuName = "Enemy Dictionary")]
public class EnemyDictionary : ScriptableObject
{
    public List<EnemyCost> enemyCosts;
}

[System.Serializable]
public struct EnemyCost
{
    public int cost;
    public EnemyBoat.EnemyType enemyType;
    public EnemyBoat enemy;
}
