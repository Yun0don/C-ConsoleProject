using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RogueLikeRpg
{
    public class Dice
    {
        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Front { get; private set; }
        public int Back { get; private set; }
        public int Right { get; private set; }
        public int Left { get; private set; }

        public string Name { get; private set; }


        // 기본 주사위 배치: 윗면:1, 아랫면:6, 앞면:2, 뒷면:5, 오른쪽면:3, 왼쪽면:4
        public Dice(string name)
        {
            Name = name;
            Top = 1;
            Bottom = 6;
            Front = 2;
            Back = 5;
            Right = 3;
            Left = 4;
        }
        public Dice(string name, int[] faces)
        {
            if (faces.Length != 6)
                throw new ArgumentException("Faces 배열은 6개의 숫자를 포함해야 합니다.");
            Name = name;
            Top = faces[0];
            Bottom = faces[1];
            Front = faces[2];
            Back = faces[3];
            Right = faces[4];
            Left = faces[5];
        }



        #region
        // 주사위를 북쪽(앞쪽)으로 굴리기: 윗면 → 앞면 → 아랫면 → 뒷면 순으로 이동
        public void RollNorth()
        {
            int temp = Top;
            Top = Front;
            Front = Bottom;
            Bottom = Back;
            Back = temp;
        }

        // 주사위를 남쪽(뒷쪽)으로 굴리기
        public void RollSouth()
        {
            int temp = Top;
            Top = Back;
            Back = Bottom;
            Bottom = Front;
            Front = temp;
        }

        // 주사위를 동쪽(오른쪽)으로 굴리기: 윗면 → 왼쪽 → 아랫면 → 오른쪽 순으로 이동
        public void RollEast()
        {
            int temp = Top;
            Top = Left;
            Left = Bottom;
            Bottom = Right;
            Right = temp;
        }

        // 주사위를 서쪽(왼쪽)으로 굴리기
        public void RollWest()
        {
            int temp = Top;
            Top = Right;
            Right = Bottom;
            Bottom = Left;
            Left = temp;
        }

        // 수직축(윗면/아랫면 고정)을 중심으로 시계방향 회전 (앞, 오른쪽, 뒤, 왼쪽의 위치만 변경)
        public void RotateClockwise()
        {
            int temp = Front;
            Front = Left;
            Left = Back;
            Back = Right;
            Right = temp;
        }

        // 수직축을 중심으로 반시계방향 회전
        public void RotateCounterClockwise()
        {
            int temp = Front;
            Front = Right;
            Right = Back;
            Back = Left;
            Left = temp;
        }
    }
}
    #endregion
    // DiceRoller 클래스: 주사위 던지기 기능을 하나의 메서드로 제공
    

//    int attackPower = DiceRoller.RollDice(); // 단일 메서드 호출