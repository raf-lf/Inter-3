using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibraryMenu
{
    public static string ReturnUpgradeInfo(bool ignoraAvaiability, bool returnTitle, int upgradeId)
    {
        if (returnTitle)
        {
            if (GameManager.weaponUpgrades[upgradeId])
            {
                switch (upgradeId)
                {
                    case 0: return "Pente Duplo";
                    case 1: return "Cano Tripartido";
                    case 2: return "Propulsor Magnético";
                    case 3: return "Canhão Iônico";
                    case 4: return "Instinto Assassino";
                    default: return null;
                }
            }
            else return "Melhoria Não-Encontrada";
        }
        else
        {
            if (GameManager.weaponUpgrades[upgradeId])
            {
                switch (upgradeId)
                {
                    case 0: return 
                            "Aumenta de 6 para 10 a quantidade de munição que cabe em um único clipe da pistola.\nTambém melhora a eficiência dos disparos, " +
                            "então cada clipe encontrado fornece a mesma quantidade adicional de munição";
                    case 1: return "";
                    case 2: return "Disparos tem 15 % de chance de causar dano adicional.";
                    case 3: return "";
                    case 4: return "";
                    default: return null;
                    }
                }
            else return "A melhoria para esta arma ainda não foi encontrada.";
        }
    }

    public static string ReturnMedalInfo(bool ignoraAvaiability, bool returnTitle, int upgradeId)
    {
        if (returnTitle)
        {
            if (GameManager.weaponUpgrades[upgradeId])
            {
                switch (upgradeId)
                {
                    case 0: return "Natureza Verdadeira";
                    case 1: return "Colecionador 5 Estrelas";
                    case 2: return "Mestre Acumulador";
                    default: return null;
                }
            }
            else return "Medalha Desconhecida";
        }
        else
        {
            if (GameManager.weaponUpgrades[upgradeId])
            {
                switch (upgradeId)
                {
                    case 0:
                        return
                        "Descobriu a verdadeira natureza de Quila e dos oscilantes.";
                    case 1: return "Encontrou todos os documentos e melhorias de armas no jogo!";
                    case 2: return "Acumulou um total de 50 objetos entre granadas e pentes de munição!";
                    default: return null;
                }
            }
            else return "Medalha ainda não conquistada.";
        }
    }
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
                return "Melhoria de Pistola: '" + ReturnUpgradeInfo(true,true,0) + "' instalada";
            case 1:
                return "Modificação de Pistola: '" + ReturnUpgradeInfo(true,true, 1) + "' instalada";
            case 2:
                return "Melhoria de Rifle: '" + ReturnUpgradeInfo(true, true, 2) + "' instalada";
            case 3:
                return "Modificação de Rifle: '" + ReturnUpgradeInfo(true, true, 3) + "' instalada";
            case 4:
                return "Melhoria Corpo-a-corpo: '" + ReturnUpgradeInfo(true, true, 4) + "' instalada";
            default:
                return null;
        }
    }
        public static string LogDocument(string documentName)
    {
        return "Documento '" + documentName + "' encontrado";

    }
}
