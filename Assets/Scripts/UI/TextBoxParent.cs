using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class TextBoxParent : MonoBehaviour
{
    [Header("Typewritter")]
    public float characterIntervalTime;
    public AudioClip characterSFX;
    protected string textToWrite;
    protected bool canEnd;

    [Header("Text Boxes")]
    public Animator textBoxAnimator;
    public Text textBoxText;
    public Image textBoxPortrait;

    private void SetPortrait()
    {
        if (textBoxPortrait != null) textBoxPortrait.sprite = LibraryDialogue.characterPortrait[LibraryDialogue.currentPortraitId];
    }

    public virtual void Write (string text)
    {
        SetPortrait();

        textBoxAnimator.SetBool("active", true);

        textToWrite = text;

        StopAllCoroutines();
        StartCoroutine(WriteCharacter());

    }

    public IEnumerator WriteCharacter()
    {
        if (textToWrite != null)
        {
            for (int i = 0; i <= textToWrite.Length; i++)
            {
                yield return new WaitForSeconds(characterIntervalTime);

                textBoxText.text = textToWrite.Substring(0, i);

                PlayCharacterSFX();

                if (i == textToWrite.Length) FinishedTypeWriting();

            }
        }

    }

    public virtual void FinishedTypeWriting()
    {
        canEnd = true;


    }

    public void CloseTextBox()
    {
        textBoxAnimator.SetBool("active", false);

    }

    public void PlayCharacterSFX()
    {

    }


}
