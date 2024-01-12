using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_18
{
    public class JobManager
    {
        public void choice(Player player)
        {

            Console.WriteLine("원하는 직업을 선택해 주세요!");
            Console.WriteLine("");
            Console.WriteLine("1. 용기사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine(">>> ");

            int input;
            if (int.TryParse(Console.ReadLine(), out input))
            {
                switch (input)
                {
                    case 1:
                        Warrior(player);
                        Console.WriteLine($"{player.name}님의 직업은 {player.job} 입니다");
                        break;
                    case 2:
                        Mage(player);
                        Console.WriteLine($"{player.name}님의 직업은 {player.job} 입니다");
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

        public void Warrior(Player player)
        {
            player.job = "용기사";
            player.atk += 6; //기본 공격에 추가로 더함
            player.def += 5;
        }

        public void Mage(Player player)
        {
            player.job = "마법사";
            player.atk += 7; //기본 공격에 추가로 더함
            player.def += 4;
        }
    }
}

