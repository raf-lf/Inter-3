using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class GameManager
{
    [Header("Management")]
    public static bool GamePaused;
    public static bool CantPause;
    public static bool CutscenePlaying;

    [Header("Resources")]
    public static int ItemHeal = 5;
    public static int ItemGrenade = 1;
    public static int[] AmmoClips = { 0, 3, 0 };


    [Header("Collectibles")]
    public static bool[] weaponUpgrades = new bool[5];
    public static bool[,] documents = new bool[4, 4];
    public static bool[] medals = new bool[3];

    [Header("Script Management")]
    public static GameObject PlayerCharacter;
    public static Player scriptPlayer;
    public static PlayerWeapons scriptWeapons;
    public static PlayerMovement scriptMovement;
    public static PlayerActions scriptActions;
    public static PlayerAudio scriptPlayerAudio;

    public static CameraFollow scriptCamera;
    public static AudioManager scriptAudio;
    public static Hud scriptHud;
    public static MenuUi scriptMenu;

    public static TextLog scriptLog;
    public static TextComment scriptComment;
    public static TextDialogue scriptDialogue;
    public static Cutscene currentCutscene;


    public static void PauseGame(bool pause)
    {
        GamePaused = pause;
        scriptCamera.PauseCameraOffset(pause);

        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public static void Cutscene(bool on)
    {
        CutscenePlaying = on;
        Player.PlayerControls = !on;
        scriptMovement.HaltMovement(on);

    }

}
