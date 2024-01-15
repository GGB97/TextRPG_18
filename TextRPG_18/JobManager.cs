using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG_18
{
    public class JobManager
    {
        public static JobManager I = new();
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
                Console.WriteLine("1. 광전사 [피의 길: 체력을 소비하는 대신 강력한 공격력과 학살 능력을 가진 직업]");
                Console.WriteLine("2. 용기사 [드래곤의 길: 무난한 공격력과 든든한 방어력을 갖춘 밸런스형 직업]");
                Console.WriteLine("3. 마법사 [지식의 길: 영창에 성공한다면 궁극의 대마법을 시전할 수 있는 직업]\n");
                Console.Write($"{player.name} : ");

                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("");
                    switch (input)
                    {
                        case 1:
                            player.SelectedClass = new Warrior();
                            warrior = (Warrior)player.SelectedClass;
                            warrior.Pick(player);
                            Console.WriteLine($"{player.name} 님의 직업은 {warrior.name} 입니다");
                            Thread.Sleep(1000);
                            Out = true;
                            break;
                        case 2:
                            player.SelectedClass = new Kinght();
                            kingth = (Kinght)player.SelectedClass;
                            kingth.Pick(player);
                            Console.WriteLine($"{player.name} 님의 직업은 {kingth.name} 입니다");
                            Thread.Sleep(1000);
                            Out = true;
                            break;

                        case 3:
                            player.SelectedClass = new Mage();
                            mage = (Mage)player.SelectedClass;
                            mage.Pick(player);
                            Console.WriteLine($"{player.name} 님의 직업은 {mage.name} 입니다");
                            Thread.Sleep(1000);
                            Out = true;
                            break;
                        default:
                            GameManager.printError(input.ToString());
                            break;
                    }
                }
                else
                {
                    GameManager.printError(input.ToString());
                }
            }
        }
    }
}

