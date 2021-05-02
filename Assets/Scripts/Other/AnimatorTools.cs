using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTools : MonoBehaviour
{
    public void SetBoolTrue(string boolName)
    {
        GetComponent<Animator>().SetBool(boolName, true);

    }
    public void SetBoolFalse(string boolName)
    {
        GetComponent<Animator>().SetBool(boolName, false);

    }

    public void playSFX(AudioClip clip)
    {
        GameManager.sfxAudioSource.PlayOneShot(clip);

    }


}
