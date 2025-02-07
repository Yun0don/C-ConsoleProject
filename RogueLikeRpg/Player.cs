using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public abstract partial class Player
    {
        public int Hp { get; protected set; }
        public int MaxHp { get; protected set; }
        public int Att { get; protected set; }
        public int Exp { get; protected set; }
        public int MaxExp { get; protected set; }
        public int Level { get; protected set; }
        // 템 및 주사위 관리
        public Dice Dice { get; private set; }
        public List<Item> Inventory { get; private set; }
        public bool IsInventoryOpen = false;
        private int selectedItemIndex = 0;
        public Player()
        {
            Level = 1;
            Exp = 0;
            MaxExp = 100;
            Inventory = new List<Item>();
            Dice = new Dice("기본주사위");
        }
        // 직업별 문양 표시를 자식에서 가져옴
        protected abstract char GetClassSymbol();
        public void CreatePlayer(DungeonManager dungeonManager)
        {

            Console.WriteLine("=====================================");
            Console.WriteLine($"       직업 {GetClassSymbol()}  Lv.{Level}  EXP {Exp} / {MaxExp}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        HP {Hp} / {MaxHp}  공격력 {Att}");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        보유 => {Dice.Name}              ");
            Console.WriteLine("=====================================");
            int[] faces = new int[] { Dice.Top, Dice.Bottom, Dice.Front, Dice.Back, Dice.Right, Dice.Left };
            Array.Sort(faces);
            Console.WriteLine("        눈금 => " + string.Join(",", faces));
            // 보유주사위 눈금 => ex 1,2,3,4,5,6
            Console.WriteLine("=====================================");
            Console.WriteLine($"        인벤토리 열기 => (i) ");
            Console.WriteLine("=====================================");
            Console.WriteLine($"        현재 던전 => {dungeonManager.GetCurrentFloor()}층");
            Console.WriteLine("=====================================");
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Console.WriteLine("플레이어가 사망했습니다.");
              //   Respawn();
            }
        }

        public void GainExp(int amount)
        {
            // 몬스터 별로 경험치 차등화
            Exp += amount;
            if (Exp >= MaxExp)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Exp = 0;
            MaxExp += 20;
            Hp = MaxHp;
            MaxHp += 20;
            Att += 5;
            Console.WriteLine($"레벨업! 현재 레벨: {Level}");
        }

    //   private void Respawn()
    //   {
    //       Console.WriteLine("부활 중... 아이템은 유지됩니다.");
    //       Hp = MaxHp;
    //       Exp = 0;
    //   }


        // 아이템 클래스 관련 메서드 간접접근
        public void DrinkPortion(int amount)
        {
            Hp = Math.Min(Hp + amount, MaxHp);
        }
        public void UseWeaponItem(int amount)
        {
            Att += amount;
        }
        public void UseArmorItem(int amount)
        {
            MaxHp += amount;
            Hp += amount;
        }
        // 주사위 관련 메서드
        public void EquipDice(Dice newDice)
        {
            Dice = newDice;
            Console.WriteLine($"주사위가 {newDice.Name}으로 교체되었습니다!");
        }

        // 인벤관련 메서드
        public void ObtainItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine("  ");
            Console.WriteLine($"{item.Name}을 (를) 획득했습니다!");
        }
        public void ShowInventory()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("==========    인벤토리    ==========");
            if (Inventory.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
            }
            else
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    if (i == selectedItemIndex)
                        Console.WriteLine($"> {Inventory[i].Name} ({Inventory[i].Type})");  
                    else
                        Console.WriteLine($"  {Inventory[i].Name} ({Inventory[i].Type})");
                }
            }
            Console.WriteLine("=========     아이템 선택   ==========");
            Console.WriteLine("==========  (1) ↓ (2) ↑ ===========");
            Console.WriteLine("=====   Enter: 사용 i: 닫기    ======");
            Console.WriteLine("=====================================");

            IsInventoryOpen = true;
        }
        public void CloseInventory()
        {
            Console.Clear();
            IsInventoryOpen = false;
        }
        public void HandleInventoryInput(char input)
        {
            if (!IsInventoryOpen) return;

            switch (input)
            {
                case '2':  // 위
                    selectedItemIndex = (selectedItemIndex - 1 + Inventory.Count) % Inventory.Count;
                    ShowInventory();
                    break;

                case '1':  // 아래로
                    selectedItemIndex = (selectedItemIndex + 1) % Inventory.Count;
                    ShowInventory();
                    break;

                case (char)13:  // Enter 키 (아이템 사용)
                    if (Inventory.Count > 0)
                    {
                        Item selectedItem = Inventory[selectedItemIndex];

                       
                        switch (selectedItem.Type)
                        {
                            case ItemType.Potion:
                                DrinkPortion(selectedItem.EffectValue); 
                                Console.WriteLine($"{selectedItem.Name}을(를) 사용 HP +{selectedItem.EffectValue} 회복");
                                break;

                            case ItemType.Weapon:
                                UseWeaponItem(selectedItem.EffectValue); 
                                Console.WriteLine($"{selectedItem.Name}을 장착 공격력 +{selectedItem.EffectValue} 증가");
                                break;

                            case ItemType.Armor:
                                UseArmorItem(selectedItem.EffectValue);  
                                Console.WriteLine($"{selectedItem.Name}을 착용 최대 HP +{selectedItem.EffectValue} 증가");
                                break;
                        }

                        Inventory.RemoveAt(selectedItemIndex);
                        selectedItemIndex = Math.Max(0, selectedItemIndex - 1); 
                        ShowInventory();  
                    }
                    break;

                case 'i':  
                    CloseInventory();
                    break;
            }
        }

        // 프로퍼티 때문에 만든 좌표값 옵저버에도 활용예정
        public void SetPosition(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
        public virtual int Attack()
        {
            return Att;
        }










    }
}


