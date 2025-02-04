using System;

namespace RogueLikeRpg
{
    public class GameManager
    {
        // 싱글톤 인스턴스
        private static GameManager instance = null;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        // 생성자 private → 외부에서 인스턴스 생성 방지
        private GameManager() { }

        // 게임 시작
        public void GameStart()
        {
            Console.WriteLine("====================================================");
            Console.WriteLine("====================================================");
            Console.WriteLine("=============== C# Roguelike RPG ===================");
            Console.WriteLine("====================================================");
            Console.WriteLine("===============    GAME START    ===================");
            Console.WriteLine("====================================================");
            Console.WriteLine("===============   Press any key  ===================");
            Console.WriteLine("====================================================");
            Console.WriteLine("====================================================");
            Console.ReadKey();

            PlayerClass chosenClass = SelectClassType();
            PlayerType player = new PlayerType(24, 12, chosenClass); // 맵 정중앙
            Dungeon dungeon = new Dungeon();

            GameLoop(player, dungeon);
        }

        // 직업 선택
        private PlayerClass SelectClassType()
        {
            Console.Clear();
            Console.WriteLine("=====================================================");
            Console.WriteLine("=====================================================");
            Console.WriteLine("==========       직업을 선택하세요         ==========");
            Console.WriteLine("=====================================================");
            Console.WriteLine("==========   1.전사   2.도적   3.마법사    ==========");
            Console.WriteLine("=====================================================");
            Console.WriteLine("=====================================================");

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

        // 메인 게임 루프
        private void GameLoop(PlayerType player, Dungeon dungeon)
        {
            while (true)
            {
                Console.SetCursorPosition(0, 0); // 깜빡임 방지
                dungeon.DisplayMap(player);

                Console.WriteLine("이동: W(↑) A(←) S(↓) D(→)");
                Console.Write("명령 입력: ");
                char input = char.ToLower(Console.ReadKey().KeyChar);
                dungeon.ClearPlayerPosition(player);
                player.Move(input, dungeon.Map);
            }
        }
    }
}
