using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class LibraryDialogue
{
    public static int currentPortraitId;
    public static Sprite[] characterPortrait = new Sprite[7];

    public static string RetrieveDialogue(int level, int chatId ,int lineId)
    {
        switch (level)
        {
            case 1:
                switch(chatId)
                {
                    case 1:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0;
                                return ("Hello there! This is just a test! This is also the last line. AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            default: return null;
                        }

                    case 2:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 1;
                                return ("Hello there! This is just a test! This is also the first line. AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            case 2:
                                currentPortraitId = 2; 
                                return ("And this is the last line! AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            default: return null;
                        }
                    default: 
                        return null;

                }
            
            default: 
                return null;


        }        
    }

    public static string RetrieveComment(int level, int chatId, int lineId)
    {
        switch (level)
        {
            case 1:
                switch (chatId)
                {
                    case 1:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 3; 
                                return ("This is just a quick chat!");
                            default: return null;
                        }

                    case 2:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0; 
                                return ("Is this the first line of the chat?");
                            case 2:
                                currentPortraitId = 2; 
                                return ("Indeed!");
                            default: return null;
                        }
                    default: 
                        return null;

                }

            default: 
                return null;


        }
    }
}
