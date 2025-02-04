using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public struct Monster
    {
        public string Name { get; }
        public int Hp { get; set; }
        public int Att { get; }

        public char Type;

        public Monster(string name, int hp, int attack, char type)
        {
            Name = name;
            Hp = hp;
            Att = attack;
            Type = type;
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            Console.WriteLine($"{Name}이(가) {damage}의 피해를 입었습니다!");

            if (Hp <= 0)
            {
                Console.WriteLine($"{Name}이(가) 쓰러졌습니다!");
            }
        }

        public int DealDamage()
        {
            Console.WriteLine($"{Name}이(가) 공격했습니다! (공격력: {Att})");
            return Att;
        }
    }
    public class MonsterManager
    {
            private List<Monster> monsters = new List<Monster>();
            private Random random = new Random();

            public MonsterManager()
            {
                // 몬스터 초기 생성
                monsters.Add(new Monster("슬라임", 20, 5, '~'));
                monsters.Add(new Monster("고블린", 30, 8, '='));
                monsters.Add(new Monster("오크", 50, 12, 'ㅁ' ));
                monsters.Add(new Monster("늑대", 35, 10, 'X'));
            }

            public Monster GetRandomMonster()
            {
                int index = random.Next(monsters.Count);
                return monsters[index];
            }

            public void SpawnMonster()
            {
                Monster monster = GetRandomMonster();
                Console.WriteLine($"몬스터 출현!{monster.Type} [{monster.Name}] HP: {monster.Hp}, 공격력: {monster.Att}");
            }
    }

}