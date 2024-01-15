﻿using System;
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
                Console.WriteLine("3. 원소마법사");
                Console.Write($"{player.name} : ");

                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            player.SelectedClass = new Warrior();
                            warrior = (Warrior)player.SelectedClass;
                            warrior.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {warrior.name} 입니다");
                            Thread.Sleep(1000);
                            Out = true;
                            break;
                        case 2:
                            player.SelectedClass = new Kinght();
                            kingth = (Kinght)player.SelectedClass;
                            kingth.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {kingth.name} 입니다");
                            Thread.Sleep(1000);
                            Out = true;
                            break;

                        case 3:
                            player.SelectedClass = new Mage();
                            mage = (Mage)player.SelectedClass;
                            mage.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {mage.name} 입니다");
                            Thread.Sleep(1000);
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

