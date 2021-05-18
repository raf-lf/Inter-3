using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : Interactible
{
    public int documentCategory;
    public int documentId;

    private void Start()
    {
        //Destroy this item if it was already picked in a previous save
        if (GameManager.documents[documentCategory,documentId]) Destroy(gameObject);

    }

    public override void RememberLoad()
    {
        base.RememberLoad();
        Destroy(gameObject);

    }

    public override void Interact()
    {
        base.Interact();

        GetComponent<Animator>().SetBool("active", false);

        GameManager.documents[documentCategory, documentId] = true;

        GameManager.scriptLog.Write(LibraryMenu.LogDocument(LibraryDocument.RetrieveDocumentTitle(documentCategory,documentId)));

    }
}
