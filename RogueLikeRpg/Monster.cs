using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public struct Monster
    {
        public string Name { get; }
        public int Hp { get; set; }

        public int MaxHp { get; set; }
        public int Att { get; }

        public char Type;

        public Monster(string name, int hp, int maxhp ,int attack, char type)
        {
            Name = name;
            Hp = hp;
            Att = attack;
            Type = type;
            MaxHp = maxhp;
        }

        public void TakeDamage(int damage)
        {
            // 주사위의 값을 반환해서 데미지를 결정하고 피해를 입힘.
            Hp -= damage;
            Console.WriteLine($"{Name}이(가) {damage}의 피해를 입었습니다!");
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
                monsters.Add(new Monster("슬라임", 20, 20, 5, '~'));  
                monsters.Add(new Monster("고블린", 30, 30, 8, '='));
                monsters.Add(new Monster("오크", 50, 50, 12, 'ㅁ' ));
                monsters.Add(new Monster("늑대", 35, 35, 10, 'X'));
            }

            public Monster GetRandomMonster()
            {
                int index = random.Next(monsters.Count);
                return monsters[index];
            }

            public void SpawnMonster(Player player)
            {
                Monster monster = GetRandomMonster();
                Battle battle = new Battle(player, monster);
                battle.StartBattle();
            }
            ///  나중에 DungeonManager 생성하고 밸런싱할때 생각해보자.
            //   public void TryEncounterMonster()
            //   {
            //       int chance = random.Next(100);
            //       if (chance < 20)
            //       {
            //       SpawnMonster();
            //       Console.ReadKey();
            //       }
            //   }
    }
}