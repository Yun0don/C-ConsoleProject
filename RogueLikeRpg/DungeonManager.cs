using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public class DungeonManager
    {
        private const int MaxFloor = 20;
        private Stack<Dungeon> dungeonStack = new Stack<Dungeon>();

        public DungeonManager()
        {
            // 첫 번째 층 생성: 1층은 내려가는 포탈만 배치
            Dungeon firstFloor = new Dungeon();
            firstFloor.PlaceDownPortal(); // 'v' 심볼
            dungeonStack.Push(firstFloor);
        }

        public Dungeon CurrentDungeon => dungeonStack.Peek();

        /// 현재 층의 맵을 플레이어 위치와 함께 출력합니다.
        public void DisplayCurrentDungeon(Player player)
        {

            CurrentDungeon.DisplayMap(player);
        }

        /// 현재 층에서 일반 아이템('?')와 주사위 아이템('%')를 체크합니다.
        public void CheckCurrentFloor(Player player, ItemManager itemManager)
        {
            CurrentDungeon.CheckForItem(player, itemManager);
            CurrentDungeon.CheckForDiceItem(player);
        }

        /// 플레이어가 현재 층의 내려가는 포탈(v) 위에 있을 경우,
        /// 새 층을 생성하여 내려가도록 합니다.
        /// 새 층에는 위 포탈('^')이 이전 층의 내려가는 포탈 위치로 강제 설정되며,
        /// 새로 내려갈 포탈('v')도 배치됩니다.
       
        public void GoDown(Player player)
        {
            if (dungeonStack.Count >= MaxFloor)
            {
                Console.WriteLine("최대 층에 도달했습니다. 더 이상 내려갈 수 없습니다.");
                return;
            }
            var currentFloor = CurrentDungeon;
            if (player.X == currentFloor.DownPortal.x && player.Y == currentFloor.DownPortal.y)
            {
                if (dungeonStack.Count == MaxFloor - 1)
                {
                    MonsterManager monsterManager = new MonsterManager();
                    monsterManager.SpawnBoss(player);
                }
                Dungeon newFloor = new Dungeon(); // 새 층 생성
                newFloor.SetUpPortal(currentFloor.DownPortal); // 이전 층의 내려가는 포탈 위치를 새 층의 위 포탈('^')로 강제 설정
                newFloor.PlaceDownPortal(); // 새 층에 내려가는 포탈('v') 배치
                dungeonStack.Push(newFloor); // 플레이어 위치를 새 층의 위 포탈 위치로 변경
                player.SetPosition(newFloor.UpPortal.x, newFloor.UpPortal.y);
                Console.WriteLine("아래층으로 내려갔습니다. 현재 층:" + dungeonStack.Count);
            }
            else
            {
                Console.WriteLine("내려가는 포탈 위에 있지 않습니다.");
            }
        }
        /// 플레이어가 현재 층의 위 포탈('^') 위에 있을 경우,
        /// 이전 층으로 올라갑니다.
        public void GoUp(Player player)
        {
            if (dungeonStack.Count <= 1)
            {
                Console.WriteLine("첫 번째 층입니다. 위로 올라갈 수 없습니다.");
                return;
            }

            var currentFloor = CurrentDungeon;
            if (player.X == currentFloor.UpPortal.x && player.Y == currentFloor.UpPortal.y)
            {
                dungeonStack.Pop();
                var previousFloor = CurrentDungeon;
                player.SetPosition(previousFloor.DownPortal.x, previousFloor.DownPortal.y);
                Console.WriteLine("위층으로 올라갔습니다.현재 층: " + dungeonStack.Count);
            }
            else
            {
                Console.WriteLine("위 포탈 위에 있지 않습니다.");
            }
        }
        public int GetCurrentFloor()
        {
            return dungeonStack.Count; 
        }

    }
}
