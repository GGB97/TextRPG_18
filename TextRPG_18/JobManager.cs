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
            while(true)
            {
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
                            warrior = new Warrior("광전사", 100, 30, 15, 10, 0, 0); //hp,mp,atk,def
                            warrior.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {warrior.name} 입니다");
                            break;
                        case 2:
                            kingth = new Kinght("용기사", 100, 30, 5, 13, 3, 0 );
                            kingth.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {kingth.name} 입니다");
                            break;

                        case 3:
                            mage = new Mage("원소 마법사", 100, 50, 7, 7,0,3);
                            mage.Pick(player);
                            Console.WriteLine($"{player.name}님의 직업은 {mage.name} 입니다");
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

