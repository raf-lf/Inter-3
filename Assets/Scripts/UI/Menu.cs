using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Animator submenuAnim;
    [SerializeField]
    private Animator inventoryAnim;
    [SerializeField]
    private Animator descriptionAnim;
    [SerializeField]
    private Animator optionsAnim;

    public void OptionsOpen()
    {
        inventoryAnim.SetInteger("position", 1);
        optionsAnim.SetBool("active", true);

    }

    public void OptionsClose()
    {
        inventoryAnim.SetInteger("position", 0);
        optionsAnim.SetBool("active", false);

    }

    public void DescriptionOpen()
    {
            inventoryAnim.SetInteger("position", -1);
            descriptionAnim.SetBool("active", true);
    }

    public void DescriptionClose()
    {
        inventoryAnim.SetInteger("position", 0);
        descriptionAnim.SetBool("active", false);
    }

    public void InventoryOpen()
    {
       // Cursor.visible = true;
        inventoryAnim.SetBool("active", true);
        submenuAnim.SetBool("active", true);

    }
    public void InventoryClose()
    {
      //  Cursor.visible = false;
        inventoryAnim.SetBool("active", false);
        inventoryAnim.SetInteger("position", 0);
        submenuAnim.SetBool("active", false);
        descriptionAnim.SetBool("active", false);
        optionsAnim.SetBool("active", false);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(inventoryAnim.GetBool("active")) InventoryClose();
            else InventoryOpen();
        }
        
    }
}
