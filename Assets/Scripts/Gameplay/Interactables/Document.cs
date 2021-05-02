using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : Interactible
{
    public int documentCategory;
    public int documentId;

    public override void Interact()
    {
        GetComponent<Animator>().SetBool("active", false);

        GameManager.documents[documentCategory, documentId] = true;

        GameManager.scriptLog.Write(LibraryMenu.LogDocument(LibraryDocument.RetrieveDocumentTitle(documentCategory,documentId)));

    }
}
