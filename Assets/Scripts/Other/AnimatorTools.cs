using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTools : MonoBehaviour
{
    public void AnimatorBoolTrue(string boolName)
    {
        GetComponent<Animator>().SetBool(boolName, true);

    }
    public void AnimatorBoolFalse(string boolName)
    {
        GetComponent<Animator>().SetBool(boolName, false);

    }

    public void playAudioClip(AudioClip clip)
    {
        GameManager.scriptAudio.genericSfxAudioSource.volume = 1 * GameManager.scriptAudio.volumeSfx;
        GameManager.scriptAudio.genericSfxAudioSource.PlayOneShot(clip);

    }


}
