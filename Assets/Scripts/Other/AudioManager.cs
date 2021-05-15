using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]

    public AudioSource bgmAudioSource;
    public AudioSource genericSfxAudioSource;
    public AudioSource genericAmbientAudioSource;

    public float volumeSfx = 1;
    public float volumeBgm = .25f;
    public float volumeAmbient = .75f;
    public float volumeBgmModifier = 1;

    private void Start()
    {
        GameManager.scriptAudio = GetComponent<AudioManager>();
    }

    private void Update()
    {
        genericSfxAudioSource.volume = volumeSfx;
        genericAmbientAudioSource.volume = volumeAmbient;
        bgmAudioSource.volume = volumeBgm * volumeBgmModifier;

    }

    public void MusicOff(float speed)
    {
        GetComponent<Animator>().speed = speed;
        GetComponent<Animator>().SetBool("musicOff", true);
        GetComponent<Animator>().SetBool("max", false);

    }
    public void MusicOn(float speed)
    {
        GetComponent<Animator>().speed = speed;
        GetComponent<Animator>().SetBool("musicOff", false);
        GetComponent<Animator>().SetBool("max", false);

    }
    public void MusicMax()
    {
        GetComponent<Animator>().SetBool("max", true);
    }
}
