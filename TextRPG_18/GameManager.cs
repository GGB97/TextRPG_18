namespace TextRPG
{
    internal class GameManager
    {
        Player player;
        Shop shop;
        DungeonManager dungeonManager;
        QusetManager qusetManager;

        public GameManager(Player player)
        {
            this.player = player;
            shop = new Shop();
            dungeonManager = new DungeonManager();
            qusetManager = new QusetManager();
        }

        public void GameStart()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");

            while (true)
            {
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
                    player.printStatus();

                    while (true)
                    {
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine("1. 레벨업");
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();

                        if (str == "1")
                        {
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
                    player.inventory.print();

                    while (true)
                    {
                        Console.WriteLine("1. 장착 관리");
                        Console.WriteLine("2. 나가기");
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();

                        if (str == "1")
                        {
                            //장비 관리
                            player.EquipManager();
                            break;
                        }
                        if (str == "2")
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
                    Console.WriteLine($"소지금 : {player.gold}");
                    shop.print();

                    while (true)
                    {
                        Console.WriteLine("1. 아이템 구매");
                        Console.WriteLine("2. 아이템 판매");
                        Console.WriteLine("3. 나가기");
                        Console.Write($"{player.name} : ");
                        str = Console.ReadLine();
                        if (str == "1")
                        {
                            // 아이템 구매
                            shop.buy(player);
                            break;
                        }
                        else if (str == "2")
                        {
                            // 아이템 판매
                            shop.sell(player);
                            break;
                        }
                        else if (str == "3")
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
                    dungeonManager.Select(player);
                }
                else if (str == "5")
                {
                    Console.WriteLine("500G를 내면 휴식을 할 수 있습니다. (빈사상태 일 경우 1000G) ");
                    Console.WriteLine($"소지금 : {player.gold} G");

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
                    qusetManager.Enter(player);
                }
                else if (str == "9")
                {
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
        }
    }
}
