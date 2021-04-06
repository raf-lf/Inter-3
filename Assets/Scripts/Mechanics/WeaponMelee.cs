using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : MonoBehaviour
{
    public int damage;
    public float range;
    public float autoDestroyTimer;

    private void Awake()
    {
        autoDestroyTimer *= 1;
    }

    public void DirectionSpeed(int direction)
    {
        switch (direction)
        {
            case 1:
                transform.rotation = Quaternion.Euler(0,  0,  0);
                break;
            case 2:
                transform.rotation = Quaternion.Euler(0, 0,  315);
                break;
            case 3:
                transform.rotation =  Quaternion.Euler(0, 0,  270);
                break;
            case 4:
                transform.rotation =  Quaternion.Euler(0, 0, 225);
                break;
            case 5:
                transform.rotation =  Quaternion.Euler(0, 0, 180);
                break;
            case 6:
                transform.rotation =  Quaternion.Euler(0, 0, 135);
                break;
            case 7:
                transform.rotation =  Quaternion.Euler(0, 0, 90);
                break;
            case 8:
                transform.rotation =  Quaternion.Euler(0, 0, 45);
                break;
        }
        transform.Translate(Vector3.up * range);
    }


    public void Blast()
    {
        Destroy(this.gameObject);
    }


    void FixedUpdate()
    {
        //transform.position = Player.PlayerCharacter.transform.position;
        if (autoDestroyTimer <= 0) Blast();
        else autoDestroyTimer--;
    }
}
