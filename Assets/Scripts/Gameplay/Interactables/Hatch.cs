using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    public Switch switchToActivate;


    void Update()
    {
        if(switchToActivate.isActive)
        {
            if (GetComponent<Animator>().GetBool("active") == false) GetComponent<Animator>().SetBool("active", true);

        }
        else
        {
            if (GetComponent<Animator>().GetBool("active")) GetComponent<Animator>().SetBool("active", false);

        }
        
    }
}
