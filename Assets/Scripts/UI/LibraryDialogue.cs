using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibraryDialogue
{
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
                            case 1: return ("Hello there! This is just a test! This is also the last line. AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            default: return null;
                        }

                    case 2:
                        switch (lineId)
                        {
                            case 1: return ("Hello there! This is just a test! This is also the first line. AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            case 2: return ("And this is the last line! AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                            default: return null;
                        }
                    default: 
                        return null;

                }
            
            default: 
                return null;


        }        
    }

    public static string RetrieveChatter(int level, int chatId, int lineId)
    {
        switch (level)
        {
            case 1:
                switch (chatId)
                {
                    case 1:
                        switch (lineId)
                        {
                            case 1: return ("This is just a quick chat!");
                            default: return null;
                        }

                    case 2:
                        switch (lineId)
                        {
                            case 1: return ("Is this the first line of the chat?");
                            case 2: return ("Indeed!");
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
