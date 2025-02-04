// using System;
// using System.Collections.Generic;
// 
// namespace RogueLikeRpg
// {
//     public class Dungeon
//     {
//         private const int WIDTH = 48;
//         private const int HEIGHT = 27;
//         private char[,] map = new char[HEIGHT, WIDTH];
//         private Random random = new Random();
// 
//         private List<(int x, int y)> roomCenters = new List<(int, int)>();
// 
//         public Dungeon()
//         {
//             GenerateMap();
//         }
// 
//         private void GenerateMap()
//         {
//             InitializeMap();
// 
//             // 방 12개 생성
//             for (int i = 0; i < 12; i++)
//             {
//                 int startX = random.Next(2, WIDTH - 9);   // 테두리(@) 고려
//                 int startY = random.Next(2, HEIGHT - 7);
//                 int roomWidth = random.Next(6, 10);
//                 int roomHeight = random.Next(4, 8);
// 
//                 CreateRoom(startX, startY, roomWidth, roomHeight);
// 
//                 // 방의 중심점 저장
//                 int centerX = startX + roomWidth / 2;
//                 int centerY = startY + roomHeight / 2;
//                 roomCenters.Add((centerX, centerY));
//             }
// 
//             // 중심점끼리 복도 연결
//             for (int i = 0; i < roomCenters.Count - 1; i++)
//             {
//                 ConnectRooms(roomCenters[i], roomCenters[i + 1]);
//             }
//         }
// 
//         private void InitializeMap()
//         {
//             // 맵 초기화 (외곽은 '@', 내부는 '#')
//             for (int y = 0; y < HEIGHT; y++)
//             {
//                 for (int x = 0; x < WIDTH; x++)
//                 {
//                     if (IsBorder(x, y))
//                         map[y, x] = '@'; // 외곽 벽
//                     else
//                         map[y, x] = '#'; // 내부 벽
//                 }
//             }
//         }
// 
//         private bool IsBorder(int x, int y)
//         {
//             return (y == 0 || y == HEIGHT - 1 || x == 0 || x == WIDTH - 1);
//         }
// 
//         private void CreateRoom(int startX, int startY, int roomWidth, int roomHeight)
//         {
//             for (int y = startY; y < startY + roomHeight && y < HEIGHT - 1; y++)
//             {
//                 for (int x = startX; x < startX + roomWidth && x < WIDTH - 1; x++)
//                 {
//                     map[y, x] = '.'; // 방 내부 표시
//                 }
//             }
//         }
// 
//         private void ConnectRooms((int x, int y) roomA, (int x, int y) roomB)
//         {
//             // 수평 복도 생성
//             for (int x = Math.Min(roomA.x, roomB.x); x <= Math.Max(roomA.x, roomB.x); x++)
//             {
//                 if (map[roomA.y, x] != '@') // 외곽 벽 침범 방지
//                     map[roomA.y, x] = '.';
//             }
// 
//             // 수직 복도 생성
//             for (int y = Math.Min(roomA.y, roomB.y); y <= Math.Max(roomA.y, roomB.y); y++)
//             {
//                 if (map[y, roomB.x] != '@') // 외곽 벽 침범 방지
//                     map[y, roomB.x] = '.';
//             }
//         }
// 
//         public void DisplayMap()
//         {
//             for (int y = 0; y < HEIGHT; y++)
//             {
//                 for (int x = 0; x < WIDTH; x++)
//                 {
//                     Console.Write(map[y, x]);
//                     Console.Write(map[y, x]); // 가로 2배 출력
//                 }
//                 Console.WriteLine();
//             }
//         }
//     }
// }
// 