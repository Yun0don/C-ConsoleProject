using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public class Dungeon
    {
        private const int Width = 48;
        private const int Height = 27;
        public char[,] Map { get; private set; }  // 외부에서 읽기 가능
        private Random random = new Random();
        private ItemManager itemManager = new ItemManager();
        private List<(int x, int y)> roomCenters = new List<(int, int)>();

        // 포탈 좌표 (내려가는 포탈: 'v', 위로 올라가는 포탈: '^')
        public (int x, int y) DownPortal { get; private set; }
        public (int x, int y) UpPortal { get; private set; }

        public Dungeon()
        {
            Map = new char[Height, Width];
            GenerateMap();
            PlaceItems();     // 일반 아이템 ('?')
            PlaceDiceItems(); // 주사위 아이템 ('%')
        }

        private void GenerateMap()
        {
            // 전체 맵을 벽('#')으로 초기화
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Map[y, x] = '#';

            // 방 11개 생성
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

            // 방들을 연결하는 복도 생성
            for (int i = 0; i < roomCenters.Count - 1; i++)
                ConnectRooms(roomCenters[i], roomCenters[i + 1]);
        }

        private void CreateRoom(int startX, int startY, int roomWidth, int roomHeight)
        {
            for (int y = startY; y < startY + roomHeight && y < Height; y++)
                for (int x = startX; x < startX + roomWidth && x < Width; x++)
                    Map[y, x] = '.'; // 방 내부는 바닥('.')
        }

        private void ConnectRooms((int x, int y) roomA, (int x, int y) roomB)
        {
            // 수평 복도
            for (int x = Math.Min(roomA.x, roomB.x); x <= Math.Max(roomA.x, roomB.x); x++)
                Map[roomA.y, x] = '.';
            // 수직 복도
            for (int y = Math.Min(roomA.y, roomB.y); y <= Math.Max(roomA.y, roomB.y); y++)
                Map[y, roomB.x] = '.';
        }

        private void PlaceItems()
        {
            // 예: 3~7개의 일반 아이템 ('?') 배치
            int numberOfItems = random.Next(3, 8);
            for (int i = 0; i < numberOfItems; i++)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Map[y, x] == '.')
                    Map[y, x] = '?';
                else
                    i--;
            }
        }

        public void CheckForItem(Player player, ItemManager itemManager)
        {
            if (Map[player.Y, player.X] == '?')
            {
                Item foundItem = itemManager.GetRandomItem();
                player.ObtainItem(foundItem);
                Map[player.Y, player.X] = '.';
            }
        }
        public void ClearPlayerPosition(Player player)
        {
             Map[player.Y, player.X] = '.';
        }
        // 내려가는 포탈 배치 ('v')
        public void PlaceDownPortal()
        {
            int count = 0;
            while (true)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Map[y, x] == '.')
                {
                    Map[y, x] = 'v';
                    DownPortal = (x, y);
                    break;
                }
                if (++count > 1000) break;
            }
        }

        // 위로 올라가는 포탈 배치 ('^')
        public void PlaceUpPortal()
        {
            int count = 0;
            while (true)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Map[y, x] == '.')
                {
                    Map[y, x] = '^';
                    UpPortal = (x, y);
                    break;
                }
                if (++count > 1000) break;
            }
        }

        /// <summary>
        /// 외부에서 전달한 좌표를 기반으로 위로 올라가는 포탈을 강제로 설정합니다.
        /// </summary>
        public void SetUpPortal((int x, int y) portalCoord)
        {
            if (portalCoord.x >= 0 && portalCoord.x < Width &&
                portalCoord.y >= 0 && portalCoord.y < Height &&
                Map[portalCoord.y, portalCoord.x] == '.')
            {
                Map[portalCoord.y, portalCoord.x] = '^';
                UpPortal = portalCoord;
            }
            else
            {
                PlaceUpPortal();
            }
        }

        public void PlaceDiceItems()
        {
            int count = random.Next(2, 5);
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(0, Width);
                int y = random.Next(0, Height);
                if (Map[y, x] == '.')
                    Map[y, x] = 'ㅁ';
                else
                    i--;
            }
        }

        public void CheckForDiceItem(Player player)
        {
            if (Map[player.Y, player.X] == 'ㅁ')
            {
                Dice newDice = DiceManager.GetRandomDice();
                player.EquipDice(newDice);
                Map[player.Y, player.X] = '.';
            }
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
