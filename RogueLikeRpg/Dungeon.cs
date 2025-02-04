using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public class Dungeon
    {
        private const int Width = 48;
        private const int Height = 27;
        public char[,] Map { get; private set; }  // 외부 접근 가능하도록 변경
        private Random random = new Random();
        private List<(int x, int y)> roomCenters = new List<(int, int)>();

        public Dungeon()
        {
            Map = new char[Height, Width];
            GenerateMap();
        }

        private void GenerateMap()
        {
            // 맵 초기화 (모두 벽으로 설정)
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Map[y, x] = '#';
                }
            }

            // 방 12
            for (int i = 0; i < 11; i++)
            {
                int startX = random.Next(1, Width - 8);
                int startY = random.Next(1, Height - 6);
                int roomWidth = random.Next(6, 10);
                int roomHeight = random.Next(4, 8);

                CreateRoom(startX, startY, roomWidth, roomHeight);


                int centerX = startX + roomWidth / 2;
                int centerY = startY + roomHeight / 2;
                roomCenters.Add((centerX, centerY));
            }

            for (int i = 0; i < roomCenters.Count - 1; i++)
            {
                ConnectRooms(roomCenters[i], roomCenters[i + 1]);
            }
        }

        private void CreateRoom(int startX, int startY, int roomWidth, int roomHeight)
        {
            for (int y = startY; y < startY + roomHeight && y < Height; y++)
            {
                for (int x = startX; x < startX + roomWidth && x < Width; x++)
                {
                    Map[y, x] = '.'; // 방 내부 표시
                }
            }
        }

        private void ConnectRooms((int x, int y) roomA, (int x, int y) roomB)
        {
            // 수평 복도
            for (int x = Math.Min(roomA.x, roomB.x); x <= Math.Max(roomA.x, roomB.x); x++)
            {
                Map[roomA.y, x] = '.';
            }

            // 수직 복도
            for (int y = Math.Min(roomA.y, roomB.y); y <= Math.Max(roomA.y, roomB.y); y++)
            {
                Map[y, roomB.x] = '.';
            }
        }
        public void ClearPlayerPosition(Player player)
        {
            Map[player.Y, player.X] = '.';
        }
        public void DisplayMap(Player player)
        {
            // 플레이어 위치 표시
            Map[player.Y, player.X] = player.Symbol;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.Write(Map[y, x]);
                    Console.Write(Map[y, x]); // 가독성을 위해 2번 출력
                }
                Console.WriteLine();
            }
        }
    }
}