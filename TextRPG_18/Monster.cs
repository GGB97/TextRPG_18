using System;
using System.ComponentModel.Design;

public class Monster
{
	public string name;
    public int type;
    public int level;
    public int hp;
    public int maxHp;
    public int atk;
    public int gold;
    public int exp;
    public bool drop_potion;
    public string live = "live";

    public Monster(string name, int type, int level, int hp, int atk, int gold, int exp, bool drop_potion)
	{
		this.name = name;
		this.type = type;
		this.level = level;
		this.hp = hp;
		this.atk = atk;
        this.gold = gold;
        this.exp = exp;
        this.drop_potion = drop_potion;
        this.maxHp = hp;
	}



    public void attack(Player player, ref string turn, List<Monster> monsters)
    {
        if (turn == "enemy_turn")
        {
            if (live == "live")
            {
                Console.WriteLine($"[{name}의 턴!]");
                Thread.Sleep(500);

                Random random = new Random();
                int Skill_Chance = random.Next(0, 10); //각 몬스터는 랜덤 확률로 고유 스킬을 사용한다.
                bool Skill_Use_Check = false; //스킬 사용 했으면 기본공격 안함. 스킬 사용 안했으면 기본 공격.
                int Damage_correction_1 = random.Next(0, 2);  // (0이면 데미지 - 보정, 1이면 데미지 + 보정)
                int Damage_correction_2 = random.Next(0, 31); // 보정치 : +-30%

                int damage = (atk - player.def);

                if (Damage_correction_1 == 1)
                {
                    damage -= (damage * Damage_correction_2) * 1 / 100;
                }
                else
                {
                    damage += (damage * Damage_correction_2) * 1 / 100;
                }
                

                if (damage < 0)  // 플레이어 방어력이 몬스터 공격력에 비해 너무 높을 경우 
                {
                    damage = random.Next(0, 2); //랜덤으로 0 또는 1 의 데미지
                }

                if (type == (int)MonsterType.Goblin)  // 고블린은 30% 확률로 아무것도 안함, 10%확률로 동료를 부름
                {
                    if (Skill_Chance <= 2)
                    {
                        Console.WriteLine($"{name}은(는) 기회를 살피고 있다!\n");
                        Thread.Sleep(500);
                        Skill_Use_Check = true;
                    }
                    else if (Skill_Chance == 3)
                    {
                        int call_colleague = random.Next(0, 2);
                        Console.WriteLine($"{name}은(는) 동료를 불러왔다!");
                        Thread.Sleep(250);
                        if (call_colleague == 0)
                        {
                            monsters.Add(new Monster("고블린", (int)MonsterType.Goblin, 2, 6, 12, 100, 50, false));
                            Console.WriteLine($"고블린이(가) 전투에 참전했다!\n");
                            Thread.Sleep(250);
                        }
                        else
                        {
                            monsters.Add(new Monster("고블린 사제", (int)MonsterType.Goblin_Frist, 6, 5, 10, 120, 70, true));
                            Console.WriteLine($"고블린 사제이(가) 전투에 참전했다!\n");
                            Thread.Sleep(250);
                        }
                        Skill_Use_Check = true;
                    }
                }
                else if (type == (int)MonsterType.Orc)  // 오크는 40% 확률로 광폭한 타격 시전
                {
                    if (Skill_Chance <= 3)
                    {
                        damage = (int)(damage * 2); // 2배 데미지
                        player.hp -= damage;
                        double self_damage = (atk * 0.5); // 오크 본인은 본인 Atk의 절반 만큼 피해를 입는다.
                        self_damage = Math.Truncate(self_damage); //소수점 아래 버림

                        Console.WriteLine($"{name}의 광폭한 타격!");
                        Thread.Sleep(250);
                        Console.WriteLine($"{name}은(는) 미쳐 날뛰면서 자신과 {player.name}을(를) 공격했다!");
                        Console.Write($"{name}은(는) ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"-{(int)self_damage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" 의 데미지를 입었다!");
                        Thread.Sleep(250);
                        Console.Write($"{player.name}은(는) {name}에게 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"-{damage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 의 데미지를 입었다! / 남은 HP: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{player.hp}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(250);

                        hp -= (int)(self_damage); //오크는 자해로 죽을 수 있음
                        if (hp <= 0)
                        {
                            Console.WriteLine($"{name}은(는) 쓰러졌다!\n");
                            Thread.Sleep(250);
                            hp = 0;
                            live = "dead";
                        }
                        Skill_Use_Check = true;

                        if (player.hp <= 0)
                        {
                            Console.WriteLine($"\n{player.name}은(는) 패배했다..\n");
                            turn = "battle_defeat";
                        }
                    }
                }
                else if (type == (int)MonsterType.LizardMan)  // 리자드맨은 30% 확률로 트윈 블레이드 시전
                {
                    if (Skill_Chance <= 2)
                    {
                        damage = (int)(damage * 0.7); // 1회째 타격은 0.7배
                        player.hp -= damage;
                        Console.WriteLine($"{name}의 트윈 블레이드!");
                        Thread.Sleep(250);
                        Console.WriteLine($"{name}은(는) 발톱을 빠르게 두번 휘둘러 {player.name}을(를) 공격!");
                        Console.Write($"{player.name}은(는) {name}에게 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"-{damage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" 의 데미지를 입었다!");
                        double twin_damage = Math.Truncate(damage * 1.3);
                        damage = (int)twin_damage; // 2회째 타격은 무조건 1.3배
                        player.hp -= damage;
                        Thread.Sleep(150);
                        Console.Write($"그리고 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"-{damage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 의 데미지를 입었다! / 남은 HP: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{player.hp}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(250);
                        Skill_Use_Check = true;

                        if (player.hp <= 0)
                        {
                            Console.WriteLine($"\n{player.name}은(는) 패배했다..\n");
                            turn = "battle_defeat";
                        }
                    }
                }
                else if (type == (int)MonsterType.Goblin_Frist) //고블린 사제는 50% 확률로 동료를 회복시킨다
                {
                    if (Skill_Chance <= 4)
                    {
                        Skill_Use_Check = true;
                        int randomIndex = random.Next(0, monsters.Count);
                        int healing = random.Next(2, 10);
                        if (monsters[randomIndex].live == "live")
                        {
                            Console.WriteLine($"{name}은(는) 힐을(를) 주창했다!");
                            Thread.Sleep(250);
                            if (monsters[randomIndex].hp >= maxHp)
                            {
                                Console.WriteLine($"그러나 {monsters[randomIndex].name}은(는) 이미 건강한 상태이다!");
                                Thread.Sleep(250);
                                Console.WriteLine($"아무 일도 일어나지 않았다!\n");
                            }
                            else
                            {
                                monsters[randomIndex].hp += healing;
                                Console.Write($"{monsters[randomIndex].name}은(는) 체력을");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($" +{healing}");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write($" 만큼 회복했다!\n");

                                if (monsters[randomIndex].hp >= monsters[randomIndex].maxHp)
                                {
                                    monsters[randomIndex].hp = monsters[randomIndex].maxHp;
                                }
                            }
                        }
                        else if (monsters[randomIndex].live == "dead") //시체를 골랐을 경우
                        {
                            int resurrection = random.Next(0, 10); // 40% 확률로 부활시킨다.
                            Console.WriteLine($"{name}은(는) 부활의 기도을(를) 주창했다!");
                            Thread.Sleep(250);
                            if (resurrection <= 3)
                            {
                                Console.Write($"{monsters[randomIndex].name}은(는) ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write($"부활");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write($" 했다!\n");
                                monsters[randomIndex].live = "live"; //부활!!
                                monsters[randomIndex].hp += healing; //체력도 회복시킨다

                                if (monsters[randomIndex].hp >= monsters[randomIndex].maxHp)
                                {
                                    monsters[randomIndex].hp = monsters[randomIndex].maxHp;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"그러나 {name}의 기도는 닿지 않았다!");
                                Thread.Sleep(250);
                                Console.WriteLine($"아무 일도 일어나지 않았다!\n");
                            }
                        }
                    }
                }
                else if (type == (int)MonsterType.Troll)  // 트롤은 높은 확률로 체력을 회복하고 아무것도 안함 (최대 체력을 돌파한다) (트롤은 atk가 제일 높다. 맞으면 아픔)
                {
                    if (Skill_Chance <= 7)
                    {
                        int hp_up = random.Next(1,4);
                        Skill_Use_Check = true;
                        Console.WriteLine($"{name}은(는) 먹다남은 음식을 먹었다!");
                        Thread.Sleep(250);
                        Console.Write($"{name}의 체력과 최대 체력이 ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"{hp_up}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" 증가했다!\n");
                        hp += hp_up;
                        maxHp += hp_up;
                        Thread.Sleep(500);
                    }
                }
                else if (type == (int)MonsterType.Vampire_bat) // 흡혈 박쥐는 플레이어를 공격하면서 체력을 회복한다. 체력이 최대라면 최대 체력이 상승한다.
                {
                    if (Skill_Chance <= 5)
                    {
                        Skill_Use_Check = true;
                        Console.WriteLine($"{name}은 흡혈을 시전했다!");
                        player.hp -= damage;
                        Thread.Sleep(250);
                        Console.Write($"{player.name}은(는) {name}에게 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"-{damage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 의 데미지를 입었다! / 남은 HP: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{player.hp}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(250);
                        hp += damage;
                        Console.Write($"{name}은(는) ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"+{damage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 의 체력을 회복했다!\n");
                        Thread.Sleep(250);
                        if (hp >= maxHp)
                        {
                            Console.Write($"{name}은(는) 피를 가득 마셔 최대 체력이 ");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"1");
                            maxHp += 1;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($" 상승했다!\n");
                            Thread.Sleep(250);
                            hp = maxHp;
                        }

                        if (player.hp <= 0)
                        {
                            Console.WriteLine($"\n{player.name}은(는) 패배했다..\n");
                            turn = "battle_defeat";
                        }
                    }
                }

                if (Skill_Use_Check == false)
                {
                    player.hp -= damage;
                    if (player.hp <= 0)
                    {
                        player.hp = 0;
                    }

                    Console.WriteLine($"{name}이(가) {player.name}을(를) 공격!");
                    Thread.Sleep(500);
                    Console.Write($"{player.name}은(는) {name}에게 ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"-{damage}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" 의 데미지를 입었다! / 남은 HP: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{player.hp}");
                    Console.ForegroundColor = ConsoleColor.White;

                    Thread.Sleep(500);

                    if (player.hp <= 0)
                    {
                        Console.WriteLine($"\n{player.name}은(는) 패배했다..\n");
                        turn = "battle_defeat";
                    }
                }
            }
        }
    }
}
