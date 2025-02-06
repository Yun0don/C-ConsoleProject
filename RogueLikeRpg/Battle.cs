using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeRpg
{
    public class Battle
    {
        private Player player;
        private Monster monster;

        public Battle(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;
        }

        public void StartBattle()
        {
            Console.Clear();
            while (player.Hp > 0 && monster.Hp > 0)
            {
                DisplayStatus();
                PlayerTurn();
                Console.ReadKey(); // 여기서 잡아줌
                if (monster.Hp <= 0) break;
                MonsterTurn();
                Console.ReadKey();
            }
            EndBattle();
        }
        private void DisplayStatus()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine($"        Lv.{player.Level}  EXP {player.Exp} / {player.MaxExp}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        HP {player.Hp} / {player.MaxHp}  공격력 {player.Att}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        몬스터: {monster.Name}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        HP: {monster.Hp} / {monster.MaxHp}");
            Console.WriteLine($"        공격력: {monster.Att}");
            Console.WriteLine("=====================================");
        }
        private void PlayerTurn()
        {
            Console.WriteLine("[플레이어 턴]");
            Console.WriteLine("1. 공격  2. 아이템사용");
            Console.Write("선택: ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (choice == '1')
            {
                int damage = DiceRoller.RollDice(player.Att);
                monster.TakeDamage(damage);
                // 주사위를 굴림
                // 주사위 값을 확인함
                // 주사위 굴리고 확인하는건 dice class에서 하고 가져올것

            }
            else if (choice == '2')
            {
                player.ShowInventory();
            }
            else
            {
                Console.WriteLine("턴을 낭비 했습니다. 정신차리세요! ");
            }
        }
        private void MonsterTurn()
        {
            Console.WriteLine($"{monster.Name}이(가) 반격합니다!");
            int damage = monster.DealDamage();
            player.TakeDamage(damage);
        }
        private void EndBattle()
        {
            if (player.Hp > 0 && monster.Hp <= 0)
            {
                Console.WriteLine($"\n{monster.Name}을(를) 처치했습니다!");
                Console.WriteLine("경험치를 획득했습니다.");
                player.GainExp(20);  //  경험치 획득
            }
            else if (player.Hp <= 0)
            {
                Console.WriteLine("\n플레이어가 쓰러졌습니다. 게임 오버!");
            }
            Console.WriteLine("\n 탐험을 재개합니다. 아무 키나 누르세요...");
            Console.ReadKey();
            Console.Clear();  //  전투 종료 후 던전으로 전환
        }
    }
}
