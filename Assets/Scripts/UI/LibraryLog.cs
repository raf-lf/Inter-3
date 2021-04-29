using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibraryLog
{
    public static string LogAmmo(int weaponType, int qty)
    {
        string weaponName = "";

        switch (weaponType)
        {
            case 1:
                weaponName = "Pistola";
                break;
            case 2:
                weaponName = "Rifle";
                break;
        }

        if (qty == 1) return "Pente de munição de " + weaponName + " encontrado";
        else return qty + " pentes de munição de " + weaponName + " encontrado";

    }

    public static string LogGrenade(int qty)
    {
        if (qty == 1) return "Granada encontrada";
        else return qty + " granadas encontradas";
    }

    public static string LogHeal(int qty)
    {
        if (qty == 1) return "Cura recuperada";
        else return qty + " curas recuperadas";
    }

    public static string LogMultiple()
    {
        return "Múltiplos itens encontrados";
    }

    public static string LogUpgrade(int upgradeId)
    {
        switch (upgradeId)
        {
            case 0:
                return "Melhoria de Pistola: 'Pente Aumentado' instalada";
            case 1:
                return "Modificação de Pistola: 'Cano Triplicador' instalada";
            case 2:
                return "Melhoria de Rifle: 'Amplificador' instalada";
            case 3:
                return "Modificação de Rifle: 'Canalizador Eletromagnético' instalada";
            case 4:
                return "Melhoria Corpo-a-corpo: 'Frenesi Oscilante' instalada";
            default:
                return null;
        }
    }
        public static string LogDocument(string documentName)
    {
        return "Documento '" + documentName + "' encontrado";

    }
}
