using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Group Dictionary", menuName = "Group Dictionary")]
public class GroupDictionary : ScriptableObject
{
    public List<GroupSizeByEnemyType> groupSizesByEnemyType;
}

[System.Serializable]
public struct GroupSizeByEnemyType
{
    public EnemyBoat.EnemyType EnemyType;
    [Range(0, 1)] public float Probability;
    public int MinEnemyCount;
    public int MaxEnemyCount;
}
