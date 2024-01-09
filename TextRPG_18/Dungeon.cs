using System;

public class Dungeon
{
    bool is_Running = false;
    int level;  //던전 난이도
    public int rSpec; // 권장 방어력
    int rGold; // 보상 골드
    int rExp;  // 보상 경험치
    int killCnt = 0;   // 잡은 몬스터 수

    Player startStat;  // 시작 상태 저장용 player

    // 출현 가능 몬스터 list
    List<Monster> monsters;
    Monster[] enemies;
    public Dungeon(int level)
    {
        this.level = level;
        if (level == 1)
        {
            rSpec = 5;
            rGold = 1000;
            rExp = 10;
        }
        else if (level == 2)
        {
            rSpec = 10;
            rGold = 1700;
            rExp = 25;
        }
        else if (level == 3)
        {
            rSpec = 20;
            rGold = 2500;
            rExp = 40;
        }

        is_Running = false;
        killCnt = 0;

        monsters = new List<Monster>(); // 출현 가능한 몬스터들 미리 넣어두기
        monsters.Add(new Monster("미니언", 2, 15));
        monsters.Add(new Monster("대포", 5, 25));
        monsters.Add(new Monster("공허충", 3, 10));

        Random rand = new Random();
        enemies = new Monster[rand.Next(1, 5)];  // 랜덤으로 1~4 마리의 몬스터를 넣어 둘 수 있는 Monster[] 배열을 생성
        for (int i = 0; i < enemies.Length; i++)    // 생성한 몬스터의 배열에 리스트에 있는 몬스터(프리팹 같은 역할)
        {                                           // 을 랜덤으로 가져와서 할당 반복
            enemies[i] = new Monster(monsters[rand.Next(0, monsters.Count)]);
        }

    }

    public void Battle(Player player)
    {
        string str; int input;

        startStat = new Player(player); // player의 전투 시작 전 상태를 전투 시작 전에 저장
        killCnt = 0;
        is_Running = true;

        while (is_Running)
        {
            Console.Clear();
            Console.WriteLine("Battle!! \n");

            printEnemies(); // 몬스터 정보 출력
            player.printSimple();   // Player 정보 간략하게 출력

            Console.WriteLine("1. 공격");
            Console.Write($"{player.name} : ");
            str = Console.ReadLine();

            if (str == "1")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Player's turn\n");
                    printEnemies(true);
                    player.printSimple();
                    Console.WriteLine("대상을 선택해주세요. (0. 뒤로가기)");

                    // player의 턴
                    Console.Write($"{player.name} : ");
                    str = Console.ReadLine();
                    if (int.TryParse(str, out input))
                    {
                        // 몬스터를 제대로 선택 했을 시
                        if (0 < input && input < enemies.Length + 1) // 만약 몬스터의 수가 3 이라면
                        {                                           // 1~3 을 입력해야 몬스터를 선택 한거라서
                            input--;    // 입력값이 1~3이니까 인덱스를 제대로 건드릴려면 -1

                            if (enemies[input].hp == 0)  // 대상이 이미 죽어있을 경우
                            {
                                Console.WriteLine("사망한 대상입니다.");
                                Thread.Sleep(1000);
                                break;
                            }
                            else
                            {
                                player.Attack(enemies[input]);
                                // ----Player의 승리조건 검사
                                if (enemies[input].hp == 0) // 공격을 마친 후 enemy가 죽어있다면 killCnt++
                                    killCnt++; 
                                if (killCnt == enemies.Length)  // 죽인 몬스터의 수가 총 몬스터의 수와 같다면 다 잡은 것.
                                {
                                    Clear(player);
                                    is_Running = false;
                                    break;
                                }
                                // ----end
                            }

                            // Enemies 의 턴
                            Console.WriteLine("[적의 턴]");
                            foreach (var enemy in enemies)
                            {
                                if (enemy.hp > 0)    // enemy의 체력이 0보다 크다면 (죽지 않았다면)
                                {
                                    enemy.Attack(player);
                                    if (player.hp <= 0)  // player의 체력이 0이하라면 (죽었다면)
                                    {
                                        Fail(player);
                                        is_Running = false; // 게임이 끝났으니 다음 루프 돌지 않게
                                        break;
                                    }
                                }
                            }
                            Console.WriteLine("\nEnter키를 눌러주세요.");
                            Console.ReadLine();
                            break;
                        }
                        else if (input == 0)    // 0번은 취소
                        {
                            break;
                        }
                        else
                        {
                            // 몬스터를 제대로 선택하지 못했을 경우
                            printError(str);
                        }
                    }
                    else
                    {
                        // 몬스터를 제대로 선택하지 못했을 경우
                        printError(str);
                    }
                }
            }
            else
            {
                printError(str);
            }
        }
    }

    public void Clear(Player player)
    {
        // 클리어 시
        Console.WriteLine("--------------");
        Console.WriteLine("Victory!!!\n");
        Console.WriteLine($"던전에서 몬스터 {killCnt}마리를 잡았습니다.\n");
        Console.WriteLine(
            $"Lv. {player.getLevel()} {player.name}\n" +
            $"HP {startStat.hp} -> {player.hp}"
            );
        Console.WriteLine("--------------");

        Console.WriteLine("\nEnter키를 눌러주세요.");
        Console.ReadLine();
    }

    public void Fail(Player player)
    {
        // 실패 시
        Console.WriteLine("--------------");
        Console.WriteLine("You Lose..\n");
        Console.WriteLine(
            $"Lv. {player.getLevel()} {player.name}\n" +
            $"HP {startStat.hp} -> {player.hp}"
            );
        Console.WriteLine("--------------");
    }

    public void printEnemies()
    {
        Console.WriteLine("[적 정보]");
        foreach (Monster enemy in enemies)
        {
            enemy.printStat();    // 몬스터의 수만큼 몬스터 정보 출력
        }
        Console.WriteLine();
    }
    public void printEnemies(bool is_Numberring)
    {
        Console.WriteLine("[적 정보]");
        int i = 1;
        foreach (Monster enemy in enemies)
        {
            if (is_Numberring)
                Console.Write($"{i++}. ");
            enemy.printStat();    // 몬스터의 수만큼 몬스터 정보 출력
        }
        Console.WriteLine();
    }

    void printError(string str)
    {
        Console.WriteLine($"{str} 은(는) 올바른 입력이 아닙니다.");
        Thread.Sleep(1000);
    }
}
