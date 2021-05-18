using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    public Switch[] switchesConnected;
    public int switchesNeeded;
    [SerializeField]
    private int switchesActive;

    void OpenDoor()
    {
        if (GetComponent<Animator>().GetBool("active") == false) GetComponent<Animator>().SetBool("active", true);

    }
    void CloseDoor()
    {
        if (GetComponent<Animator>().GetBool("active")) GetComponent<Animator>().SetBool("active", false);

    }

    void Update()
    {
        switchesActive = 0;

        for (int i = 0; i < switchesConnected.Length; i++)
        {
            if (switchesConnected[i].isActive) switchesActive++;
        }
        if (switchesActive >= switchesNeeded) OpenDoor();
        else CloseDoor();
    }
}
