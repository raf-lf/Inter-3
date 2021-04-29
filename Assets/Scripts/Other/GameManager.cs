using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class GameManager
{
    [Header("Management")]
    public static bool GamePaused;
    public static bool CantPause;

    [Header("Resources")]
    public static int ItemHeal = 30;
    public static int ItemGrenade = 30;
    public static int[] AmmoClips = { 0, 4, 2 };


    [Header("Collectibles")]
    public static bool[] weaponUpgrades = new bool[5];
    public static bool[,] documents = new bool[4, 4];

    [Header("ScriptManagement")]
    public static GameObject PlayerCharacter;
    public static Hud scriptHud;
    public static Player scriptPlayer;
    public static PlayerWeapons scriptWeapons;
    public static PlayerMovement scriptMovement;
    public static PlayerActions scriptActions;



    public static void PauseGame(bool pause)
    {
        GamePaused = pause;
        CameraFollow.MainCamera.GetComponent<CameraFollow>().PauseCameraOffset(pause);

        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

}
