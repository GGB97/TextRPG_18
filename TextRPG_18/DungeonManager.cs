using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using TextRPG;

public class DungeonManager
{
    public void Select(Player player)
    {
        string str;

        while (true)
        {
            Console.WriteLine("[던전 입장] --- (0. 나가기)");
            Console.WriteLine($"현재 체력: {player.hp}\n");
            Console.WriteLine(
                "1. 던전에 입장한다."
                /*"1. 난이도 1 (방어력 8 이상 권장) \n" +
                //"2. 난이도 2 (방어력 10 이상 권장) \n" +
                "3. 난이도 3 (방어력 20 이상 권장) \n"*/
                );
            Console.Write($"{player.name} : ");
            str = Console.ReadLine();

            if (str == "1") /* || str == "2" || str == "3") */
            {
                Enter_battle(player);
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

   /* public void Enter(Player player, int level)
    {
        Console.WriteLine($"난이도 {level} 던전에 입장합니다.\n");
        Dungeon dungeon = new Dungeon(level);
    } */

    public void Enter_battle(Player player)
    {
        string turn = "player_choice";

        List<Monster> monsters;
        monsters = new List<Monster>();
        monsters.Add(new Monster("고블린", (int)MonsterType.Goblin, 2, 6, 12, 100, 50,false));
        monsters.Add(new Monster("오크", (int)MonsterType.Orc, 5, 15, 15, 150, 75,false));
        monsters.Add(new Monster("리자드맨", (int)MonsterType.LizardMan, 8, 7, 20, 200, 100, false));
        monsters.Add(new Monster("고블린 사제", (int)MonsterType.Goblin_Frist, 6, 5, 10, 120, 70, true));
        monsters.Add(new Monster("흡혈 박쥐", (int)MonsterType.Vampire_bat, 4, 4, 10, 50, 60, false));
        monsters.Add(new Monster("트롤", (int)MonsterType.Troll, 7, 12, 30, 150, 150, true));

        List<Monster> monstersInBattle = battle_start(player, monsters);
        //전투에 진입해서 생성한 랜덤 몬스터 데이터를 표시 및 리턴한다


        while (true)
        {
            if (turn == "player_choice")
            {
                player.battel_DisplayPlayerInfo();

                Console.WriteLine($"[{player.name}의 턴!]");

                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 아이템 사용");
                Console.WriteLine("3. 도주");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                string userInput = Console.ReadLine();

                if (userInput == "1")
                {
                    choice_attack_target(player, monstersInBattle, ref turn);
                }
                else if (userInput == "2")
                {
                    player.Use_Item_Manager();
                }
                else if (userInput == "3")
                {
                    Console.WriteLine("\n성공적으로 도망쳤다!");
                    Console.WriteLine($"=====================================================\n");
                    break;
                }
                else
                {
                    Console.Write($"{userInput} 은(는) 잘못된 입력입니다.");
                }
            }
            else
            {
                break;
            }
        }
    }



    public static List<Monster> battle_start(Player player, List<Monster> monsters)  //전투 시작시 몬스터 인스턴스 생성
    {
        Random random = new Random();
        int numberOfMonsters = random.Next(1, 5); // 랜덤 숫자 생성
        Console.WriteLine($"\n=====================================================");
        Console.WriteLine($"앗! {numberOfMonsters}마리의 야생 몬스터가 출현했다!\n");

        List<Monster> monstersInBattle = new List<Monster>();

        for (int i = 0; i < numberOfMonsters; i++)
        {
            // 리스트 중에서 랜덤 몬스터 선택
            Monster randomMonster = monsters[random.Next(monsters.Count)];

            // 인스턴스 생성
            Monster monsterInstance = new Monster(randomMonster.name, randomMonster.type, randomMonster.level, randomMonster.hp, randomMonster.atk, randomMonster.gold, randomMonster.exp, randomMonster.drop_potion);

            // 리스트에 인스턴스 등록
            monstersInBattle.Add(monsterInstance);

            // 몬스터 정보 표시
            Console.WriteLine($"Lv.{monsterInstance.level} {monsterInstance.name} HP: {monsterInstance.hp}/{monstersInBattle[i].maxHp}, ATK: {monsterInstance.atk}");
        }
        return monstersInBattle; // 몬스터 인스턴스 리스트 반환
    }

    public static void choice_attack_target(Player player, List<Monster> monstersInBattle, ref string turn)
    {
        Console.WriteLine("\n[전투 중인 몬스터 목록]");

        for (int i = 0; i < monstersInBattle.Count; i++)
        {
            if (monstersInBattle[i].live == "dead")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{i + 1} Lv.{monstersInBattle[i].level} {monstersInBattle[i].name} [사망] ATK: {monstersInBattle[i].atk}");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{i + 1} Lv.{monstersInBattle[i].level} {monstersInBattle[i].name} HP: {monstersInBattle[i].hp}/{monstersInBattle[i].maxHp}  ATK: {monstersInBattle[i].atk}");
        }

        Console.WriteLine("\n공격할 몬스터 선택 (숫자 입력) --- (0. 대기)");
        Console.Write("선택을 입력하세요:");
        int selectedMonsterIndex;

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out selectedMonsterIndex) && selectedMonsterIndex >= 0 && selectedMonsterIndex <= monstersInBattle.Count)
            {
                if (selectedMonsterIndex == 0)
                {
                    Console.WriteLine($"{player.name}은(는) 대기했다!\n");
                    break;
                }

                if (monstersInBattle[selectedMonsterIndex - 1].live == "dead")
                {
                    Console.WriteLine("선택한 몬스터는 이미 죽었습니다. 다시 선택해주세요.");
                    continue;
                }
                break;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
            }
        }

        if (turn == "player_choice")
        {
            if (selectedMonsterIndex != 0)
            {
                Monster selectedMonster = monstersInBattle[selectedMonsterIndex - 1];

                // Player attacks the selected monster
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"\n{player.name}이(가) {selectedMonster.name}을(를) 공격!");
                Thread.Sleep(500);
                Console.Write($"{selectedMonster.name}은(는) ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"-{player.atk} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"의 데미지를 입었다!\n");
                Thread.Sleep(500);

                // 몬스터 체력 감소
                selectedMonster.hp -= (int)player.atk;

                // 몬스터 체력이 0이면 사망판정
                if (selectedMonster.hp <= 0)
                {
                    Console.WriteLine($"{selectedMonster.name}은(는) 쓰러졌다!\n");
                    selectedMonster.hp = 0;
                    selectedMonster.live = "dead";

                    // 여기에 퀘스트 조건 검사
                    foreach(var q in player.quests)
                    {
                        q.Check(selectedMonster);
                    }
                }
            }
        }

        bool allMonstersDead = monstersInBattle.All(monster => monster.live == "dead");

        if (allMonstersDead)
        {
            turn = "battle_win";
            battle_result(player, monstersInBattle, ref turn);
        }
        else
        {
            turn = "enemy_turn";
        }

        if (turn == "enemy_turn")
        {
            Console.WriteLine($"=====================================================");
            Console.WriteLine("[적의 턴!]");
            for (int i = 0; i < monstersInBattle.Count; i++)
            {
                monstersInBattle[i].attack(player, ref turn, monstersInBattle);
                Console.WriteLine("");
            }
            if (turn == "enemy_turn")
            {
                turn = "player_choice";
            }
            else if (turn == "battle_defeat")
            {
                battle_result(player, monstersInBattle, ref turn);
            }
        }

        if (turn == "player_choice")
        {

            // 몬스터 목록표시
            Console.WriteLine($"\n=====================================================");
            Console.WriteLine("\n[전투 중인 몬스터 목록]");

            for (int i = 0; i < monstersInBattle.Count; i++)
            {
                if (monstersInBattle[i].live == "dead")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($" Lv.{monstersInBattle[i].level} {monstersInBattle[i].name} [사망] ATK: {monstersInBattle[i].atk}");
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($" Lv.{monstersInBattle[i].level} {monstersInBattle[i].name} HP: {monstersInBattle[i].hp}/{monstersInBattle[i].maxHp} ATK: {monstersInBattle[i].atk}");
            }
        }
    }

    public static void battle_result(Player player, List<Monster> monstersInBattle, ref string turn)
    {
        Console.WriteLine($"\n=====================================================");
        Console.WriteLine("\n[Battle Result]");

        if (turn == "battle_defeat")
        {
            Console.WriteLine("패배.\n");
            Console.WriteLine($"{player.name} 레벨 {player.level}");
            Console.WriteLine($"체력: {player.hp}");
            Console.WriteLine("\n던전 입구로 돌아가려면 0을 입력하세요.");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    player.hp = 1;
                    Console.WriteLine($"\n=====================================================\n");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
        else if (turn == "battle_win")
        {
            Console.WriteLine("승리!\n");

            //몬스터의 골드와 경험치 총합 계산 후 랜덤 보정

            Random random = new Random();
            int RandomGold = random.Next(0, 11); // 랜덤으로 10% 보정
            int RandomExp = random.Next(0, 11);
            int totalGold = monstersInBattle.Sum(monster => monster.gold) + (monstersInBattle.Sum(monster => monster.gold) * RandomGold)* 1/100;
            int totalExp = monstersInBattle.Sum(monster => monster.exp) + (monstersInBattle.Sum(monster => monster.exp) * RandomExp) * 1/100;

            //승리했으므로 플레이어 골드 경험치 획득

            Console.WriteLine($"Gold 획득!");
            Console.Write($"{player.gold}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" + {totalGold}\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"경험치 획득!");
            Console.Write($" {player.exp}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($" + {totalExp}\n");
            Console.ForegroundColor = ConsoleColor.White;

            int potion_drop = 0;

            foreach (Monster monseter in monstersInBattle)
            {
                if (monseter.drop_potion == true)
                {
                    potion_drop += 1;
                }
            }

            for (int i = 0; i < potion_drop; i++)
            {
                int potion_luck = random.Next(0, 10);
                if (potion_luck == 0)
                {
                }
                else if (potion_luck <= 5) // 50%확률로 하급포션
                {
                    player.inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 50, 250));
                    Console.Write($"전리품으로 ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"하급 회복 포션");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" 을(를) 획득!");
                }
                else if (potion_luck <= 8) // 30%확률로 중급 회복포션
                {
                    player.inventory.items.Add(new Consumption("중급 회복 포션", "체력을 적당히 회복할 수 있는 포션", 75, 500));
                    Console.Write($"전리품으로 ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"중급 회복 포션");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" 을(를) 획득!");
                }
                else if (potion_luck <= 9) // 10%확률로 고급 회복포션
                {
                    player.inventory.items.Add(new Consumption("고급 회복 포션", "체력을 대폭 회복할 수 있는 포션", 120, 1500));
                    Console.Write($"전리품으로 ");
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write($"고급 회복 포션");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" 을(를) 획득!");
                }
            }
            Console.WriteLine($"");


            player.gold += totalGold;
            player.exp += totalExp;

            Console.WriteLine($"이름: {player.name}");
            Console.WriteLine($"직업: {player.job}");
            Console.WriteLine($"체력: {player.hp}");
            Console.WriteLine($"Gold: ");
            GameManager.printGold(player);

            // 경험치 확인 후 레벨업
            if (player.exp >= player.maxExp)
            {
                player.Levelup();
            }
            else
            {
                Console.WriteLine($"현재 레벨:{player.level}");
                Console.WriteLine($"현재 EXP: {player.exp}/{player.maxExp}");
            }

            Console.WriteLine("\n던전 입구로 돌아가려면 0을 입력하세요.");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "0")
                {

                    break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                }
            }
        }
    }
}
