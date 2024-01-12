using System;
using System.Numerics;
using TextRPG;

public class QusetManager
{
    List<Quest> quests;
    public QusetManager()
    {
        quests = new List<Quest>();

        quests.Add(new Quest("고블린 사냥", "고블린 10마리 사냥", 1000, 50, "고블린", 10));
        quests.Add(new Quest("퀘스트 완료 테스트", "수락시 바로 클리어 가능", 1000, 50, "고블린", 0, true));
    }

    public void Enter(Player player)
    {
        string str;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("퀘스트를 수락/완료 할 수 있습니다.\n");

            player.PrintQuests();

            Console.WriteLine("1. 퀘스트 받기");
            Console.WriteLine("2. 퀘스트 관리");
            Console.WriteLine("0. 나가기\n");

            Console.Write($"{player.name} : ");
            str = Console.ReadLine();

            if (str == "1")
            {
                // 퀘스트 받기
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("[퀘스트 받기]\n");
                    PrintQuests();
                    Console.WriteLine("퀘스트 선택. (0. 나가기)\n");
                    Console.Write($"{player.name} : ");
                    str = Console.ReadLine();
                    int input;

                    if (int.TryParse(str, out input))
                    {
                        if (0 < input && input < quests.Count + 1)
                        {
                            input--;    // 퀘스트 선택시
                            quests[input].Accept(player);
                            break;
                        }
                        else if (input == 0)
                        {
                            break;
                        }
                        else
                        {
                            GameManager.printError(str);
                        }
                    }
                    else
                        GameManager.printError(str);
                }

            }
            else if (str == "2")
            {
                // 퀘스트 관리
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("[퀘스트 관리]\n");
                    player.PrintQuests();
                    Console.WriteLine("1. 퀘스트 완료");
                    Console.WriteLine("2. 퀘스트 포기");
                    Console.WriteLine("0. 나가기\n");
                    Console.Write($"{player.name} : ");
                    str = Console.ReadLine();

                    if (str == "1")
                    {
                        // 퀘스트 완료
                        QuestComplete_Abandon(player, true);
                        break;
                    }
                    else if (str == "2")
                    {
                        // 퀘스트 포기
                        QuestComplete_Abandon(player, false);
                        break;
                    }
                    else if (str == "0")
                    {
                        break;
                    }
                    else
                    {
                        GameManager.printError(str);
                    }
                }
            }
            else if (str == "0")
            {
                break;
            }
            else
            {
                GameManager.printError(str);
            }
        }
    }

    void QuestComplete_Abandon(Player player, bool ca)
    {
        string str;
        string caStr = ca ? "완료" : "포기";
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"[퀘스트 {caStr}]\n");
            player.PrintQuests();
            Console.WriteLine("퀘스트 선택. (0. 나가기)\n");
            Console.Write($"{player.name} : ");
            str = Console.ReadLine();
            int input;

            if (int.TryParse(str, out input))
            {
                if (0 < input && input < quests.Count + 1)
                {
                    input--;    // 퀘스트 선택시
                    if (ca)
                    {
                        player.quests[input].Complete(player);
                    }
                    else
                    {
                        player.quests[input].Abandon(player);
                    }
                    
                    break;
                }
                else if (input == 0)
                {
                    break;
                }
                else
                {
                    GameManager.printError(str);
                }
            }
            else
                GameManager.printError(str);
        }
    }

    public void PrintQuests()
    {
        Console.WriteLine("[수락 가능한 퀘스트 목록]");
        Console.WriteLine("-------------");
        if (quests.Count == 0)
        {
            Console.WriteLine("수락 가능한 퀘스트가 없습니다.");
        }
        else
        {
            int n = 1;
            foreach (var q in quests)
            {
                Console.Write($"{n++}. ");
                q.Print();
            }
        }
        Console.WriteLine("-------------\n");
    }
}
