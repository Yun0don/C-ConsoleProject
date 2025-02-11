using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    // 플레이어 직업을 나타내는 열거형(enum)
    public enum PlayerClass
    {
        Warrior,
        Thief,
        Mage
    }

    // PlayerType은 Player를 상속받는 구체 클래스입니다.
    public class PlayerType : Player
    {
        private PlayerClass playerClass;

        // 생성자: 부모(Player) 생성자 호출 후, 플레이어의 초기 좌표, 직업, 심볼 등을 설정
        public PlayerType(int startX, int startY, PlayerClass playerClass) : base()
        {
            X = startX;
            Y = startY;
            this.playerClass = playerClass;
            Symbol = GetClassSymbol();

            // 각 직업별로 초기 스탯 및 시작 아이템/인벤토리 설정
            if (playerClass == PlayerClass.Warrior)
            {
                MaxHp = 250;  // 전사는 높은 체력
                Hp = MaxHp;
                Att = 10;
                // 전사는 "덤불조끼"를 시작 아이템으로 들고 생성
                Inventory.Add(new Item("덤불조끼", ItemType.Armor, 20));
                Inventory.Add(new Item("덤불조끼", ItemType.Armor, 20));
                Inventory.Add(new Item("하이랜더", ItemType.Weapon, 20));
                Inventory.Add(new Item("하이랜더", ItemType.Weapon, 20));
            }
            else if (playerClass == PlayerClass.Thief)
            {
                MaxHp = 50;   // 도적은 낮은 체력, 높은 공격력
                Hp = MaxHp;
                Att = 30; // 30*2
                // 도적은 추가적인 공격력 효과는 Attack()에서 처리 (50% 확률로 두 번 공격)
            }
            else if (playerClass == PlayerClass.Mage)
            {
                MaxHp = 150;  // 마법사는 보통 체력
                Hp = MaxHp;
                Att = 20;     // 예시로 공격력을 15로 설정
                // 마법사는 포션 3종류(빨간포션, 주황포션, 하얀포션)를 모두 인벤토리에 추가
                Inventory.Add(new Item("빨간포션", ItemType.Potion, 25));
                Inventory.Add(new Item("빨간포션", ItemType.Potion, 25));
                Inventory.Add(new Item("주황포션", ItemType.Potion, 50));
                Inventory.Add(new Item("주황포션", ItemType.Potion, 50));
                Inventory.Add(new Item("하얀포션", ItemType.Potion, 100)); 
                Inventory.Add(new Item("하얀포션", ItemType.Potion, 100));
                Dice = new Dice("마법사의 주사위", new int[] { 3, 3, 4, 4, 5, 6, });
            }
        }

        // 직업에 따른 플레이어 심볼 반환
        protected override char GetClassSymbol()
        {
            if (playerClass == PlayerClass.Warrior)
                return '@';
            else if (playerClass == PlayerClass.Thief)
                return '$';
            else if (playerClass == PlayerClass.Mage)
                return '^';
            else
                return '?';
        }

        /// <summary>
        /// 플레이어의 공격을 계산하여 반환합니다.
        /// 도적은 50% 확률로 주사위를 2번 던져 두 번 공격한 데미지를 합산합니다.
        /// </summary>
        /// <returns>공격 데미지</returns>
        public override int Attack()
        {
            int damage = 0;
            Random random = new Random();

            if (playerClass == PlayerClass.Thief)
            {
                // 도적인 경우 50% 확률로 두 번 공격
                if (random.NextDouble() < 1.0)
                {
                    damage = DiceRoller.RollDice(Att, Dice) + DiceRoller.RollDice(Att, Dice);
                }
                else
                {
                    damage = DiceRoller.RollDice(Att, Dice);
                }
            }
            else
            {
                // 전사, 마법사 등은 한 번 공격
                damage = DiceRoller.RollDice(Att, Dice);
            }

            return damage;
        }
    }
}
