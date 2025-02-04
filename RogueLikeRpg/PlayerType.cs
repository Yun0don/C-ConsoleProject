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
        Warrior,    // 전사
        Thief,      // 도적
        Mage        // 마법사
    }

    // Player 클래스 (기본 클래스)
    public class PlayerType : Player
    {
        public PlayerType(int startX, int startY, PlayerClass playerClass) 
            : base(startX, startY, playerClass) // 부모 클래스(Player) 생성자 호출
        {
            Symbol = GetClassSymbol(playerClass); // 심볼 초기화
        }

        private char GetClassSymbol(PlayerClass playerClass)
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

    
    
    
    
  