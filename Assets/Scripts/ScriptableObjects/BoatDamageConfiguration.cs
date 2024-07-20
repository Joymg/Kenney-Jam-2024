using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boat Dmg Config", menuName = "Boat Dmg Config")]
public class BoatDamageConfiguration : ScriptableObject
{
    public List<BoatVisualsByDamageState> boatVisualsByDamageStates = new();
}
[System.Serializable]
public struct BoatVisualsByDamageState
{
    public Boat.State State;
    public Boat.Visuals Visuals;
}
