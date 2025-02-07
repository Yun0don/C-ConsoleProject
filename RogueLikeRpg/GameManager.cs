using System;

namespace RogueLikeRpg
{
    public class GameManager
    {
        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();
                return instance;
            }
        }
        private GameManager() { }

        public void GameStart()
        {
            Console.WriteLine("****************************************************");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("* ■■■    ■■■   ■    ■   ■■■   ■■■■  *");
            Console.WriteLine("* ■   ■  ■    ■  ■    ■  ■        ■        *");
            Console.WriteLine("* ■■■   ■    ■  ■    ■  ■■■■  ■■■■  *");
            Console.WriteLine("* ■  ■   ■    ■  ■    ■  ■    ■  ■        *");
            Console.WriteLine("* ■   ■   ■■■   ■■■■   ■■■   ■■■■  *");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("****************************************************");
            Console.WriteLine("*                G A M E   S T A R T               *");
            Console.WriteLine("****************************************************");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("*              PRESS ANY KEY TO CONTINUE...        *");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("****************************************************");
            Console.ReadKey();

            PlayerClass chosenClass = SelectClassType();
            PlayerType player = new PlayerType(24, 12, chosenClass); // 맵 정중앙

            DungeonManager dungeonManager = new DungeonManager();
            MonsterManager monsterManager = new MonsterManager();
            ItemManager itemManager = new ItemManager();
           
            GameLoop(player, dungeonManager, monsterManager, itemManager);
        }

        private PlayerClass SelectClassType()
        {
            Console.Clear();
            Console.WriteLine("****************************************************");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("* ■■■    ■■■   ■    ■   ■■■   ■■■■  *");
            Console.WriteLine("* ■   ■  ■    ■  ■    ■  ■        ■        *");
            Console.WriteLine("* ■■■   ■    ■  ■    ■  ■■■■  ■■■■  *");
            Console.WriteLine("* ■  ■   ■    ■  ■    ■  ■    ■  ■        *");
            Console.WriteLine("* ■   ■   ■■■   ■■■■   ■■■   ■■■■  *");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("****************************************************");
            Console.WriteLine("*                직업을 선택하세요                 *");
            Console.WriteLine("****************************************************");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("*            1.전사   2.도적   3.마법사            *");
            Console.WriteLine("*                                                  *");
            Console.WriteLine("****************************************************");

            while (true)
            {
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (input)
                {
                    case '1': return PlayerClass.Warrior;
                    case '2': return PlayerClass.Thief;
                    case '3': return PlayerClass.Mage;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 1, 2, 3 중 하나를 선택하세요.");
                        break;

                }
            }
        }

        private void GameLoop(PlayerType player, DungeonManager dungeonManager,
                            MonsterManager monsterManager, ItemManager itemManager  )
        {
            Console.Clear();
            Random random = new Random();

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                player.RenderMap(dungeonManager.CurrentDungeon); // 플레이어 시야 제한 적용하여 맵 출력
                player.CreatePlayer();

                Console.WriteLine("이동: W(↑) A(←) S(↓) D(→) / >: 내려가기 / <: 올라가기");
                char input = char.ToLower(Console.ReadKey().KeyChar);

                if (player.IsInventoryOpen)
                {
                    player.HandleInventoryInput(input);
                }
                else
                {
                    dungeonManager.CurrentDungeon.ClearPlayerPosition(player);

                    if (input == '.') // 내려가기
                    {
                        if (player.X == dungeonManager.CurrentDungeon.DownPortal.x && player.Y == dungeonManager.CurrentDungeon.DownPortal.y)
                        {
                            dungeonManager.GoDown(player);
                        }
                        else
                        {
                            Console.WriteLine("포탈이 없습니다. 내려갈 수 없습니다.");
                        }
                    }
                    else if (input == ',') //올라가기
                    {
                        if (player.X == dungeonManager.CurrentDungeon.UpPortal.x && player.Y == dungeonManager.CurrentDungeon.UpPortal.y)
                        {
                            dungeonManager.GoUp(player);
                        }
                        else
                        {
                            Console.WriteLine("포탈이 없습니다. 올라갈 수 없습니다.");
                        }
                    }
                    else
                    {
                        player.Move(input, dungeonManager.CurrentDungeon.Map, player);
                    }
                    dungeonManager.CheckCurrentFloor(player, itemManager);

                    // 5% 확률로 몬스터 전투 시작
                    if (random.Next(100) < 0)
                    {
                        Console.Clear();
                        monsterManager.SpawnMonster(player);
                    }
                }
            }
        }

    }
}
