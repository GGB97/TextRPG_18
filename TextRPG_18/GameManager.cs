
using System;
using System.Xml.Linq;
using TextRPG_18;

namespace TextRPG
{
    internal class GameManager
    {
        Player player;
        Shop shop;
        DungeonManager dungeonManager;
        QuestManager qusetManager;

        public GameManager(Player player)
        {
            this.player = player;
            shop = new Shop();
            dungeonManager = new DungeonManager();
            qusetManager = new QuestManager();
        }

        public void GameStart()
        {
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
                if (str == $"{(int)MenuType.STATUS}")
                {
                    while (true)
                    {
                        Console.Clear();
                        player.printStatus();

                        Console.WriteLine("1. 레벨업");
                        Console.WriteLine("2. 포인트 분배");
                        Console.WriteLine("0. 나가기\n");
                        Console.WriteLine();
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();

                        if (str == "1")
                        {
                            player.Levelup();
                        }
                        else if (str == "2")
                        {
                            player.AllocateStats();
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
                else if (str == $"{(int)MenuType.INVEN}")
                {
                    // 인벤토리


                    while (true)
                    {
                        Console.Clear();
                        player.inventory.print();
                        Console.WriteLine("1. 장비 관리 / 아이템 사용");
                        Console.WriteLine("0. 나가기");
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
                else if (str == $"{(int)MenuType.STORE}")
                {
                    // 상점
                    Console.WriteLine();
                    while (true)
                    {
                        Console.Clear();
                        shop.print2();
                        Console.Write($"소지 골드 : ");
                        printGold(player);
                        Console.WriteLine();
                        Console.WriteLine("1. 아이템 구매");
                        Console.WriteLine("2. 아이템 판매");
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
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
                else if (str == $"{(int)MenuType.DUNGUEON}")
                {
                    // 던전
                    Console.Clear();
                    dungeonManager.Select(player);
                }
                else if (str == $"{(int)MenuType.REST}")
                {
                    Console.Clear();
                    Console.WriteLine("[휴식하기]");
                    Console.WriteLine("500G를 내면 휴식을 할 수 있습니다.");
                    Console.WriteLine();
                    Console.Write($"소지 골드 : ");
                    printGold(player);

                    while (true)
                    {
                        Console.WriteLine("1. 휴식하기");
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine() ;
                        Console.WriteLine();
                        if (str == "1")
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
                else if (str == $"{(int)MenuType.QUEST}")
                {
                    Console.WriteLine();
                    qusetManager.Enter(player);
                }
                else if (str == $"{(int)MenuType.SAVE}")
                {
                    Console.WriteLine();
                    DataManager.I.Save(player);
                }
                else if (str == $"{(int)MenuType.EXIT}")
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

        public static Player CreateCharacter() //!!!!!!!!!!캐릭터 생성!!!!!!!!!!!!
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write("이름 입력 : ");

            string input = Console.ReadLine();
            Player cPlayer = new Player(input);
            JobManager.I.choice(cPlayer);

            Console.WriteLine($"환영합니다. {cPlayer.name} 님의 캐릭터가 생성 되었습니다.");
            Thread.Sleep(750);
            return cPlayer;
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
            string playerName = DataManager.I.LoadAll();
            Console.WriteLine(playerName);
            if (playerName == null)
            {
                player = CreateCharacter();
            }
            else
            {
                player = DataManager.I.Load(playerName);
            }
            GameManager gm = new GameManager(player);
            
            gm.GameStart();
        }
    }
}
