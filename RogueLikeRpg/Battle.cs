using System;

namespace RogueLikeRpg
{
    public class Battle
    {
        private Player player;
        private Monster? monster; // 일반 몬스터 전투 여부
        private Boss? boss;       // 보스 전투 여부

        // 일반 몬스터 전투 생성자
        public Battle(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;
        }

        // 보스 전투 생성자 
        public Battle(Player player, Boss boss)
        {
            this.player = player;
            this.boss = boss;
        }

        public void StartBattle()
        {
            Console.Clear();
            while (player.Hp > 0 && (monster.HasValue && monster.Value.Hp > 0))
            {
                Console.Clear();
                DisplayStatus(monster.Value.Name, monster.Value.Hp, monster.Value.MaxHp, monster.Value.Att);
                PlayerTurn();
                Console.ReadKey();
                if (monster.HasValue && monster.Value.Hp <= 0) break;
                MonsterTurn();
                Console.ReadKey();
            }
            EndBattle();
        }

        public void StartBossBattle()
        {
            Console.Clear();
            while (player.Hp > 0 && (boss.HasValue && boss.Value.Hp > 0))
            {
                BossImage();
                DisplayStatus(boss.Value.Name, boss.Value.Hp, boss.Value.MaxHp, boss.Value.Att);
                PlayerTurn();
                Console.ReadKey();
                if (boss.HasValue && boss.Value.Hp <= 0) break;
                BossTurn();
                Console.ReadKey();
                Console.Clear();

            }
            EndBossBattle();
        }

        private void DisplayStatus(string enemyName, int enemyHp, int enemyMaxHp, int enemyAtt)
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"        Lv.{player.Level}  EXP {player.Exp} / {player.MaxExp}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        HP {player.Hp} / {player.MaxHp}  공격력 {player.Att}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        적: {enemyName}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        HP: {enemyHp} / {enemyMaxHp}");
            Console.WriteLine($"        공격력: {enemyAtt}");
            Console.WriteLine("=====================================");
        }

        private void PlayerTurn()
        {
            Console.WriteLine("[플레이어 턴]");
            Console.WriteLine("1. 공격");
            Console.Write("선택: ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (choice == '1')
            {
                int damage = player.Attack();
                if (monster.HasValue)
                {
                    // 임시 변수에 복사해서 체력 업데이트 후 다시 할당
                    Monster temp = monster.Value;
                    temp.TakeDamage(damage);
                    monster = temp;
                }
                else if (boss.HasValue)
                {
                    Boss temp = boss.Value;
                    temp.TakeDamage(ref temp, damage);
                    boss = temp;
                }
            }
            else if (choice == '2')
            {
                player.ShowInventory();
            }
            else
            {
                Console.WriteLine("턴을 낭비 했습니다. 정신차리세요! ");
            }
        }

        private void MonsterTurn()
        {
            if (monster.HasValue)
            {
                Console.WriteLine($"{monster.Value.Name}이(가) 반격합니다!");
                int damage = monster.Value.DealDamage();
                player.TakeDamage(damage);
            }
        }

        private void BossTurn()
        {
            if (boss.HasValue)
            {
                Console.WriteLine($"{boss.Value.Name}이(가) 강력한 공격을 시도합니다!");
                int damage = boss.Value.DealDamage();
                player.TakeDamage(damage);
            }
        }

        private void EndBattle()
        {
            if (player.Hp > 0 && monster.HasValue && monster.Value.Hp <= 0)
            {
                Console.WriteLine($"\n{monster.Value.Name}을(를) 처치했습니다!");
                Console.WriteLine("경험치를 획득했습니다.");
                player.GainExp(20);
            }
            else if (player.Hp <= 0)
            {
                Console.WriteLine("\n플레이어가 쓰러졌습니다. 게임 오버!");
            }
            Console.WriteLine("\n탐험을 재개합니다. 아무 키나 누르세요...");
            Console.ReadKey();
            Console.Clear();
        }

        private void EndBossBattle()
        {
            if (player.Hp > 0 && boss.HasValue && boss.Value.Hp <= 0)
            {
                Console.WriteLine($"\n{boss.Value.Name}을(를) 쓰러뜨렸다!");
                Console.WriteLine("던전을 정복했습니다!");
                GameManager.Instance.GameClear();
            }
            else if (player.Hp <= 0)
            {
                Console.WriteLine("\n플레이어가 쓰러졌습니다. 게임 오버!");
            }
            Console.WriteLine("\n탐험을 재개합니다. 아무 키나 누르세요...");
            Console.ReadKey();
            Console.Clear();
        }
        private void BossImage()
        {
            string[] asciiArt = {
            ":::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::",
            "::::::::::::::::::--::=+=::::::::::::::::::::::::::::::-##*::=+-:::::::::::::::",
            ":::::::::::::::::*#%=:*#%=:::::::::::::::::::::::::::::*%#+:*%#+:::::::::::::::",
            "::::::::::::::==:-##%-=###::::::::::::-=---:-:::::::::-%##=+%#*:=*+::::::::::::",
            ":::::::::::=+*%%#++#%#*%%%===::::--=+==++=+=++=--::=##*%%%#%%#+*%%%+:::::::::::",
            "::::::::--=*#%%%%%%%%%%%%###*-:-=+====+++====+*++:::=*#%%%%%%%%%%%##-:-=-::::::",
            ":::::::*%%%#+*%%%%#####%##*-::=++==+*######+===+*+=::-*####%%%%%###--+%%#-:::::",
            "::::::-+**#%%#%%%%#*#%###*:::=+=-+#*++++++*%#+==+*++::-######%%%%%==#%#*-::::::",
            ":::::-#%%##%%%%%%%##%%%%#+::-+=-=#*++++*#*+*##===*#+=:-*###*##%%%%#%%#+-=+*=:::",
            ":::::-++*%%%%%%#####***##+::=*==**+***%%@%*##%*==+%+=-=*##*###%%%%%%%%##%%#=:::",
            ":::::::::=%#%%######*#**#*::-==+*#+*%#@#***%#+-====-==*#****######%%%%%%+-:::::",
            "::::::::::+%##%%%###**###%*=---=*#%%##%***#%%#-:-=+**#%#########%%#%%#%=:::::::",
            ":::-=+=-::=%#%%#**##**#%##%###*#*#####%#***#%@%%@%%%%%##%%%***##%%%##%*+**+::::",
            "::=#%%%%#*##*%%#******#%####%#++++**##****##%%%%@@%%%%%@%%******##%%%%##%#*::::",
            "::+***#%%%%####%##***########***++++++%%%%%@@@@@@@@%%###*##*#**#*##%%###+--=++:",
            "=#%%%%%%%%%%##++###%%%#%%%%%%*%%+++***%#*%##@@@@@@@#########**#####%%%%++*#%%#=",
            "-**+*#%%%%##**%#+=+%%@@%%%%*##**#%#*%%%***+*%@*+#%%%%%%%%##@%%#####%%%%%%##+=-:",
            ":=#%%%%%%%%#%%###**++######*+##*###+#*#*##*#%#-+%%%@@@@@@@@@%%###*###%%%%#++++=",
            ":=#*+*%%###%@#*#****####%%%*+++++******++***#*=*%%%%%*++++=++***%%%##%%%%%%%##*",
            "::-+###**#####***#**%######**++==++*#*+++++*#*+*%%%%%#**+==+**#**#%@##%%%*=-:::",
            "::##%%#####*##%****#%%####*#%+==+*++##******%*+*#%%%%%%%#%%**#****#%%###*=-::::",
            "::+%##%%%%##*#%*#***#%##%%%%#++++**+++*%#*++***#%%%%######%#***####*##**#%%#=::",
            "::=%#%%%%%%##%#####**###%#==+#######*+==*%#=****%%%%%%%##%%#****%#*##%#######::",
            ":::*##%@%%%#@@####**#%%@#*===#####*#*+==#%%+###+#%%%@%%##%**#*#*######%%%%#%+::",
            ":::-%#*+**#%@%%%%%%%###%%%==+###*###++==*%%*%%##%%%%@@%%%#***####%%#%%%%%%#%=::",
            "::::--:::::-=+++++=-=*%@@@%%#**#####++*#####%%%%@@@%%%@%%%#######%@%#%%%%##+:::",
            ":::::::::::::::::::=%@%%%%%@%++*****++#@%#*#*#*#@@%%%%%%*+*##%%%%%%#*==--*#::::",
            ":::::::::::::::===*+**#%%%%%*========++**++++++*#%%%%@%%#+::::---::::::::::::::",
            "::::::::::::::*@%*##%%%%*+#%+===========+=+=====#%#%%%%%%%*=:::::::::::::::::::",
            "::::::::::::=+##+=*####*=:-#+--------==-----+=*#%*#%%%%%##%%**-::::::::::::::::"
        };

            foreach (string line in asciiArt)
            {
                Console.WriteLine(line);
            }
        }

    }
}
