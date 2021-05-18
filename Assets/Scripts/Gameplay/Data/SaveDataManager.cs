using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    private static bool pastInitialSetup;
    public static int currentCheckpointId;
    public static Vector3 playerSpawnPosition;

    public static List<int> rememberedIdsToLoad = new List<int>();
    public static List<int> checkpointsUsed = new List<int>();
    public static List<int> cutscenesSeen = new List<int>();
    public static List<int> commentsSeen = new List<int>();

    [Header("Starting Resources")]
    public int startHeals = 5;
    public int startGrenades = 1;
    public int[] startAmmoClips = { 0, 3, 0 };

    [Header("Saved Resources")]
    private static int savedHeals;
    private static int savedGrenades;
    private static int[] savedAmmoClips = { 0, 0, 0 };
    private static int[] savedAmmo = { 0, 0, 0 };

    private void Start()
    {
        GameManager.scriptSaveData = GetComponent<SaveDataManager>();

        if (pastInitialSetup == false)
        {
            playerSpawnPosition = GameManager.PlayerCharacter.transform.position;

            SetStartingResources();
            pastInitialSetup = true;
        }
        else
        {
            LoadData();
        }
    }

    public static void SetCheckpoint(int id, Vector3 checkpointPosition)
    {
        playerSpawnPosition = checkpointPosition;
        currentCheckpointId = id;
        checkpointsUsed.Add(id);
        SaveData();

    }

    public static void SaveData()
    {
        SaveResources();
        SaveInteractibles();

    }

    public static void LoadData()
    {
        //Teleport player to current checkpoint
        GameManager.PlayerCharacter.transform.position = playerSpawnPosition;
        Camera.main.gameObject.transform.position = playerSpawnPosition + GameManager.scriptCamera.offset;

        LoadResources();
        LoadInteractibles();
        TurnOffSeenCutscenes();
        TurnOffSeenComments();
    }

    public void SetStartingResources()
    {
        if (pastInitialSetup == false)
        {
            GameManager.ItemHeal = startHeals;
            GameManager.ItemGrenade = startGrenades;

            for (int i = 0; i < startAmmoClips.Length; i++)
            {
                GameManager.AmmoClips[i] = startAmmoClips[i];
            }
        }

    }
    private static void SaveResources()
    {
        savedHeals = GameManager.ItemHeal;
        savedGrenades = GameManager.ItemGrenade;

        for (int i = 0; i < GameManager.AmmoClips.Length; i++)
        {
            savedAmmoClips[i] = GameManager.AmmoClips[i];
            savedAmmo[i] = PlayerWeapons.ammo[i];
        }
    }

    private static void SaveInteractibles()
    {
        Interactible[] allInteractibles = FindObjectsOfType<Interactible>();

        foreach (Interactible interactible in allInteractibles)
        {
            //Objects with remembered Id = 0 are not recorded!
            if (interactible.rememberOnLoad)
            {
                rememberedIdsToLoad.Add(interactible.rememberedInteractibleId);
                print(interactible.name + " was saved!");
            }

        }
    }

    private static void LoadResources()
        {
            GameManager.ItemHeal = savedHeals;
            GameManager.ItemGrenade = savedGrenades;

            for (int i = 0; i < GameManager.AmmoClips.Length; i++)
            {
                GameManager.AmmoClips[i] = savedAmmoClips[i];
                PlayerWeapons.ammo[i] = savedAmmo[i];

            }
        }

    private static void LoadInteractibles()
    {

        Interactible[] allInteractibles = FindObjectsOfType<Interactible>();

        foreach (Interactible interactible in allInteractibles)
        {
            //Objects with remembered Id = 0 are not recorded!
            if (rememberedIdsToLoad.Contains(interactible.rememberedInteractibleId))
            {
                interactible.RememberLoad();
                print(interactible.name + " was loaded!");
            }

        }

    }

    private static void TurnOffSeenCutscenes()
    {
        Cutscene[] allCutscenes = FindObjectsOfType<Cutscene>();

        foreach (Cutscene cutscene in allCutscenes)
        {
            if (cutscenesSeen.Contains(cutscene.cutsceneSaveId)) cutscene.off = true;
        }

    }
    private static void TurnOffSeenComments()
    {
        CallCommentLog[] allComments = FindObjectsOfType<CallCommentLog>();

        foreach (CallCommentLog comment in allComments)
        {
            if (commentsSeen.Contains(comment.commentSaveId)) comment.off = true;
        }

    }
}
