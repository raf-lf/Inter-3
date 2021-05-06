using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource source;
    public Vector2 standartPitchVariance = new Vector2(.7f,1.3f);

    [Header("Movement")]
    public AudioClip[] ladderStep = new AudioClip[3];
    public AudioClip[] jumpUp = new AudioClip[2];
    public AudioClip[] jumpLand = new AudioClip[3];
    public AudioClip[] roll = new AudioClip[1];

    [Header("Footsteps")]
    public int floorType;
    public AudioClip[] stepSfxDirt = new AudioClip[3];
    public AudioClip[] stepSfxStone = new AudioClip[3];
    public AudioClip[] stepSfxMetal = new AudioClip[3];

    [Header("Weapons")]
    public AudioClip[] swap = new AudioClip[3];
    public AudioClip[] reload = new AudioClip[3];
    public AudioClip[] noAmmo = new AudioClip[3];


    public void playSFX(AudioClip[] audioClip, float volume, Vector2 pitchVariance)
    {
        source.volume = volume * GameManager.volumeSFX;
        source.pitch = Random.Range(pitchVariance.x, pitchVariance.y);
        source.PlayOneShot(audioClip[(int)Random.Range(0, audioClip.Length)]);

    }

    public void SfxSwap()
    {
        source.volume = .5f * GameManager.volumeSFX;
        source.pitch = Random.Range(standartPitchVariance.x, standartPitchVariance.y);
        source.PlayOneShot(swap[PlayerWeapons.equipedWeapon]);
    }
    public void SfxReload()
    {
        source.volume = 1 * GameManager.volumeSFX;
        source.pitch = Random.Range(standartPitchVariance.x, standartPitchVariance.y);
        source.PlayOneShot(reload[PlayerWeapons.equipedWeapon]);
    }
    public void SfxNoAmmo()
    {
        source.volume = 1 * GameManager.volumeSFX;
        source.pitch = Random.Range(standartPitchVariance.x, standartPitchVariance.y);
        source.PlayOneShot(noAmmo[PlayerWeapons.equipedWeapon]);
    }

    public void StepSfx(float volume)
    {
        source.volume = volume * GameManager.volumeSFX;
        source.pitch = Random.Range(.9f, 1.1f);
        switch (floorType)
        {
            case 0:
                source.PlayOneShot(stepSfxDirt[(int)Random.Range(0, 3)]);
                break;
            case 1:
                source.PlayOneShot(stepSfxStone[(int)Random.Range(0, 3)]);
                break;
            case 2:
                source.PlayOneShot(stepSfxMetal[(int)Random.Range(0, 3)]);
                break;
        }
    }
    public void LadderStepSfx()
    {
        playSFX(ladderStep, 1, standartPitchVariance);
    }
    public void JumpUpSfx()
    {
        playSFX(jumpUp, 1, standartPitchVariance);
    }
    public void JumpLandSfx()
    {
        playSFX(jumpLand, 1, standartPitchVariance);

        StepSfx(2);
    }
    public void RollSfx()
    {
        playSFX(roll, 1, standartPitchVariance);
    }

}
