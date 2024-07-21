using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum SFX { Shoot, Hit }
    public static AudioManager Instance;
    [SerializeField] private AudioDictionary AudioDictionary;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void Play(SFX sfx)
    {
        audioSource.pitch = Random.Range(.9f, 1.1f);
        audioSource.clip = AudioDictionary.entries.First(entry => entry.SFX == sfx).clip;
        audioSource.Play();
    }
}
