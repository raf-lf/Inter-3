using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public bool playOnAwake;

    public AudioSource source;
    public float volume;
    public Vector2 pitchVariance;
    public AudioClip[] audioClip = new AudioClip[1];

    private void Awake()
    {
        if (playOnAwake) playSFX();
        
    }

    public void playSFX()
    {
        source.volume = volume * GameManager.volumeSFX;
        source.pitch = Random.Range(pitchVariance.x, pitchVariance.y);
        source.PlayOneShot(audioClip[(int)Random.Range(0,audioClip.Length)]);

    }

}
