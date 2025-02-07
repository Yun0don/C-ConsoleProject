using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueLikeRpg // partial도 부모취급당한다.
{
    public partial class Player
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public char Symbol { get; protected set; }
        private const int VIEW_RANGE = 5; // 3x3 시야 반경



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

        public void Move(char direction, char[,] map, Player player)
        {
            int newX = X;
            int newY = Y;

            if (direction == 'w') newY--; // 위로 이동
            else if (direction == 's') newY++; // 아래로 이동
            else if (direction == 'a') newX--; // 왼쪽으로 이동
            else if (direction == 'd') newX++; // 오른쪽으로 이동
            else if (direction == 'i')
            {
                if (IsInventoryOpen)
                {
                    CloseInventory();
                }
                else
                {
                    ShowInventory();
                }
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
        public void RenderMap(Dungeon dungeon)
        {

            int mapHeight = 27; // 
            int mapWidth = 48;  // 

            char[,] tempMap = (char[,])dungeon.Map.Clone(); // 원본 맵 보호

            // 플레이어 위치 표시
            tempMap[Y, X] = Symbol;

            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    if (y == 0 || y == 26 || x == 0 || x == 47)
                    {
                        Console.Write("##"); 
                    }
                    // 내부 타일은 시야 제한을 적용하여 출력
                    else if (Math.Abs(X - x) <= VIEW_RANGE && Math.Abs(Y - y) <= VIEW_RANGE)
                    {
                        Console.Write(tempMap[y, x]); // 내부 맵 출력
                        Console.Write(tempMap[y, x]); // 가독성을 위해 두 번 출력
                    }
                    else
                    {
                        Console.Write("  "); // 시야 밖은 공백 처리
                    }
                }
                Console.WriteLine();
            }
        }

        // public void DrawOutLine(Dungeon dungeon)
        // {
        //     Console.Clear();
        // 
        //     for (int y = 0; y < 27; y++) // Height = 27
        //     {
        //         for (int x = 0; x < 48; x++) // Width = 48
        //         {
        //             // 맵의 외곽 테두리(0, 26, 0, 47)는 항상 '#' 출력
        //             if (y == 0 || y == 26 || x == 0 || x == 47)
        //             {
        //                 Console.Write("##"); // 외곽을 항상 보이게 두 번 출력
        //             }
        //             else
        //             {
        //                 Console.Write(dungeon.Map[y,x]); // 내부는 공백 (시야 제한 따로 적용 가능)
        //                 Console.Write(dungeon.Map[y,x]); // 내부는 공백 (시야 제한 따로 적용 가능)
        //             }
        //         }
        //         Console.WriteLine();
        //     }
        // }
    }   // 
}
