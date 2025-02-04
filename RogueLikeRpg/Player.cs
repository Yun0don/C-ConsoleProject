using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public partial class Player
    {
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }
        public int Att { get; private set; }
        public int Exp { get; private set; }
        public int MaxExp { get; private set; }
        public int Level { get; private set; }
        public List<Item> Inventory { get; private set; }

        public Player()
        {
            MaxHp = 100;
            Hp = MaxHp;
            Att = 10;
            Level = 1;
            MaxExp = 100;
            Exp = 0;
            Inventory = new List<Item>();
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Console.WriteLine("플레이어가 사망했습니다.");
                Respawn();
            }
        }

        public void GainExp(int amount)
        {
            Exp += amount;
            if (Exp >= MaxExp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Exp = 0;
            MaxHp += 20;
            Hp = MaxHp;
            Att += 5;
            Console.WriteLine($"레벨업! 현재 레벨: {Level}");
        }

        private void Respawn()
        {
            Console.WriteLine("부활 중... 아이템은 유지됩니다.");
            Hp = MaxHp;
            Exp = 0;
        }
    }
}


