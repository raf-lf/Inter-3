using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public static class LibraryDialogue
{
    public static int currentPortraitId;
    public static Sprite[] characterPortrait = new Sprite[7];

    public static string RetrieveDialogue(int level, int chatId, int sectionId ,int lineId)
    {
        switch (level)
        {
            //Level 1 dialogues
            case 1:
                switch (chatId)
                {
                    //Quilla wakes up
                    case 1:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 2;
                                        return ("Alguém na escuta? Precisamos de ajuda!");
                                    default: return null;
                                }
                            case 2:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 2;
                                        return ("Tem alguém aí? Nós estamos sendo!");
                                    default: return null;
                                }
                            case 3:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 2;
                                        return ("As máquinas do exército estão destruindo o portão! A base está sendo invadida! Nós não temos muito tempo!");
                                    default: return null;
                                }
                            case 4:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return ("Quilla na escuta. Quem está falando?");
                                    case 2:
                                        currentPortraitId = 2;
                                        return ("Um sobrevivente nessa frequência? Aqui é Reina, líder da Resistência. Estamos sob ataque dos militares e precisamos " +
                                            "de evacuação urgente!? ");
                                    default: return null;
                                }
                            case 5:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return ("Estou enviando minha posição. Como chego até aí ?");
                                    case 2:
                                        currentPortraitId = 2;
                                        return ("Você está no deserto? Está próxima! Siga na direção leste até chegar nos limites da cidade!");
                                    case 3:
                                        currentPortraitId = 0;
                                        return ("Positivo. Estou a caminho.");
                                    case 4:
                                        currentPortraitId = 2;
                                        return ("Entro em contato novamente quando você estiver se aproximando! Vou tentar encontrar outros membros da " +
                                            "Resistência nessa frequência.");
                                    case 5:
                                        currentPortraitId = 2;
                                        return ("Por favor, venha rápido e tome cuidado!");
                                    case 6:
                                        currentPortraitId = 0;
                                        return ("Entendido. Quilla desligando.");
                                    default: return null;
                                }
                            case 6:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return ("Operador, você está na escuta?");
                                    case 2:
                                        currentPortraitId = 1;
                                        return ("Sempre. Parece que a situação da Resistência é pior do que foi reportado, mas parece que ainda tempo " +
                                            "tempo de chegar até eles.");
                                    case 3:
                                        currentPortraitId = 1;
                                        return ("Prepare seu equipamento, agente. Quando estiver pronta ative o interruptor para abrir a porta e siga " +
                                            "na direção leste.Estarei observando seu trajeto.");
                                    case 4:
                                        currentPortraitId = 0;
                                        return ("Certo. Me deseje sorte, Operador.");
                                    case 5:
                                        currentPortraitId = 1;
                                        return ("Sorte não é um fator, apenas suas decisões.");
                                    default: return null;
                                }
                            default: return null;

                        }
                    //Capsule found
                    case 2:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 1;
                                        return ("Estou detectando altos níveis de radiação neste local, agente. É de extrema importância que você saia da área.");
                                    case 2:
                                        currentPortraitId = 0;
                                        return ("Uma cápsula quebrada saindo do chão... O que isso tá fazendo por aqui?");
                                    case 3:
                                        currentPortraitId = 1;
                                        return ("Sugiro focar em sua missão, agente.");
                                    case 4:
                                        currentPortraitId = 0;
                                        return ("Certo... Vamos sair daqui.");
                                    default: return null;
                                }
                            default: return null;
                        }
                    //Go back
                    case 3:
                        switch (sectionId)
                        {
                            case 1:
                            switch (lineId)
                            {
                                case 1:
                                    currentPortraitId = 0;
                                    return ("Esta área é perigosa, devo tomarcuidado.");

                                default: return null;
                            }

                            default: return null;
                }
                    //Shredder in sight
                    case 4:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 1;
                                        return ("Fique em alerta. Um dilacerador se encontra próximo da sua posição.");
                                    case 2:
                                        currentPortraitId = 0;
                                        return ("Coisinhas complicadas... Não acho que vou conseguir passar despercebida.");
                                    case 3:
                                        currentPortraitId = 1;
                                        return ("Esta variedade de oscilante é programada para atacar alvos se aproximando.Já foram responsáveis por muitas mortes.");
                                    case 4:
                                        currentPortraitId = 0;
                                        return ("Sei como lidar com eles, alguns disparos com o revolver serão o suficiente.");
                                    case 5:
                                        currentPortraitId = 1;
                                        return ("Sim, sugiro considerar uma aproximação corpo-a-corpo apenas se estiver com pouca munição.");
                                    default: return null;
                                }
                            default: return null;
                        }
                    //Watchout bombardments
                    case 5:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return ("Parece que os bombardeios atingiram essa área bem drasticamente...");
                                    case 2:
                                        currentPortraitId = 2;
                                        return ("Aqueles malditos! Esta área foi uma das mais afetadas pelas bombas que o governo disparou contra a gente...");
                                    case 3:
                                        currentPortraitId = 2;
                                        return ("Se os bombardeios não tivessem acontecido, minha mãe ainda estaria viva...");
                                    case 4:
                                        currentPortraitId = 0;
                                        return ("...");

                                    default: return null;
                                }
                            case 2:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return "Eu entendo, Reina. Eu também perdi alguém muito importante nos ataques.";
                                    case 2:
                                        currentPortraitId = 2;
                                        return "Eu... Eu sinto muito... O que aconteceu?";
                                    case 3:
                                        currentPortraitId = 0;
                                        return "Minha filha, mas já faz muito tempo.";
                                    case 4:
                                        currentPortraitId = 2;
                                        return "...";
                                    case 5:
                                        currentPortraitId = 0;
                                        return "Quantos anos você tem, Reina?";
                                    case 6:
                                        currentPortraitId = 2;
                                        return "Eu? Vinte e um...";
                                    case 7:
                                        currentPortraitId = 0;
                                        return "Acredito que ela teria sua idade hoje.";
                                    case 8:
                                        currentPortraitId = 2;
                                        return "Eu...";
                                    case 0:
                                        currentPortraitId = 1;
                                        return "...";

                                    default: return null;
                                }

                            default: return null;
                        }
                    //Watchout escavation pit
                    case 6:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 1;
                                        return ("Um possível obstáculo a frente.");

                                    default: return null;
                                }
                            case 2:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return "O que é aquilo?";
                                    case 2:
                                        currentPortraitId = 1;
                                        return "Um antigo poço de escavação do governo.Estou recebendo algumas leituras elétricas comparáveis àquelas presentes " +
                                            "em sistemas de defesa avançados.";

                                    default: return null;
                                }
                            case 3:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return "Isso está no meio do caminho... Vou ter que me preparar...";

                                    default: return null;
                                }

                            default: return null;
                        }
                    //Escavation pit
                    case 7:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 2;
                                        return "Quilla, tome cuidado! Você está próxima do poço de escavação!";
                                    case 2:
                                        currentPortraitId = 0;
                                        return "Sim, o que exatamente é este lugar?";
                                    case 3:
                                        currentPortraitId = 2;
                                        return "Um antigo projeto governamental. O local é protegido por um perigoso sistema de defesa que dispara contra quem " +
                                            "se aproxima.";
                                    case 4:
                                        currentPortraitId = 0;
                                        return "Hm... E parece que o trajeto para continuar não é tão simples.Duvido que eu consiga saltar por estas plataformas...";
                                    case 5:
                                        currentPortraitId = 2;
                                        return "Tem um controlador próximo de você. Eu acho que você consegue controlar a posição das plataformas com ele, se ainda " +
                                            "estiver funcionando.";
                                    case 6:
                                        currentPortraitId = 0;
                                        return "Certo. Vou ver o que posso fazer.";

                                    default: return null;
                                }
                            case 2:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 1;
                                        return "Tome cuidado, agente. O poço está cheio de detritos e fragmentos afiados, uma queda pode ser problemática.";

                                    default: return null;
                                }

                            default: return null;
                        }
                    //Defense system turned off
                    case 8:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 1;
                                        return "Sistema de defesa desligado, sua passagem por este local agora está mais segura.";

                                    default: return null;
                                }
                            case 2:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 3;
                                        return "O que temos aqui? Um verme vestindo as cores da resistência tem a audácia de sabotar uma instalação governamental " +
                                            "oficial?";
                                    case 2:
                                        currentPortraitId = 0;
                                        return "General Narus... O líder do governo lida pessoalmente com infrações como essas?";
                                    case 3:
                                        currentPortraitId = 3;
                                        return "O que eu faço ou não não é de seu interesse, rato imundo! Reinicie o sistema imediatamente!";
                                    case 4:
                                        currentPortraitId = 0;
                                        return "Eu acho que não... Vai ser muito mais fácil entrar na cidade sem essa coisa atirando nas minhas costas.";
                                    case 5:
                                        currentPortraitId = 3;
                                        return "Ah! Então você está vindo? Vou preparar um lugar especial pra você observar enquanto eu esmago seus companheiros, " +
                                            "rebelde imprestável!";
                                    case 6:
                                        currentPortraitId = 0;
                                        return "Pode deixar, eu também tenho planos de esmagar um certo general quando eu o encontrar. Nos vemos em breve."; 

                                    default: return null;
                                }
                            case 3:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 2;
                                        return "Quilla! Você está bem? Conseguiu passar pelo poço de escavação?";
                                    case 2:
                                        currentPortraitId = 0;
                                        return "O sistema de defesa foi desligado. E parece que o general Narus não gostou nem um pouco disso.";
                                    case 3:
                                        currentPortraitId = 2;
                                        return "Ele falou com você?! Ele foi avistado liderando o ataque lá fora! Preciso que você venha logo, não consegui encontrar " +
                                            "nenhum outro membro da resistência aí fora!";
                                    case 4:
                                        currentPortraitId = 0;
                                        return "Estou a caminho, Reina. Por favor, aguente.";

                                    default: return null;
                                }

                            default: return null;
                        }
                    //Watchout anihilator
                    case 9:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 2;
                                        return "Você está bem próxima da cidade, Quilla! Consegue ver a entrada?";

                                    default: return null;
                                }
                            case 2:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return "Sim, mas parece que tem algum tipo de oscilante grande bloqueando a passagem.";
                                    case 2:
                                        currentPortraitId = 2;
                                        return "Esse modelo é extremamente perigoso e durável! Eu não sei se armas convencionais serão o suficiente!";
                                    case 3:
                                        currentPortraitId = 0;
                                        return "Hm... Vou ter de pensar em uma possível estratégia...";

                                    default: return null;
                                }
                            case 3:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 2;
                                        return "Por favor, cuidado! A entrada da cidade esta cheia de minas terrestres!";
                                    case 2:
                                        currentPortraitId = 0;
                                        return "Vou ser cautelosa, obrigado Reina.";
                                    case 3:
                                        currentPortraitId = 2;
                                        return "Estou torcendo por você!";

                                    default: return null;
                                }

                            default: return null;
                        }
                    //City gates
                    case 10:
                        switch (sectionId)
                        {
                            case 1:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return "Reina? O oscilante foi destruído.";

                                    default: return null;
                                }
                            case 2:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 0;
                                        return "Reina?";
                                    case 2:
                                        currentPortraitId = 2;
                                        return "Quilla! Eles conseguiram atravessar as defesas internas! Estamos mobilizando a maior quantidade de homens possíveis!";
                                    case 3:
                                        currentPortraitId = 2;
                                        return "A base está um caos... Muitos estão morrendo... Eu... eu não sei o que fazer!";
                                    case 4:
                                        currentPortraitId = 0;
                                        return "Tenha força! Você é a líder destas pessoas. Deve mostrar a eles que consegue aguentar a pressão.";
                                    case 5:
                                        currentPortraitId = 0;
                                        return "Deve liderar eles neste momento de dificuldade, eu acredito que você seja capaz disso!";
                                    case 6:
                                        currentPortraitId = 1;
                                        return "...";
                                    case 7:
                                        currentPortraitId = 2;
                                        return "Eu... Eu farei o meu melhor.";
                                    case 8:
                                        currentPortraitId = 0;
                                        return "Eu sei que fará. Eu estou a caminho. Como chego até vocês?";
                                    case 9:
                                        currentPortraitId = 2;
                                        return "Você precisa entrar na cidade e encontrar a entrada do metrô. A base da resistência fica no subsolo.";
                                    case 10:
                                        currentPortraitId = 2;
                                        return "A cidade está cheia de soldados do governo, Quilla... Eles estabeleceram um perímetro para nenhum de nós escapar!";
                                    case 11:
                                        currentPortraitId = 0;
                                        return "Não se preocupe, vou fazer o possível para lidar com eles. Encontro você quando encontrar a entrada do metrô. Fique " +
                                            "em segurança, querida.";
                                    case 12:
                                        currentPortraitId = 2;
                                        return "Eu... Eu...";
                                    case 13:
                                        currentPortraitId = 2;
                                        return "Certo! Você também! E obrigada!";

                                    default: return null;
                                }
                            case 3:
                                switch (lineId)
                                {
                                    case 1:
                                        currentPortraitId = 1;
                                        return "...";
                                    case 2:
                                        currentPortraitId = 1;
                                        return "A maneira que você trata esta garota é notável, agente.";
                                    case 3:
                                        currentPortraitId = 0;
                                        return "Ela precisa da minha ajuda, operador.";
                                    case 4:
                                        currentPortraitId = 1;
                                        return "Sim, mas é apenas isso?";
                                    case 5:
                                        currentPortraitId = 0;
                                        return "...";
                                    case 6:
                                        currentPortraitId = 0;
                                        return "Ela... Ela me lembra da minha filha.";
                                    case 7:
                                        currentPortraitId = 1;
                                        return "...";
                                    case 8:
                                        currentPortraitId = 0;
                                        return "Eu... Já te contei sobre ela, operador?";
                                    case 9:
                                        currentPortraitId = 1;
                                        return "Sim, agente. Mas discutir relações pessoais do passado não é o foco de sua missão.";
                                    case 10:
                                        currentPortraitId = 0;
                                        return "Você tem razão...";
                                    case 11:
                                        currentPortraitId = 0;
                                        return "Vamos continuar, tenho que encontrar a entrada do metrô e chegar na base da resistência.";

                                    default: return null;
                                }

                            default: return null;
                        }

                    default: return null;
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
                    //Quilla leaves bunker through left
                    case 1:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 1;
                                return ("A base da resistência não fica nesta direção, agente.");
                            case 2:
                                currentPortraitId = 0;
                                return ("Só que talvez possamos encontrar algo de útil nesta direção.");
                            case 3:
                                currentPortraitId = 1;
                                return ("É sua decisão, mas se atente à importância de sua tarefa.");
                            case 4:
                                currentPortraitId = 1;
                                return ("Explore o ambiente com cuidado, você pode encontrar munição e outros recursos.");
                            default: return null;
                        }
                    //Abandoned camp    
                    case 2:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0; 
                                return ("Um acampamento abandonado... O que será que aconteceu com seus antigos habitantes?");
                            case 2:
                                currentPortraitId = 1; 
                                return "Procure por documentos deixados por sobreviventes. Estas perguntas podem ser respondidas.";
                            
                            default: return null;
                        }
                    //Outpost 
                    case 3:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0;
                                return "Um posto avançado da Resistência? Me parece abandonado...";
                            case 2:
                                currentPortraitId = 1;
                                return "É comum encontrar suprimentos em lugares como estes.";
                            case 3:
                                currentPortraitId = 0;
                                return "Hm... Talvez eu consiga ter uma boa visão do terreno no topo da torre?";

                            default: return null;
                        }
                    //Radioactive swamp 
                    case 4:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0;
                                return "O que aconteceu com este lugar?";
                            case 2:
                                currentPortraitId = 1;
                                return "O ambiente foi profundamente afetado pela guerra. A radiação dizimou a flora e a fauna.";

                            default: return null;
                        }
                    //Landmines 
                    case 5:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 1;
                                return "Minas terrestres, agente. Tente não correr pela área.";
                            case 2:
                                currentPortraitId = 0;
                                return "Seria ótimo se os dilaceradores as ativassem...";
                            case 3:
                                currentPortraitId = 0;
                                return "Hm... Mas eles não são imunes as explosões delas...";

                            default: return null;
                        }
                    //Upgrade found 
                    case 6:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 1;
                                return "Esta é uma melhoria valiosa para seu revólver agente.";
                            case 2:
                                currentPortraitId = 0;
                                return "Devo ficar atenta. Talvez eu consiga encontrar mais peças como esta em outros lugares.";

                            default: return null;
                        }
                    //Biologist's corpse 
                    case 7:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0;
                                return "Um cientista morto? O que ele estava fazendo aqui?";
                            case 2:
                                currentPortraitId = 1;
                                return "A análise médica indica fraturas ósseas nas pernas, mas a morte ocorreu por hemorragia causada por dilacerações.";
                            case 3:
                                currentPortraitId = 0;
                                return "Os dilaceradores pegaram ele, aí ele rastejou até aqui...";
                            case 4:
                                currentPortraitId = 1;
                                return "Esta completamente desarmado. Ele não deveria ter se exposto aos perigos do deserto.";
                            case 5:
                                currentPortraitId = 0;
                                return "Tenha um pouco de respeito com os mortos, operador.";
                            case 6:
                                currentPortraitId = 1;
                                return "...";
                            case 7:
                                currentPortraitId = 1;
                                return "Tomarei nota de seus sentimentos, agente.";

                            default: return null;
                        }
                    //Battlefield
                    case 8:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0;
                                return "Tantos destroços e máquinas de guerra quebradas... Um grande confronto aconteceu aqui.";
                            case 2:
                                currentPortraitId = 1;
                                return "Fique atenta para uma possível emboscada. Inimigos podem estar escondidos entre os detritos.";

                            default: return null;
                        }
                    //Rifle ammo found
                    case 9:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 1;
                                return "Esta munição não é compatível com seu equipamento atual, agente.";
                            case 2:
                                currentPortraitId = 0;
                                return "Sim, mas talvez eu consiga encontrar um fuzil em bom estado no futuro. Vou ficar com ela.";

                            default: return null;
                        }
                    //Religious survivor's corpse
                    case 10:
                        switch (lineId)
                        {
                            case 1:
                                currentPortraitId = 0;
                                return "Essa pessoa já morreu a muito tempo.";
                            case 2:
                                currentPortraitId = 1;
                                return "A análise médica indica um adolescente do sexo masculino morto por inanição.";
                            case 3:
                                currentPortraitId = 0;
                                return "Hm... E este santuário? Que lugar incomum, o que o garoto fazia aqui?";
                            case 4:
                                currentPortraitId = 1;
                                return "...";
                            case 5:
                                currentPortraitId = 0;
                                return "Espero que tenha encontrado o que procurava.";

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
