using System;

namespace RogueLikeRpg
{
    public static class DiceManager
    {
        private static Random random = new Random();

        public static Dice GetRandomDice()
        {
            int roll = random.Next(100);

            if (roll < 5) // 5
            {
                return new Dice("너가 멸망되는 주사위", new int[] { 1, 1, 1, 1, 1, 1 });
            }
            else if (roll < 10) // 5
            {
                return new Dice("만들다 만 주사위", new int[] { 1, 1, 1, 1, 2, 3 });
            }
            else if (roll < 15) // 5
            {
                return new Dice("나락의 주사위", new int[] { 1, 1, 1, 1, 3, 3 });
            }
            else if (roll < 25) // 10
            {
                return new Dice("일상의 주사위", new int[] { 1, 3, 1, 3, 1, 3 });
            }
            else if (roll < 35) // 10
            {
                return new Dice("도박사의 주사위", new int[] { 2, 4, 2, 4, 2, 4 });
            }
            else if (roll < 45) // 10
            {
                return new Dice("모와 도의 주사위", new int[] { 1, 6, 1, 6, 1, 6 });
            } 
            else if (roll < 50) //5
            {
                return new Dice("삼위일체의 주사위", new int[] { 1, 3, 5, 1, 3, 5 });
            }
            else if (roll < 70) // 20
            {
                return new Dice("버렸던 주사위", new int[] { 1, 2, 3, 4, 5, 6 });
            }
            else if (roll < 75) // 5
            {
                return new Dice("극락일뻔한 주사위", new int[] { 1, 1, 6, 1, 1, 6 });
            }
            else if (roll < 95)  // 20
            {
                return new Dice("돌아온 주사위", new int[] { 1, 2, 3, 4, 5, 6 });
            }
            else if (roll < 97) // 2
            {
                return new Dice("전설의 주사위", new int[] { 5, 5, 5, 5, 5, 5 });
            }
            else if (roll < 99) // 2
            {
                return new Dice("신의 주사위", new int[] { 5, 5, 5, 6, 6, 6 });
            }
            else // 1
            {
                return new Dice("엔딩보는 주사위", new int[] { 6, 6, 6, 6, 6, 6 });
            }
        }
    }
}
