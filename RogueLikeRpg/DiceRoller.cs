using System;
using System.Threading;

namespace RogueLikeRpg
{
    public static class DiceRoller
    {
        // Random 인스턴스를 클래스 수준에서 생성 (여러번 새 인스턴스를 만들지 않도록)
        private static Random rand = new Random();

        /// <summary>
        /// 플레이어가 장착한 주사위를 이용하여 애니메이션 효과와 함께 주사위를 굴린 후,
        /// 최종 윗면에 따른 배율을 적용하여 최종 공격력을 계산하고 반환합니다.
        /// </summary>
        /// <param name="baseAttack">플레이어의 기본 공격력</param>
        /// <param name="dice">플레이어가 장착한 주사위</param>
        /// <returns>계산된 최종 공격력</returns>
        public static int RollDice(int baseAttack, Dice dice)
        {
            // 애니메이션 설정
            int totalFrames = 20;      // 애니메이션 프레임 수
            int delay = 25;            // 프레임 간 딜레이 (밀리초)
            int startX = 0;            // 애니메이션 시작 X 좌표
            int startY = 15;           // 애니메이션 시작 Y 좌표 ("바닥" 위치)
            int peakHeight = 15;       // 포물선 궤적의 최대 높이
            int horizontalDelta = 2;   // 프레임당 X 좌표 증가량

            // 포물선 궤적 애니메이션 실행
            for (int frame = 0; frame <= totalFrames; frame++)
            {
                Console.Clear();
                Console.WriteLine("스페이스바 => 애니메이션 스킵");

                // 0 ~ π 사이의 t 값을 이용해 포물선 궤적 (0→1→0)을 생성
                double t = Math.PI * frame / totalFrames;
                int currentY = startY - (int)(peakHeight * Math.Sin(t));
                int currentX = startX + horizontalDelta * frame;

                // 매 프레임마다 무작위 회전 동작을 적용하여 주사위의 면들을 변경함  
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

                // 주사위의 현재 상태(윗면, 앞면, 오른쪽면)를 ASCII 아트로 생성
                string[] diceArt = GetCubeFrame(dice.Top, dice.Front, dice.Right);

                // 계산된 좌표에 ASCII 아트를 출력
                DrawCubeAtPosition(diceArt, currentX, currentY);

                // 스페이스바 입력 시 애니메이션 스킵
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        break;
                    }
                }
                Thread.Sleep(delay);
            }

            // 애니메이션 종료 후 최종 윗면을 기준으로 배율 결정
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

        // 주사위의 3면(윗면, 앞면, 오른쪽면)을 ASCII 아트 문자열 배열로 생성
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

        // 지정한 (x, y) 좌표에 ASCII 아트를 출력하는 메서드
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
