using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="AD", menuName ="AD")]
public class AudioDictionary : ScriptableObject
{
    public List<AudioEntry> entries;
}

[System.Serializable]
public struct AudioEntry
{
    public AudioManager.SFX SFX;
    public AudioClip clip;

}


