using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class MenuUi : MonoBehaviour, IPointerClickHandler
{
    public Animator submenuAnim;
    public Animator inventoryAnim;
    public Animator descriptionAnim;
    public Animator optionsAnim;

    public Text descriptionBoxText;
    public Text descriptionBoxTitle;

    public Selectable[] weaponUpgradeBox = new Selectable[5];
    public Selectable[,] documentBox = new Selectable[4, 4];


    public void InventoryOpen()
    {
        GameManager.PauseGame(true);

        descriptionBoxTitle.text = null;
        descriptionBoxText.text = null;
        // Cursor.visible = true;
        inventoryAnim.SetBool("active", true);
        submenuAnim.SetBool("active", true);

        for (int i1 = 0; i1 < documentBox.GetLength(0); i1++)
        {
            //Check if all 3 documents of each collecion have been recovered
            if (GameManager.documents[i1, 0] && GameManager.documents[i1, 1] && GameManager.documents[i1, 2]) GameManager.documents[i1, 3] = true;

            for (int i2 = 0; i2 < documentBox.GetLength(1); i2++)
            {
                if (GameManager.documents[i1,i2]) documentBox[i1,i2].interactable = true;
                else documentBox[i1,i2].interactable = false;

            }

        }

    }
    public void InventoryClose()
    {
        GameManager.PauseGame(false);

        //  Cursor.visible = false;
        inventoryAnim.SetBool("active", false);
        submenuAnim.SetBool("active", false);
        descriptionAnim.SetBool("active", false);
        optionsAnim.SetBool("active", false);

        for (int i1 = 0; i1 < documentBox.GetLength(0); i1++)
        {
            for (int i2 = 0; i2 < documentBox.GetLength(1); i2++)
            {
                documentBox[i1, i2].interactable = false;

            }

        }
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (descriptionAnim.GetBool("active")|| optionsAnim.GetBool("active"))
            {
                OptionsClose();
                DescriptionClose();
            }
            else if (inventoryAnim.GetBool("active")) InventoryClose();
            else InventoryOpen();
        }

    }

}
