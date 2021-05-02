using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MenuItem : MonoBehaviour,IPointerClickHandler
{
    // 0 - Weapon Upgrade, 1 - Documents, 2 - Medals
    public int itemType;
    public int id1;
    public int id2;

    void Start()
    {
        switch (itemType)
        {
            case 0:
                GameManager.scriptMenu.weaponUpgradeBox[id1] = GetComponent<Selectable>();
                break;
            case 1:
                GameManager.scriptMenu.documentBox[id1, id2] = GetComponent<Selectable>();
                break;
            case 2:
                GameManager.scriptMenu.medalBox[id1] = GetComponent<Selectable>();
                break;

        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            if (GameManager.scriptMenu.descriptionAnim.GetBool("active")) GameManager.scriptMenu.DescriptionClose();

            GameManager.scriptMenu.DescriptionOpen();

            switch (itemType)
            {
                //Weapon Upgrades
                case 0:
                    GameManager.scriptMenu.descriptionBoxTitle.text = LibraryMenu.ReturnUpgradeInfo(false, true, id1);
                    GameManager.scriptMenu.descriptionBoxText.text = LibraryMenu.ReturnUpgradeInfo(false, false, id1);
                    break;
                //Documents
                case 1:
                    GameManager.scriptMenu.descriptionBoxTitle.text = LibraryDocument.RetrieveDocumentTitle(id1, id2);
                    GameManager.scriptMenu.descriptionBoxText.text = LibraryDocument.RetrieveDocumentText(id1, id2);
                    break;
                //Medals
                case 2:
                    GameManager.scriptMenu.descriptionBoxTitle.text = LibraryMenu.ReturnMedalInfo(false, true, id1);
                    GameManager.scriptMenu.descriptionBoxText.text = LibraryMenu.ReturnMedalInfo(false, false, id1);
                    break;
            }
        }
    }
}
