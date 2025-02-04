using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeRpg
{
    public partial class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Symbol { get; set; }

        public Player(int startX, int startY, PlayerClass playerClass)
        {
            X = startX;
            Y = startY;
            Symbol = GetClassSymbol(playerClass);
        }

        private char GetClassSymbol(PlayerClass playerClass)
        {
            if (playerClass == PlayerClass.Warrior)
                return '@';
            else if (playerClass == PlayerClass.Thief)
                return '$';
            else if (playerClass == PlayerClass.Mage)
                return '*';
            else
                return '?';
        }

        public void Move(char direction, char[,] map)
        {
            int newX = X;
            int newY = Y;

            if (direction == 'w') newY--; // 위로 이동
            else if (direction == 's') newY++; // 아래로 이동
            else if (direction == 'a') newX--; // 왼쪽으로 이동
            else if (direction == 'd') newX++; // 오른쪽으로 이동
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            if (IsValidMove(newX, newY, map))
            {
                X = newX;
                Y = newY;
            }
        }

        private bool IsValidMove(int x, int y, char[,] map)
        {
            return x >= 0 && x < map.GetLength(1) &&
                   y >= 0 && y < map.GetLength(0) &&
                   map[y, x] != '#';
        }
    }
}
