using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WriteText : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public GameObject chatterBox;
    public Text chatterText;

    public GameObject logBox;
    public Text logText;

    public float characterInterval = 0.01f;
    private string textToWrite;

    private float logEndTimer = 2;

    public float chatterCharacterDelay = 0.1f;
    private float chatterEndTimer = 3;
    private bool chatterEnded;

    private bool dialogueSkip;
    private bool dialogueNext;
    private bool finalLine;

    private int dialogueType = 0;

    private int[] currentId = new int[3];

    public static WriteText scriptWrite;

    private void Start()
    {
        scriptWrite = GetComponent<WriteText>();
    }

    public bool CheckIfLastLine(int lv, int d, int l)
    {
        if (LibraryDialogue.RetrieveDialogue(lv, d, l) == null) return true;
        else return false;
    }

    public void DialogueWrite(int lv, int d, int l)
    {
        currentId[0] = lv;
        currentId[1] = d;
        currentId[2] = l;

        dialogueType = 1;

        finalLine = CheckIfLastLine(lv, d, l + 1);

        textToWrite = LibraryDialogue.RetrieveDialogue(lv, d, l);

        if (chatterBox.GetComponent<Animator>().GetBool("active") == true) chatterBox.GetComponent<Animator>().SetBool("active", false);

        if (dialogueBox.GetComponent<Animator>().GetBool("active") == false)
        {
            dialogueBox.GetComponent<Animator>().SetBool("active", true);
            Player.PlayerControls = false;
            GameManager.scriptMovement.HaltMovement();
        }

        dialogueSkip = true;

        StopCoroutine(DialogueWrite());
        StopCoroutine(ChatterWrite());
        StartCoroutine(DialogueWrite());
    }
    public void ChatterWrite(int lv, int d, int l)
    {
        currentId[0] = lv;
        currentId[1] = d;
        currentId[2] = l;

        dialogueType = 2;

        finalLine = CheckIfLastLine(lv, d, l + 1);

        textToWrite = LibraryDialogue.RetrieveChatter(lv, d, l);

        if (chatterBox.GetComponent<Animator>().GetBool("active") == false) chatterBox.GetComponent<Animator>().SetBool("active", true);

        chatterEndTimer = textToWrite.Length * chatterCharacterDelay;

        StopCoroutine(DialogueWrite());
        StopCoroutine(ChatterWrite());
        StartCoroutine(ChatterWrite());
    }

    public void LogWrite(string logToWrite)
    {
        dialogueType = 3;

        finalLine = true;

        logText.text = logToWrite;

        if (logBox.GetComponent<Animator>().GetBool("active") == false) logBox.GetComponent<Animator>().SetBool("active", true);

        logEndTimer = logToWrite.Length * chatterCharacterDelay + Time.time;
    }

    private void CharacterSFX()
    {
        /*
        
        if (dialogue)
        {
        }
        else
        {
        }

        */

    }


    IEnumerator DialogueWrite()
    {
        for (int i = 0; i <= textToWrite.Length; i++)
        {

            dialogueText.text = textToWrite.Substring(0, i);
            CharacterSFX();

            if (i == textToWrite.Length)
            {
                dialogueSkip = false;
                dialogueNext = true;
            }

            yield return new WaitForSeconds(characterInterval);

        }
    }

    IEnumerator ChatterWrite()
    {
          for (int i = 0; i <= textToWrite.Length; i++)
          {
              chatterText.text = textToWrite.Substring(0, i);
              CharacterSFX();

              if (i == textToWrite.Length)
              {
                chatterEndTimer += Time.time;
                chatterEnded = true;
              }

              yield return new WaitForSeconds(characterInterval);
          }
        
        yield return new WaitForSeconds(characterInterval);
    }

    private void LineEnd()
    {
        if (finalLine)
        {
            if (dialogueType == 1)
            {
                dialogueBox.GetComponent<Animator>().SetBool("active", false);
                Player.PlayerControls = true;
            }
            else if (dialogueType == 2)
            {
                chatterBox.GetComponent<Animator>().SetBool("active", false);
            }
            else if (dialogueType == 3)
            {
                logBox.GetComponent<Animator>().SetBool("active", false);
            }

            dialogueType = 0;
        }
        else
        {
            if (dialogueType == 1) DialogueWrite(currentId[0], currentId[1], currentId[2] + 1);
            else if (dialogueType == 2) ChatterWrite(currentId[0], currentId[1], currentId[2] + 1);
        }
    }

    private void Update()
    {
        if (dialogueType == 1 && Input.GetMouseButtonDown(0) && GameManager.GamePaused == false)
        {
            if (dialogueSkip)
            {
                StopAllCoroutines();
                dialogueText.text = textToWrite;
                dialogueSkip = false;
                dialogueNext = true;
            }

            else if (dialogueNext)
            {
                dialogueNext = false;
                LineEnd();
            }
        }
        else if (dialogueType == 2 && Time.time >= chatterEndTimer && chatterEnded)
        {
            chatterEnded = false;
            LineEnd();
        }
        else if (dialogueType == 3 && Time.time >= logEndTimer)
        {
            LineEnd();
        }
    }

}
