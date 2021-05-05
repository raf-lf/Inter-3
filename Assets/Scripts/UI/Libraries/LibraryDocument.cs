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
                            return ("Despedida para o Marido");
                        case 3:
                            if (ReportCheck(category)) return ("Análise do Mundo");
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
                            return ("Biografia de Propagada do Líder");
                        case 2:
                            return ("Estratégia de Combate");
                        case 3:
                            if (ReportCheck(category)) return ("Análise do Governo");
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
                            if (ReportCheck(category)) return ("Análise da Resistência");
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
                            if (ReportCheck(category)) return ("Análise dos Oscilantes");
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
                            return ("Beto, eu sinto muito. \n\nEu sei que estamos casados a pouco tempo e que você queria um tempo comigo durante esse final de semana, " +
                                "mas eu preciso te contar o que aconteceu, e não consigo fazer isso olhando nos seus olhos, neste momento.\n\n Minha carreira e anos " +
                                "de estudo jogados fora. Lembro quando eu era um jovem cursando economia, empolgado com a vida e com vontade de bater de frente " +
                                "contra a sociedade.Provar para todos eles que não importa o que eles pensam de mim, eu seria o cara mais bem sucedido de todos!\n\n" +
                                "A empresa mandou todos os funcionários embora a algumas horas atrás.Eu nunca tinha visto o Felipe chorar tanto, eles tiveram que o" +
                                "restringir para que ele não se jogasse na frente dos carros. Honestamente, me surpreende como conseguimos ficar tanto tempo de portas " +
                                "abertas com esta maldita guerra. Eu acho que foi culpa daqueles vídeos que estão divulgando sobre uma suposta arma química.Espero que " +
                                "seja tudo uma mentira.\n\n Eu preciso de um tempo..Vou pras montanhas, quem sabe todo aquele meu conhecimento de escoteiro de quando " +
                                "eu era moleque me ajude nesse momento difícil? \n\n Volte para a casa dos seus pais esse final de semana, fuja dessa cidade horrível " +
                                "por pelo menos alguns dias. Eu volto quando tiver com a cabeça melhor. Aí penso como reconstruir minha vida nesse mundo de merda.\n\n" +
                                "Desculpe, mas eu preciso ficar sozinho.\nTe amo, Carlos.");
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
