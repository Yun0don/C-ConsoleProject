using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public struct Monster
    {
        public string Name { get; }
        public int Hp { get; set; }
        public int MaxHp { get; }
        public int Att { get; }
        public char Type { get; }

        public Monster(string name, int hp, int maxhp, int attack, char type)
        {
            Name = name;
            Hp = hp;
            MaxHp = maxhp;
            Att = attack;
            Type = type;
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp < 0) Hp = 0; // 체력 최소값 보장
            Console.WriteLine($"{Name}이(가) {damage}의 피해를 입었습니다! (남은 체력: {Hp}/{MaxHp})");
        }

        public int DealDamage()
        {
            Console.WriteLine($"{Name}이(가) 공격했습니다! (공격력: {Att})");
            return Att;
        }
    }

    public struct Boss
    {
        public string Name { get; }
        public int Hp { get; set; }
        public int MaxHp { get; }
        public int Att { get; }

        private static Random rand = new Random();

        public Boss(string name, int hp, int maxhp, int attack)
        {
            Name = name;
            Hp = hp;
            MaxHp = maxhp;
            Att = attack;
        }

        public void TakeDamage(ref Boss boss, int damage)
        {
            Hp -= damage;
            if (boss.Hp < 0) Hp = 0;
            Console.WriteLine($"{boss.Name}이(가) {damage}의 피해를 입었습니다! (남은 체력: {boss.Hp}/{boss.MaxHp})");
        }

        public int DealDamage()
        {
            if (Hp <= 0)
            {
                Console.WriteLine($"{Name}은(는) 이미 쓰러졌다.");
                return 0;
            }

            Console.WriteLine($"{Name}이(가) 강력한 공격을 시도합니다! (공격력: {Att})");
            int totalDamage = Att;

            if (rand.Next(0, 3) == 0)
            {
                Console.WriteLine($"{Name}이(가) 연속 공격을 사용합니다! (공격력: {Att})");
                totalDamage += Att;
            }

            return totalDamage;
        }
    }

    public class MonsterManager
    {
        private List<Monster> monsters = new List<Monster>();
        private Random random = new Random();
        private Boss finalBoss;

        private const double Hp_Increase = 0.1;
        private const double Att_Increase = 0.1;

        public MonsterManager()
        {
            // 일반 몬스터 초기 설정
            monsters.Add(new Monster("슬라임", 20, 20, 5, '~'));
            monsters.Add(new Monster("고블린", 30, 30, 8, '='));
            monsters.Add(new Monster("오크", 50, 50, 12, 'ㅁ'));
            monsters.Add(new Monster("늑대", 35, 35, 10, 'X'));

            // 최종 보스 설정
            finalBoss = new Boss("자쿰", 1000, 1000, 50);
        }

        /// 일반 몬스터 랜덤 생성 및 전투 시작
        public void SpawnMonster(Player player, int currentFloor)
        {
            Monster baseMonster = GetRandomMonster();
            double hpMultiplier = 1.0 + (currentFloor - 1) * Hp_Increase;
            double attMultiplier = 1.0 + (currentFloor - 1) * Att_Increase;

            Monster scaledMonster = new Monster(
                baseMonster.Name,
                (int)(baseMonster.Hp * hpMultiplier),
                (int)(baseMonster.MaxHp * hpMultiplier),
                (int)(baseMonster.Att * attMultiplier),
                baseMonster.Type
            );

            Console.WriteLine($"{scaledMonster.Name}이(가) 나타났다! 전투 시작!");
            Battle battle = new Battle(player, scaledMonster);
            battle.StartBattle();
        }

        public void SpawnBoss(Player player)
        {
            Battle battle = new Battle(player, finalBoss);
            battle.StartBossBattle();
        }

        private Monster GetRandomMonster()
        {
            int index = random.Next(monsters.Count);
            return monsters[index];
        }
    }
}
