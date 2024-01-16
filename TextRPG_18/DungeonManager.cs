using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using TextRPG;
using TextRPG_18;

public class DungeonManager
{
    public void Select(Player player)
    {
        string str;

        while (true)
        {
            Console.WriteLine("[던전 입장]");
            Console.WriteLine("[플레이어 레벨에 비례해 출현 몬스터가 강해집니다.] \n");
            Console.WriteLine($"현재 체력 : {player.hp} / {player.maxHp}");
            Console.WriteLine(
                "1. 던전 입장 \n" + //"2. 무한 던전 입장 \n" +
                "0. 나가기");
            Console.WriteLine();
                /*"1. 난이도 1 (방어력 8 이상 권장) \n" +
                //"2. 난이도 2 (방어력 10 이상 권장) \n" +
                "3. 난이도 3 (방어력 20 이상 권장) \n"*/

                
            Console.Write($"{player.name} : ");
      
            str = Console.ReadLine();
            Console.WriteLine();
            if (str == "1") /* || str == "2" || str == "3") */
            {
                
                Enter_battle(player);

            }
            else if (str == "0")
            {
                break;
            }
           /* else if (str == "2")
            {
                Console.Clear();
                Console.WriteLine("[준비 중입니다.]\n");
            } */
            else
            {
                GameManager.printError(str);
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
        monsters.Add(new Monster("고블린", (int)MonsterType.Goblin, 12, 50, 30, 100, 50, false, 10)); //이름, 타입, 레벨, 체력, 공격력, 골드, 경험치, 포션 드랍여부, 회피치
        monsters.Add(new Monster("오크", (int)MonsterType.Orc, 17, 65, 35, 150, 75,false,5));
        monsters.Add(new Monster("리자드맨", (int)MonsterType.LizardMan, 20, 45, 45, 200, 100, false,13));
        monsters.Add(new Monster("고블린 사제", (int)MonsterType.Goblin_Frist, 16, 65, 25, 120, 70, true, 5));
        monsters.Add(new Monster("흡혈 박쥐", (int)MonsterType.Vampire_bat, 15, 40, 30, 50, 60, true, 15));
        monsters.Add(new Monster("트롤", (int)MonsterType.Troll, 25, 70, 60, 150, 150, true, 1));

        List<Monster> monstersInBattle = battle_start(player, monsters);
        //전투에 진입해서 생성한 랜덤 몬스터 데이터를 표시 및 리턴한다



        while (true)
        {
            if (turn == "player_choice")
            {
   

                player.battel_DisplayPlayerInfo();  //몬스터 랜덤 등장
                //player.SelectedClass.Initialization(player);  //스킬 턴 횟수 초기화
                Console.WriteLine($"[{player.name}의 턴!]");
                Console.WriteLine("1. 일반공격");
                Console.WriteLine($"2. " + player.SelectedClass.GetName1());
                Console.WriteLine($"3. " + player.SelectedClass.GetName2());
                Console.WriteLine("4. 아이템 사용");
                Console.WriteLine("0. 도주");
                Console.WriteLine();

                Console.Write($"{player.name} :");
                string str = Console.ReadLine();

                if (str == "1")
                {
                    choice_attack_target(player, monstersInBattle, ref turn);
                }
                else if (str == "0")
                {
                    player.SelectedClass.turn = 3;
                    player.SelectedClass.Initialization(player);  //스텟 초기화
                    Thread.Sleep(500);
                    Console.WriteLine("\n성공적으로 도망쳤습니다!");
                    Thread.Sleep(500);
                    Console.Clear();
                    break;
                }
                else if (str == "4")
                {
                    player.Use_Item_Manager();
                }
                else if (str == "2")
                {
                    player.SelectedClass.skill_1(monstersInBattle, player);
                    MonsterAllDie(monstersInBattle, player, ref turn); //몬스터가 전부 죽었는지 확인
                    bool allMonstersDead = monstersInBattle.All(monster => monster.live == "dead");
                    if (allMonstersDead == false)
                    {
                        EnemyTurn(monstersInBattle, player, ref turn);
                        if (turn != "battle_defeat" && turn != "battle_win")
                        {
                            MonsterList(monstersInBattle);
                        }
                        else if (turn == "battle_defeat" || turn == "battle_win")
                        {
                            battle_result(player, monstersInBattle, ref turn);
                        }
                    }
                }
                else if (str == "3")
                {
                    player.SelectedClass.Skill_2(player);
                    MonsterAllDie(monstersInBattle, player, ref turn); //몬스터가 전부 죽었는지 확인
                    bool allMonstersDead = monstersInBattle.All(monster => monster.live == "dead");
                    if (allMonstersDead == false)
                    {
                        EnemyTurn(monstersInBattle, player, ref turn);
                        if (turn != "battle_defeat" && turn != "battle_win")
                        {
                            MonsterList(monstersInBattle);
                        }
                        else if (turn == "battle_defeat" || turn == "battle_win")
                        {
                            battle_result(player, monstersInBattle, ref turn);
                        }
                    }
                }
                else
                {
                    GameManager.printError(str);
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
        Console.Clear();
        Random random = new Random();
        int numberOfMonsters = random.Next(2, (5 + player.level)); //플레이어 레벨 비례 랜덤 숫자 생성
        Console.WriteLine($"\n=====================================================");
        Console.WriteLine($"{numberOfMonsters}마리의 몬스터가 출현했다!\n");

        List<Monster> monstersInBattle = new List<Monster>();

        for (int i = 0; i < numberOfMonsters; i++)
        {
            // 리스트 중에서 랜덤 몬스터 선택
            Monster randomMonster = monsters[random.Next(monsters.Count)];
            int random_difficulty = random.Next(0, player.level+3);

            // 인스턴스 생성
            Monster monsterInstance = new Monster(randomMonster.name, randomMonster.type, randomMonster.level + random_difficulty, randomMonster.hp + random_difficulty*5, randomMonster.atk + random_difficulty*3, randomMonster.gold + random_difficulty*25, randomMonster.exp + random_difficulty*15, randomMonster.drop_potion ,randomMonster.Avoidance + random_difficulty*1/2);

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
        Console.WriteLine("0. 대기하기");
        Console.WriteLine();
        Console.WriteLine("\n공격할 몬스터 선택해 주세요.");
        Console.WriteLine();
        Console.Write($"{player.name} :");
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
                    Console.WriteLine("선택한 몬스터는 이미 죽었습니다.");
                    continue;
                }
                break;
            }
            else
            {
                GameManager.printError(selectedMonsterIndex.ToString());
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
                if (Avoidance_percentage(monstersInBattle[selectedMonsterIndex - 1].Avoidance)) //회피 성공시
                {
                    Console.WriteLine($"{selectedMonster.name}은(는) 공격을 피했다!\n");
                }
                else //회피 실패시
                {
                    int CRatk = player.PlayerDamage();
                    Console.Write($"{selectedMonster.name}은(는) ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"-{CRatk}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" 의 데미지를 입었다!\n");

                    Thread.Sleep(500);

                    // 몬스터 체력 감소
                    selectedMonster.hp -= (int)player.atk;
                }


                // 몬스터 체력이 0이면 사망판정
                if (selectedMonster.hp <= 0)
                {
                    Console.WriteLine($"{selectedMonster.name}은(는) 쓰러졌다!\n");
                    selectedMonster.hp = 0;
                    selectedMonster.live = "dead";

                    // 여기에 퀘스트 조건 검사
                    foreach (var q in player.quests.quests)
                    {
                        q.Check(selectedMonster);
                    }
                }
            }
        }


        MonsterAllDie(monstersInBattle, player, ref turn);  //모든 몬스터가 죽었는지 확인
        EnemyTurn(monstersInBattle, player, ref turn); //몬스터 턴

        player.Recovery(); //마나 회복
        Thread.Sleep(500);

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
            player.SelectedClass.turn = 999;
            player.SelectedClass.Initialization(player);  //스텟 초기화
            player.SelectedClass.turn = 0;
            Console.WriteLine("패배.\n");
            Console.WriteLine($"{player.name} 레벨 {player.level}");
            Console.WriteLine($"체력: {player.hp}");
            Console.WriteLine("\n던전 입구로 돌아가려면 0을 입력해주세요.");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    player.hp = 1;
                    Console.Clear();
                    Console.WriteLine($"\n=====================================================\n");
                    break;
                }
                else
                {
                    GameManager.printError(userInput);
                }
            }
        }
        else if (turn == "battle_win")
        {
            player.SelectedClass.turn = 999;
            player.SelectedClass.Initialization(player);  //스텟 초기화
            player.SelectedClass.turn = 0;
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
            Console.WriteLine($" + {totalExp}\n");
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
                    player.inventory.items.Add(new Consumption("하급 회복 포션", "체력을 약간 회복할 수 있는 포션", 30, 500));
                    Console.Write($"전리품으로 ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"하급 회복 포션");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" 을(를) 획득!");
                }
                else if (potion_luck <= 8) // 30%확률로 중급 회복포션
                {
                    player.inventory.items.Add(new Consumption("중급 회복 포션", "체력을 적당히 회복할 수 있는 포션", 75, 700));
                    Console.Write($"전리품으로 ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"중급 회복 포션");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" 을(를) 획득!");
                }
                else if (potion_luck <= 9) // 10%확률로 고급 회복포션
                {
                    player.inventory.items.Add(new Consumption("고급 회복 포션", "체력을 대폭 회복할 수 있는 포션", 120, 1000));
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
            Console.WriteLine($"직업: {player.SelectedClass.name}");
            Console.WriteLine($"체력: {player.hp}");
            Console.Write($"Gold: ");
            TextRPG.GameManager.printGold(player);

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
                    Console.Clear();
                    break;
                }
                else
                {
                    GameManager.printError(userInput);
                }
            }
        }
    }

    public void MonsterList(List<Monster> monstersInBattle)
    {
        Console.WriteLine("\n[전투 중인 몬스터 목록]");
        Console.WriteLine();

        for (int i = 0; i < monstersInBattle.Count; i++)
        {
            if (monstersInBattle[i].live == "dead")
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{i + 1} Lv.{monstersInBattle[i].level} {monstersInBattle[i].name} [사망] ATK: {monstersInBattle[i].atk}");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{i + 1} Lv.{monstersInBattle[i].level} {monstersInBattle[i].name} HP: {monstersInBattle[i].hp}/{monstersInBattle[i].maxHp} ATK: {monstersInBattle[i].atk}");
        }

    }

    static public void MonsterAllDie(List<Monster> monstersInBattle, Player player, ref string turn)
    {
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
    }

    static public void EnemyTurn(List<Monster> monstersInBattle, Player player, ref string turn)
    {
        player.SelectedClass.turn++;
        if (turn == "enemy_turn")
        {
            Console.WriteLine($"=====================================================");
            Console.WriteLine("[적의 턴!]");
            Console.WriteLine();
            for (int i = 0; i < monstersInBattle.Count; i++)
            {
                monstersInBattle[i].attack(player, ref turn, monstersInBattle);
                if (monstersInBattle[i].live == "live")
                {
                    Console.WriteLine("");
                }
            }

            if (player.hp <= 0)
            {
                turn = "battle_defeat";
            }
            else if (turn == "enemy_turn")
            {
                turn = "player_choice";
            }
            else if (turn == "battle_defeat")
            {
                battle_result(player, monstersInBattle, ref turn);
            }
            Console.WriteLine($"=====================================================");
        }
    }

    static public bool Avoidance_percentage(int percentage)  //회피 확률계산
    {
        Random rend = new Random();

        if (rend.Next(0, 100) < percentage)
        {
            return true;
        }
        else { return false; }
    }

}
