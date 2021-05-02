using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public bool playOnAwake;
    public float volume;
    public Vector2 pitchVariance;
    public AudioClip[] audioClip = new AudioClip[1];

    private void Awake()
    {
        if (playOnAwake) playSFX();
        
    }

    public void playSFX()
    {
        GameManager.sfxAudioSource.volume = volume;
        GameManager.sfxAudioSource.pitch = Random.Range(pitchVariance.x, pitchVariance.y);
        GameManager.sfxAudioSource.PlayOneShot(audioClip[(int)Random.Range(0,audioClip.Length)]);

    }
}
