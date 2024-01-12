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
                Console.Clear();
                Console.WriteLine("[퀘스트 받기]\n");
                PrintQuests();
                Console.WriteLine("퀘스트 선택. (0. 나가기)");
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
            else if (str == "2")
            {
                // 퀘스트 관리
                Console.Clear();
                Console.WriteLine("[퀘스트 관리]\n");
                player.PrintQuests();
                
                if(player.quests.Count != 0) // player가 보유하고 있는 퀘스트가 있을때만
                {
                    Console.WriteLine("1. 퀘스트 완료");
                    Console.WriteLine("2. 퀘스트 포기");
                    Console.WriteLine("0. 뒤로가기\n");

                    while (true)
                    {
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();
                        if (str == "1")
                        {
                            // 퀘스트 완료
                            player.PrintQuests(true);
                            break;
                        }
                        else if (str == "2")
                        {
                            // 퀘스트 포기
                            Console.Clear();
                            Console.WriteLine("[퀘스트 포기]\n");
                            player.PrintQuests(false);
                            Console.WriteLine("퀘스트 선택. (0. 나가기)");
                            Console.Write($"{player.name} : ");
                            str = Console.ReadLine();
                            int input;

                            if (int.TryParse(str, out input))
                            {
                                if (0 < input && input < quests.Count + 1)
                                {
                                    input--;    // 퀘스트 선택시
                                    //Console.WriteLine("퀘스트 포기"); Thread.Sleep(1000);
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
                else
                {
                    GameManager.PressEnter();
                    break;
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

    public void PrintQuests()
    {
        Console.WriteLine("-------------");
        if (quests.Count == 0)
        {
            Console.WriteLine("현재 수락 가능한 퀘스트가 없습니다.");
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
