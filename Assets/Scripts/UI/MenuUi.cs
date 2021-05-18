using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuUi : MonoBehaviour
{
    private bool menuOpen;

    public Animator submenuAnim;
    public Animator inventoryAnim;
    public Animator descriptionAnim;
    public Animator optionsAnim;

    public AudioSource sfxSource;
    public AudioSource bgmSource;
    public GameObject overlay;

    public Text descriptionBoxText;
    public Text descriptionBoxTitle;

    public Selectable[] weaponUpgradeBox = new Selectable[5];
    public Selectable[,] documentBox = new Selectable[4, 4];
    public Selectable[] medalBox = new Selectable[3];

    public Sprite[] characterPortraitSprites = new Sprite[7];

    private void Start()
    {
        GameManager.scriptMenu = GetComponent<MenuUi>();
        GameManager.overlay = overlay;

        for (int i = 0; i < LibraryDialogue.characterPortrait.Length; i++)
        {
            LibraryDialogue.characterPortrait[i] = characterPortraitSprites[i];
        }

    }

    public void UpdateMenuSelectables()
    {
        for (int i = 0; i < weaponUpgradeBox.Length; i++)
        {
            if (GameManager.weaponUpgrades[i]) weaponUpgradeBox[i].interactable = true;
            else weaponUpgradeBox[i].interactable = false;

        }

        for (int i = 0; i < medalBox.Length; i++)
        {
            if (GameManager.medals[i]) medalBox[i].interactable = true;
            else medalBox[i].interactable = false;

        }

        for (int i1 = 0; i1 < documentBox.GetLength(0); i1++)
        {
            //Check if all 3 documents of each collecion have been recovered
            if (GameManager.documents[i1, 0] && GameManager.documents[i1, 1] && GameManager.documents[i1, 2]) GameManager.documents[i1, 3] = true;

            for (int i2 = 0; i2 < documentBox.GetLength(1); i2++)
            {
                if (GameManager.documents[i1, i2]) documentBox[i1, i2].interactable = true;
                else documentBox[i1, i2].interactable = false;

            }
        }

    }

    public void InventoryOpen()
    {
        descriptionBoxTitle.text = null;
        descriptionBoxText.text = null;
        inventoryAnim.SetBool("active", true);
        submenuAnim.SetBool("active", true);
        // Cursor.visible = true;

        UpdateMenuSelectables();


    }
    public void InventoryClose()
    {

        inventoryAnim.SetBool("active", false);
        submenuAnim.SetBool("active", false);
        //  Cursor.visible = false;

    }

    public void OptionsOpen()
    {
        optionsAnim.SetBool("active", true);

        if (descriptionAnim.GetBool("active")) DescriptionClose();

    }

    public void OptionsClose()
    {
        optionsAnim.SetBool("active", false);

    }

    public void DescriptionOpen()
    {
        descriptionAnim.SetBool("active", true);

        if (optionsAnim.GetBool("active")) OptionsClose();
    }

    public void DescriptionClose()
    {
        descriptionAnim.SetBool("active", false);
    }

    public void MenuOpen()
    {
        InventoryOpen();

        GameManager.PauseGame(true);

        menuOpen = true;
    }
    public void MenuClose()
    {
        if (descriptionAnim.GetBool("active")) DescriptionClose();
        if (optionsAnim.GetBool("active")) OptionsClose();
        
        InventoryClose();

        GameManager.PauseGame(false);

        menuOpen = false;
    }

    void Update()
    {
        if (menuOpen)
        { 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuClose();
            }
        }

        else if (menuOpen == false && Input.GetKeyDown(KeyCode.Escape))
        {
            MenuOpen();
        }

    }

}
