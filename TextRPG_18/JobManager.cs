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
                Console.WriteLine(">>> ");

                int input;
                if (int.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            player.SelectedClass1 = new Warrior("광전사", 100, 30, 10, 10, 15, 130); //hp,mp,atk,def,치확,치피
                            warrior = (Warrior)player.SelectedClass1;
                            warrior.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {warrior.name} 입니다");
                            Out = true;
                            break;
                        case 2:
                            
                            player.SelectedClass1 = new Kinght("용기사", 130, 30, 15, 5, 25, 160);
                            kingth = (Kinght)player.SelectedClass1;
                            kingth.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {kingth.name} 입니다");
                            Out = true;
                            break;

                        case 3:
                            player.SelectedClass1 = new Mage("원소 마법사", 80, 60, 5, 3, 25, 160);
                            mage = (Mage)player.SelectedClass1;
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

