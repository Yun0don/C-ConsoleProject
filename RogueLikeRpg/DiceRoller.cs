using System;
using System.Threading;

namespace RogueLikeRpg
{
    public static class DiceRoller
    {
        /// <summary>
        /// 주사위를 던져서 포물선 궤적 애니메이션을 실행한 후 최종 윗면 값을 기반으로
        /// 배율을 적용하여 최종 공격력을 결정하고 반환합니다.
        /// </summary>
        /// <param name="baseAttack">플레이어의 기본 공격력</param>
        /// <returns>계산된 최종 공격력</returns>
        public static int RollDice(int baseAttack)
        {
            Random rand = new Random();

            // TODO: 향후 다른 주사위 종류를 지원할 수 있도록 확장 (예: SilverDice, GoldDice 등)
            Dice dice = new Dice("Bronze Dice");  // Dice 클래스는 RollNorth(), RollSouth() 등 메서드를 제공한다고 가정

            // 애니메이션 설정
            int totalFrames = 20;      // 애니메이션 프레임 수
            int delay = 25;            // 프레임 간 딜레이 (밀리초)

            // 포물선 궤적 파라미터
            int startX = 0;            // 시작 X 좌표
            int startY = 15;           // "바닥" 위치에 해당하는 Y 좌표
            int peakHeight = 15;       // 궤적의 최대 높이 (값이 클수록 궤적이 높아짐)
            int horizontalDelta = 2;   // 프레임당 X 좌표 증가량

            // 포물선 궤적 애니메이션 실행
            for (int frame = 0; frame <= totalFrames; frame++)
            {
                Console.Clear();
                Console.WriteLine("스페이스바 => 애니메이션 스킵");

                // t는 0 ~ π 사이의 값으로, sin(t)를 이용해 포물선 궤적 (0 → 1 → 0)을 만듭니다.
                double t = Math.PI * frame / totalFrames;
                int currentY = startY - (int)(peakHeight * Math.Sin(t));
                int currentX = startX + horizontalDelta * frame;

                // 매 프레임마다 무작위 회전 동작을 적용하여 실제 주사위의 구조(반대 면의 합 7)를 반영합니다.
                int move = rand.Next(0, 6);
                switch (move)
                {
                    case 0:
                        dice.RollNorth();
                        break;
                    case 1:
                        dice.RollSouth();
                        break;
                    case 2:
                        dice.RollEast();
                        break;
                    case 3:
                        dice.RollWest();
                        break;
                    case 4:
                        dice.RotateClockwise();
                        break;
                    case 5:
                        dice.RotateCounterClockwise();
                        break;
                }

                // 현재 주사위 상태(윗면, 앞면, 오른쪽면)를 ASCII 아트로 생성합니다.
                string[] diceArt = GetCubeFrame(dice.Top, dice.Front, dice.Right);

                // 계산된 (currentX, currentY) 좌표에 주사위 출력
                DrawCubeAtPosition(diceArt, currentX, currentY);
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        break; // 애니메이션 루프 종료
                    }
                }
                Thread.Sleep(delay);
            }

            // 애니메이션 종료 후 최종 주사위의 윗면 값을 이용하여 배율 결정
            int finalDiceFace = dice.Top;
            double multiplier = 1.0;
            switch (finalDiceFace)
            {
                case 1:
                    multiplier = 0.1;
                    break;
                case 2:
                    multiplier = 0.5;
                    break;
                case 3:
                case 4:
                    multiplier = 1.0;
                    break;
                case 5:
                    multiplier = 1.5;
                    break;
                case 6:
                    multiplier = 2.0;
                    break;
            }

            int finalAttack = (int)Math.Ceiling(baseAttack * multiplier);

            // 최종 결과 출력
            Console.Clear();
            Console.WriteLine($"눈금 => {finalDiceFace}");
            Console.WriteLine($"비율 => {multiplier}");
            Console.WriteLine($"최종 공격력 => {finalAttack}");
            Thread.Sleep(1000);

            return finalAttack;
        }

        // 주사위의 3면(윗면, 앞면, 오른쪽면)을 ASCII 아트로 생성하는 메서드
        private static string[] GetCubeFrame(int top, int front, int right)
        {
            return new string[]
            {
                "       ________",
                "      /       /|",
                $"     /   {top}   / |",
                "    /_______/  |",
                "    |       |  |",
                $"    |   {front}   | {right}|",
                "    |       |  /",
                "    |_______|/"
            };
        }

        // 지정된 (x, y) 좌표에서부터 ASCII 아트를 출력하는 메서드
        private static void DrawCubeAtPosition(string[] art, int x, int y)
        {
            for (int i = 0; i < art.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(art[i]);
            }
        }
    }
}
