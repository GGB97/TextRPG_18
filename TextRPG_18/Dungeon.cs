using System;
using System.ComponentModel.Design;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using TextRPG;
using static System.Net.Mime.MediaTypeNames;

public class Dungeon
{
    int level;  //던전 난이도
    public int rSpec; // 권장 방어력
    int rGold; // 보상 골드
    int rExp;  // 보상 경험치

    List<Monster> monsters;
    List<Monster> pickmonster = new List<Monster>();


    int idx = 1;

    public void DungeonTitle(Player player)
    {

        Console.WriteLine("");
        monsters = new List<Monster>(); // 레벨, 체력, 공격력
        monsters.Add(new Monster("미니언", 2, 10, 5, false));
        monsters.Add(new Monster("대포", 5, 25, 8, false));
        monsters.Add(new Monster("공허충", 3, 15, 9, false));

        Random random = new Random();

        int monsterNumber = random.Next(1, 5); // 몬스터 마릿수

        // 랜덤으로 몬스터 고름

        for (int i = 0; i < monsterNumber; i++) // 마릿수만큼 반복
        {
            Monster randomMonster = monsters[random.Next(monsters.Count)]; // 몬스터 랜덤으로 가져오기
            pickmonster.Add(new Monster(randomMonster.Name, randomMonster.Level, randomMonster.HP, randomMonster.Atk, randomMonster.IsDefeated)); // 중복되는 몬스터도 있을수도 있기 때문에 pickmonster에 새롭게 넣어준다.
        }
        Console.WriteLine($"{monsterNumber}마리의 몬스터가 출몰하였습니다.");
        Console.WriteLine("");

        foreach (var monster in pickmonster) //랜덤으로 가져온 몬스터 정보 
        {

            Console.WriteLine($" Lv.{monster.Level} {monster.Name} HP {monster.HP}");

        }

        Console.WriteLine();

        DungeonPlayerStatus(player);

        Console.WriteLine("");
        Console.WriteLine("1. 공격");
        Console.WriteLine("");
        Console.Write("원하시는 행동을 입력해 주세요 >>> ");
        Console.WriteLine("");


        string str = Console.ReadLine();

        if (str == "1")
        {
            Console.Clear();
            Console.WriteLine($"{monsterNumber}마리의 몬스터가 출몰하였습니다.");
            Console.WriteLine("");

            PickBattle(player);
            //공격 

        }
        else
        {
            Console.Write($"{str} 은(는) 잘못된 입력입니다.");
        }


    }

    //============인덱스 번호 입력해서 공격===================
    public void PickBattle(Player player)
    {

        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine("Battle!!");
        Console.WriteLine("");

        int i = 1;

        foreach (var mon in pickmonster) //랜덤으로 가져온 몬스터 정보 
        {
            if (pickmonster[i - 1].HP > 0)
            {
                Console.WriteLine($"{i} Lv.{mon.Level} {mon.Name} HP {mon.HP}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{i} Lv.{mon.Level} {mon.Name} Dead");
                Console.ResetColor(); // 색상을 기본값으로 리셋
            }
            i++;
        }

        Console.WriteLine("");
        DungeonPlayerStatus(player);
        Console.WriteLine("");
        Console.WriteLine("0. 취소");
        Console.WriteLine("공격하고자 하는 몬스터의 번호를 입력하세요. >>> ");

        string input = Console.ReadLine();

        if (int.TryParse(input, out idx))
        {
            if (idx >= 1 && idx <= pickmonster.Count)
            {
                if (!pickmonster[idx - 1].IsDefeated) // 처치한 몬스터 구분
                {
                    PlayerAttack(player);
                }
                else
                {
                    Console.WriteLine("이미 처치된 몬스터입니다. 다시 입력하세요.");
                    Thread.Sleep(700);
                    PickBattle(player);
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                Thread.Sleep(700);
                PickBattle(player);
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
            Thread.Sleep(700);
            PickBattle(player);

        }



    }
    //==============플레이어 공격===================
    public void PlayerAttack(Player player)
    {
        Console.Clear();
        Console.WriteLine("====================================");
        Console.WriteLine("Battle!!");
        Console.WriteLine("");

        Random random = new Random();
        int randomDamage = random.Next(-1, 2); // 공격력의 오차 +-1

        int damage = (int)player.atk + randomDamage; //기본 공격력 + 오차값 

        pickmonster[idx - 1].HP -= damage;

        Console.WriteLine(player.name + "의 공격!");
        Console.WriteLine($"Lv.{pickmonster[idx - 1].Level} {pickmonster[idx - 1].Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
        Console.WriteLine("");

        if (pickmonster[idx - 1].HP > 0) // 몬스터 공격
        {

            Console.WriteLine($"Lv.{pickmonster[idx - 1].Name}");
            Console.WriteLine($"HP {pickmonster[idx - 1].HP}");
            Console.WriteLine("");

        }

        if (pickmonster[idx - 1].HP <= 0) // 체력이 다하면 처치
        {
            Console.WriteLine($"Lv.{pickmonster[idx - 1].Level} {pickmonster[idx - 1].Name}");
            Console.WriteLine($"HP 0 -> Daed");
            Console.WriteLine();
            Console.WriteLine("처치하였습니다.");
            Console.WriteLine();
            Console.WriteLine("0.다음");
            Console.WriteLine("행동을 입력하세요 >>>");
            pickmonster[idx - 1].IsDefeated = true; // 처치됨 몬스터가 처치되면 실제로 없어지는 것이 아닌 가상으로 처치됨을 알려줘야 함

            Console.WriteLine("");

            if (pickmonster.Any(monster => !monster.IsDefeated)) // 랜덤으로 나온 몬스터가 다 처치되지 않았을 경우 다시 새로운 몹 선택
            {
                string contn = Console.ReadLine();
                if (contn == "0")
                {
                    PickBattle(player);
                }
            }
            else // 몬스터 다 처치할 경우
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("");
                Console.WriteLine("====================================");
                Console.WriteLine("Victory");
                Console.WriteLine("====================================");

                Console.WriteLine($"던전에서 몬스터 {pickmonster.Count}마리를 잡았습니다");
                Console.WriteLine("");

                int beforeAttackHp = player.hp;
                player.hp -= pickmonster[idx - 1].Atk;
                Console.WriteLine($"Lv.{player.GetLevel()} {player.name}");
                Console.WriteLine($"HP {beforeAttackHp} -> {player.hp}");
                Console.WriteLine("");
                // 클리어 처리

                Console.WriteLine("행동을 입력하세요 >>>");
                
            }
            pickmonster.Clear();
            return;
        }

        Thread.Sleep(700);
        MonsterAttack(player);
        

    }

    //================몬스터 공격===================
    public void MonsterAttack(Player player)
    {
        Console.Clear();

        Console.WriteLine("====================================");
        Console.WriteLine("Battle!!");
        Console.WriteLine("");
        if (pickmonster[idx - 1].HP > 0) // 처치 되기 전
        {
            int beforeAttackHp = player.hp;
            player.hp -= pickmonster[idx - 1].Atk;

            Console.WriteLine($"{pickmonster[idx - 1].Name}의 공격!");
            Console.WriteLine($"{player.name} 을(를) 맞췄습니다. [데미지 : {pickmonster[idx - 1].Atk}]");
            Console.WriteLine("");

            Console.WriteLine($"Lv.{player.GetLevel()} {player.name}");
            Console.WriteLine($"HP {beforeAttackHp} -> {player.hp}");
            Console.WriteLine("");

            Console.WriteLine("");


            if (player.hp <= pickmonster[idx - 1].Atk) // 플레이어의 체력이 0이 될 경우
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("");
                Console.WriteLine("====================================");
                Console.WriteLine("you Lose");
                Console.WriteLine("====================================");

                Console.WriteLine("");

                beforeAttackHp = player.hp;
                player.hp -= pickmonster[idx - 1].Atk;
                Console.WriteLine($"Lv.{player.GetLevel()} {player.name}");
                Console.WriteLine($"HP 100 -> {player.hp}");
                Console.WriteLine("");
                // 클리어 처리

                Console.WriteLine("행동을 입력하세요 >>>");

                pickmonster.Clear();
                return;
            }
            

        }

        Thread.Sleep(700);
        PlayerAttack(player);
        

    }


    public void DungeonPlayerStatus(Player player)
    {
        Console.WriteLine($"Lv.{player.GetLevel()} Chad ({player.getJob()})");
        Console.WriteLine($"{player.hp} / 100");
    }

    public void Clear(Player player)
    {
        // 클리어 시 보상
    }

    public void Fail(Player player)
    {
        // 실패 시  패널티
    }
}