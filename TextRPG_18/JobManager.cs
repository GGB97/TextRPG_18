using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_18
{
    public class JobManager
    {
        Warrior warrior;
        Mage mage;
        Kinght kingth;
        public void choice(Player player)
        {
            bool Out = false;

            while (!Out)
            {
                Console.Clear();
                Console.WriteLine("원하는 직업을 선택해 주세요!");
                Console.WriteLine("");
                Console.WriteLine("1. 광전사");
                Console.WriteLine("2. 용기사");
                Console.WriteLine("3. 원소 마법사");
                Console.WriteLine(">>> ");

                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1: //광전사 : 적당한 체력, 낮은 mp통, 높은 공격력, 적당한 방어력, 적당한 치확, 높은 치피, 적당한 회피, 낮은 마회 // 죽창맨. 너도 한방 나도 한방.
                            player.SelectedClass = new Warrior("광전사", 150, 50, 30, 15, 25, 120, 30, 5); //hp,mp,atk,def,치확,치피,회피,회복
                            warrior = (Warrior)player.SelectedClass;
                            warrior.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {warrior.name} 입니다");
                            Out = true;
                            break;
                        case 2: //용기사 : 높은 체력, 적당한 mp통, 적당한 공격력, 높은 방어력, 적당한 치확, 낮은 치피, 낮은 회피, 적당한 마회 // 든든맨. 국밥. 밸런스형.
                            player.SelectedClass = new Kinght("용기사", 200, 75, 20, 30, 30, 95, 10, 10);
                            kingth = (Kinght)player.SelectedClass;
                            kingth.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {kingth.name} 입니다");
                            Out = true;
                            break;

                        case 3: //마법사 : 낮은 체력, 높은 mp통, 낮은 공격력, 낮은 방어력, 높은 치확, 높은 치피, 낮은 회피, 높은 마회. // 유리 대포. 마법을 쓸 수 있다면 최강, 아니면 최약체.
                            player.SelectedClass = new Mage("원소 마법사", 120, 300, 10, 10, 60, 150, 5, 50);
                            mage = (Mage)player.SelectedClass;
                            mage.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {mage.name} 입니다");
                            Out = true;
                            break;
                        default:
                            Console.WriteLine("잘못된 선택입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 선택입니다.");
                }


            }




        }


    }
}

