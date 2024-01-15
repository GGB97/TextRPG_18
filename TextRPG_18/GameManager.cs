
using System;
using TextRPG_18;

namespace TextRPG
{
    internal class GameManager
    {
        Player player;
        Shop shop;
        DungeonManager dungeonManager;
        QuestManager qusetManager;
        JobManager job;

        public GameManager(Player player)
        {
            this.player = player;
            shop = new Shop();
            dungeonManager = new DungeonManager();
            qusetManager = new QuestManager();
            job = new JobManager();
        }

        public void GameStart()
        {
            player.CreateCharacter(); // !!!!-----캐릭터 생성---------!!!!!

            job.choice(player);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("원하는 행동을 입력해 주세요.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 휴식");
                Console.WriteLine("6. 퀘스트");
                Console.WriteLine("9. 저장");
                Console.WriteLine("0. 종료\n");

                Console.Write($"{player.name} : ");
                string str = Console.ReadLine();
                if (str == "1")
                {
                    Console.Clear();
                    player.printStatus();

                    while (true)
                    {
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine("1. 레벨업");
                        Console.WriteLine();
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();

                        if (str == "1")
                        {
                            Console.Clear();
                            player.Levelup();
                        }
                        else if (str == "0")
                        {
                            break;
                        }
                        else
                        {
                            printError(str);
                        }
                    }
                }
                else if (str == "2")
                {
                    // 인벤토리
                    
                    Console.WriteLine();
                    while (true)
                    {
                        Console.Clear();
                        player.inventory.print();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine("1. 장비 관리 / 아이템 사용");
                        Console.WriteLine();

                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();

                        if (str == "1")
                        {
                            Console.Clear();
                            //아이템 사용 및 장비 관리
                            player.Use_Item_Manager();
                        }
                        else if (str == "0")
                        {
                            break;
                        }
                        else
                        {
                            printError(str);
                        }
                    }
                }
                else if (str == "3")
                {
                    // 상점
                    Console.WriteLine();
                    while (true)
                    {
                        Console.Clear();
                        shop.print();
                        Console.Write($"소지 골드 : ");
                        printGold(player);
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine("1. 아이템 구매");
                        Console.WriteLine("2. 아이템 판매");
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();
                        if (str == "1")
                        {
                            Console.Clear();
                            // 아이템 구매
                            shop.buy(player);
                        }
                        else if (str == "2")
                        {
                            Console.Clear();
                            shop.sell(player);
                        }
                        else if (str == "0")
                        {
                            break;
                        }
                        else
                        {
                            printError(str);
                        }
                    }
                }
                else if (str == "4")
                {
                    // 던전
                    Console.Clear();
                    dungeonManager.Select(player);
                }
                else if (str == "5")
                {
                    Console.Clear();
                    Console.WriteLine("500G를 내면 휴식을 할 수 있습니다.");
                    Console.Write($"소지 골드 : ");
                    printGold(player);

                    while (true)
                    {
                        Console.WriteLine("1. 휴식하기");
                        Console.WriteLine("0. 나가기");

                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine() ;

                        if(str == "1")
                        {
                            player.Rest();
                        }
                        else if(str == "0")
                        {
                            break;
                        }
                        else
                        {
                            printError(str);
                        }
                    }
                }
                else if (str == "6")
                {
                    Console.WriteLine();
                    qusetManager.Enter(player);
                }
                else if (str == "9")
                {
                    Console.WriteLine();
                    DataManager.I.Save(player);
                }
                else if (str == "0")
                {
                    Console.WriteLine("게임을 종료합니다.");
                    break;
                }
                else
                {
                    printError(str);
                }

                Console.Clear();
            }
        }

        public static void printError(string str)
        {
            Console.WriteLine($"{str} 은(는) 올바른 입력이 아닙니다.");
            Thread.Sleep(1000);
        }

        public static void PressEnter()
        {
            Console.WriteLine("Enter키를 눌러주세요.");
            Console.ReadLine();
        }

        public static void printGold(Player player)  //골드를 노랗게 표시해서 프린트하는 메서드
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{player.gold}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" G");

        }

        static void Main(string[] args)
        {
            Player player;
            string playerName = "용사";
            player = DataManager.I.Load(playerName);
            if (player == null)
            {
                player = new Player("용사");
            }
            GameManager gm = new GameManager(player);
            
            gm.GameStart();

            ///test
        }
    }
}
