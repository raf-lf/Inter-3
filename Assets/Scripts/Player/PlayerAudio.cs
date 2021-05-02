using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Movement")]
    public int floorType;
    public AudioClip[] stepSfxDirt = new AudioClip[3];
    public AudioClip[] stepSfxStone = new AudioClip[3];

    [Header("Weapons")]
    public AudioClip[] sfxReload = new AudioClip[1];
    public AudioClip[] sfxNoAmmo = new AudioClip[1];

    public void playSFX(AudioClip[] audioClip, float volume, Vector2 pitchVariance)
    {
        GameManager.sfxAudioSource.volume = volume;
        GameManager.sfxAudioSource.pitch = Random.Range(pitchVariance.x, pitchVariance.y);
        GameManager.sfxAudioSource.PlayOneShot(audioClip[(int)Random.Range(0, audioClip.Length)]);

    }

    public void StepSfx(float volume)
    {
        GameManager.sfxAudioSource.volume = volume;
        GameManager.sfxAudioSource.pitch = Random.Range(.9f, 1.1f);
        switch (floorType)
        {
            case 0:
                GameManager.sfxAudioSource.PlayOneShot(stepSfxDirt[(int)Random.Range(0, 3)]);
                break;
            case 1:
                GameManager.sfxAudioSource.PlayOneShot(stepSfxStone[(int)Random.Range(0, 3)]);
                break;
        }
    }

}
