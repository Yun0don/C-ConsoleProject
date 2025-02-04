using System;

namespace RogueLikeRpg
{
    public class Dungeon
    {
        private const int WIDTH = 20;
        private const int HEIGHT = 10;
        private char[,] map = new char[HEIGHT, WIDTH];

        public Dungeon()
        {
            GenerateMap();
        }

        public void GenerateMap()
        {
            // 맵 초기화 (모두 벽으로 설정)
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    map[y, x] = '#';
                }
            }
        }
       public void DisplayMap()
       {
           for (int y = 0; y < HEIGHT; y++) // 10까지 세로
           {
               for (int x = 0; x < WIDTH; x++) // 20까지 가로 
               {
                   Console.Write(map[y, x]);
               }
               Console.WriteLine();
           }
       }
    }
}

// 랜덤 방 생성

//  private void CreateRoom(int startX, int startY, int roomWidth, int roomHeight)
//  {
//      for (int y = startY; y < startY + roomHeight; y++)
//      {
//          for (int x = startX; x < startX + roomWidth; x++)
//          {
//              map[y, x] = '.';
//          }
//      }
//  }
//  private void ConnectRooms(int x1, int y1, int x2, int y2)
//  {
//      // 수평 복도 생성
//      for (int x = Math.Min(x1, x2); x <= Math.Max(x1, x2); x++)
//      {
//          map[y1, x] = '.';
//      }
// 
//      // 수직 복도 생성
//      for (int y = Math.Min(y1, y2); y <= Math.Max(y1, y2); y++)
//      {
//          map[y, x2] = '.';
//      }
//  }