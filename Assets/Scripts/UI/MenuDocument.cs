using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MenuDocument : MonoBehaviour,IPointerClickHandler
{
    public int documentCategory;
    public int documentId;
    private MenuUi menuScript;

    void Start()
    {
        menuScript = GetComponentInParent<MenuUi>();
        menuScript.documentBox[documentCategory, documentId] = GetComponent<Selectable>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (menuScript.descriptionAnim.GetBool("active") == false)
        {
            menuScript.DescriptionOpen();
        }
        menuScript.descriptionBoxTitle.text = LibraryDocument.RetrieveDocumentTitle(documentCategory, documentId);
        menuScript.descriptionBoxText.text = LibraryDocument.RetrieveDocumentText(documentCategory, documentId);
    }
}
