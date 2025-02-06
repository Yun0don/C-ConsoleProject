using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeRpg
{
    // 플레이어 직업을 나타내는 열거형(enum)
    public enum PlayerClass
    {
        Warrior,   
        Thief,     
        Mage       
    }

    public class PlayerType : Player
    {
        private PlayerClass playerClass;
        public PlayerType(int startX, int startY, PlayerClass playerClass) : base()                                                                   
        // 부모 클래스(Player) 생성자 호출                                               
        {                                                                              
            X = startX;                                                                
            Y = startY;
            this.playerClass = playerClass;
            Symbol = GetClassSymbol();

            if (playerClass == PlayerClass.Warrior)
            {
                MaxHp = 150; // 전사: 높은 체력
                Hp = MaxHp;
                Att = 15;
            }
            else if (playerClass == PlayerClass.Thief)
            {
                MaxHp = 80;  // 도적: 낮은 체력, 높은 공격력
                Hp = MaxHp;
                Att = 20;
            }
            else if (playerClass == PlayerClass.Mage)
            {
                MaxHp = 100; // 마법사: 평범한 체력
                Hp = MaxHp;
                Att = 12;

            }
        }
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
    }
}

// 현재는 플레이어가 한 명이지만,
// 직업 확장성과 추후 멀티 캐릭터 도입 가능성을 고려하여 상속 구조로 설계했습니다.
// 상속을 통해 직업별 특성을 깔끔하게 분리하고,
// 유지보수가 용이하도록 설계하는 것이 목적입니다.

// 파셜을 쓰면 부모가 두개가되는건가? 경험치 관리는 Player Player.move가 이동을 관리함
// base로 호출하면 경험치가 출력이 안되고있음. -\





