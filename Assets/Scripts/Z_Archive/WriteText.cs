using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
public class WriteText : MonoBehaviour
{

    [Header("Typewritter")]
    public float characterIntervalTime = 0.01f;
    public float timeDelayPerCharacter = 0.1f;
    public float chatterDelayTimer = 3;
    public float logDelayTimer = 2;

    [Header("Text Boxes")]
    public Animator[] textBoxAnimator = new Animator[3];
    public Text[] textBoxText = new Text[3];
    public Image[] textBoxPortrait = new Image[2];
    public Sprite[] characterPortrait = new Sprite[7];

    private string[] textToWrite = new string[3];
    private bool[] onLastLine = new bool [2];

    private bool dialogueSkip;
    private bool dialogueNext;
    private bool chatterEnded;

    private int dialogueType = 0;

    private int[] currentLevel = new int[3];
    private int[] currentDialogue = new int[3];
    private int[] currentLine = new int[3];


    private void Start()
    {
        GameManager.scriptWriteText = GetComponent<WriteText>();
    }

    public bool CheckIfLastLine(int lv, int d, int l)
    {
        if (LibraryDialogue.RetrieveDialogue(lv, d, l) == null) return true;
        else return false;
    }

    public void DialogueWrite(int lv, int d, int l)
    {
        currentLevel[0] = lv;
        currentDialogue[0] = d;
        currentLine[0] = l;

        dialogueType = 1;

        onLastLine[0] = CheckIfLastLine(lv, d, l + 1);

        textToWrite[0] = LibraryDialogue.RetrieveDialogue(lv, d, l);

        //Interrupt other messages.
        StopAllCoroutines();
        textBoxAnimator[1].SetBool("active", false);
        textBoxAnimator[2].SetBool("active", false);

        if (textBoxAnimator[0].GetBool("active") == false)
        {
            textBoxAnimator[0].SetBool("active", true);
            Player.PlayerControls = false;
            GameManager.scriptMovement.HaltMovement();
        }

        dialogueSkip = true;

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

        chatterDelayTimer = textToWrite.Length * timeDelayPerCharacter;

        StopCoroutine(DialogueWrite());
        StopCoroutine(ChatterWrite());
        StartCoroutine(ChatterWrite());
    }

    /*
    public void LogWrite(string logToWrite)
    {
        dialogueType = 3;

        finalLine = true;

        logText.text = logToWrite;

        if (logBox.GetComponent<Animator>().GetBool("active") == false) logBox.GetComponent<Animator>().SetBool("active", true);

        logDelayTimer = logToWrite.Length * timeDelayPerCharacter + Time.time;
    }
    */
/*
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
/*
    }

/*
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

            yield return new WaitForSeconds(characterIntervalTime);

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
                chatterDelayTimer += Time.time;
                chatterEnded = true;
              }

              yield return new WaitForSeconds(characterIntervalTime);
          }
        
        yield return new WaitForSeconds(characterIntervalTime);
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
        else if (dialogueType == 2 && Time.time >= chatterDelayTimer && chatterEnded)
        {
            chatterEnded = false;
            LineEnd();
        }
        else if (dialogueType == 3 && Time.time >= logDelayTimer)
        {
            LineEnd();
        }
    }

}
*/