using TextRPG_18;

namespace TextRPG
{
    internal class GameManager
    {
        Player player;
        Shop shop;
        DungeonManager dungeonManager;
        JobManager job;

        public GameManager(Player player)
        {
            this.player = player;
            shop = new Shop();
            dungeonManager = new DungeonManager();
            
            job = new JobManager();
        }

        public void GameStart()
        {
            player.CreateCharacter(); // !!!!-----캐릭터 생성---------!!!!!

            job.choice(player);

            while (true)
            {
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 던전 입장");
                Console.WriteLine("5. 휴식");
                Console.WriteLine("9. 저장");
                Console.WriteLine("0. 종료");

                Console.Write($"{player.name} : ");
                string str = Console.ReadLine();
                if (str == "1")
                {
                    // 상태 보기
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
                            Console.Write($"{str} 은(는) 잘못된 입력입니다.");
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
                            Console.Write($"{str} 은(는) 잘못된 입력입니다.");
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
                            Console.Write($"{str} 은(는) 잘못된 입력입니다.");
                        }
                    }
                }
                else if (str == "4")
                {
                    // 던전
                    dungeonManager.Select(player);
                }
                else if(str == "5")
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
                            Console.WriteLine($"{str} 은(는) 잘못된 입력입니다.");
                        }
                    }
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
                    Console.Write($"{str} 은(는) 잘못된 입력입니다.");
                }
            }

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
