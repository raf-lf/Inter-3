using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LibraryDocument
{

    private static string noDocumentTitle = "Documento Não-Encontrado";
    private static string noReportTitle = "Relatório: ???";
    private static string noDocument = "O documento ainda não foi encontrado.";
    private static string noReport = "Informações insuficientes para compor um relatório.";
    private static string error = "Algum erro ocorreu...";

    public static string RetrieveDocumentTitle(int category, int id)
    {
        if (id !=3 && GameManager.documents[category, id] == false) return noDocumentTitle;
        else
        {
            switch (category)
            {
                case 0:
                    switch (id)
                    {
                        case 0:
                            return ("Estudo Ambiental");
                        case 1:
                            return ("Ficha Médica de Criança");
                        case 2:
                            return ("Despedida par o Marido");
                        case 3:
                            if (ReportCheck(category)) return ("Relatório: Mundo");
                            else return noReportTitle;
                        default:
                            return error;
                    }
                case 1:
                    switch (id)
                    {
                        case 0:
                            return ("Oração Funerária");
                        case 1:
                            return ("Propagada do Líder");
                        case 2:
                            return ("Estratégia de Combate");
                        case 3:
                            if (ReportCheck(category)) return ("Relatório: Governo");
                                else return noReportTitle;
                        default:
                            return error;
                    }
                case 2:
                    switch (id)
                    {
                        case 0:
                            return ("Anotações do Cientista");
                        case 1:
                            return ("Confirmação de Patrocínio");
                        case 2:
                            return ("Chamado às Armas");
                        case 3:
                            if (ReportCheck(category)) return ("Relatório: Resistência");
                            else return noReportTitle;
                        default:
                            return error;
                    }
                case 3:
                    switch (id)
                    {
                        case 0:
                            return ("Estudo Evolutivo");
                        case 1:
                            return ("Relatório de Engenharia");
                        case 2:
                            return ("Lista de Alvos");
                        case 3:
                            if (ReportCheck(category)) return ("Relatório: Oscilantes");
                            else return noReportTitle;
                        default:
                            return error;
                    }
                default: 
                    return error;
            }
        }
    }
    public static string RetrieveDocumentText(int category, int id)
    {
        if (id != 3 && GameManager.documents[category, id] == false) return noDocument;
        else
        {
            switch (category)
            {
                case 0:
                    switch (id)
                    {
                        case 0:
                            return ("Descrição do Estudo Ambiental.");
                        case 1:
                            return ("Descrição da Ficha Médica de Criança.");
                        case 2:
                            return ("Descrição da Despedida par o Marido.");
                        case 3:
                            if (ReportCheck(category)) return ("Descrição do Relatório: Mundo");
                            else return noReport;
                        default:
                            return error;
                    }
                case 1:
                    switch (id)
                    {
                        case 0:
                            return ("Descrição da Oração Funerária.");
                        case 1:
                            return ("Descrição da Propagada do Líder.");
                        case 2:
                            return ("Descrição da Estratégia de Combate");
                        case 3:
                            if (ReportCheck(category)) return ("Descrição do Relatório: Governo");
                            else return noReport;
                        default:
                            return error;
                    }
                case 2:
                    switch (id)
                    {
                        case 0:
                            return ("Descrição das Anotações do Cientista.");
                        case 1:
                            return ("Descrição da Confirmação de Patrocínio");
                        case 2:
                            return ("Descrição do Chamado às Armas.");
                        case 3:
                            if (ReportCheck(category)) return ("Descrição do Relatório: Resistência");
                            else return noReport;
                        default:
                            return error;
                    }
                case 3:
                    switch (id)
                    {
                        case 0:
                            return ("Descrição do Estudo Evolutivo.");
                        case 1:
                            return ("Descrição do Relatório de Engenharia.");
                        case 2:
                            return ("Descrição da Lista de Alvos.");
                        case 3:
                            if (ReportCheck(category)) return ("Descrição do Relatório: Oscilantes");
                            else return noReport;
                        default:
                            return error;
                    }
                default:
                    return error;
            }
        }
    }

    private static bool ReportCheck(int categoryToCheck)
    {
        if (GameManager.documents[categoryToCheck, 0] && GameManager.documents[categoryToCheck, 1] && GameManager.documents[categoryToCheck, 2]) return true;
        else return false;
    }

}
