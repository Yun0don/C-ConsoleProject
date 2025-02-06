using System;
using System.Collections.Generic;

namespace RogueLikeRpg
{
    public enum ItemType
    {
        Potion,
        Weapon,
        Armor,
    }

    public class Item
    {
        public string Name { get; protected set; }
        public ItemType Type { get; protected set; }
        public int EffectValue { get; protected set; }

        public Item(string name, ItemType type, int effectValue)
        {
            Name = name;
            Type = type;
            EffectValue = effectValue;
        }
    }

    public class ItemManager
    {
        public Dictionary<string, Item> ItemList { get; private set; }

        public ItemManager()
        {
            ItemList = new Dictionary<string, Item>
            {
                { "빨간포션", new Item("빨간포션", ItemType.Potion, 5) },
                { "주황포션", new Item("주황포션", ItemType.Potion, 10) },
                { "하얀포션", new Item("하얀포션", ItemType.Potion, 20) },
                { "몽둥이", new Item("몽둥이", ItemType.Weapon, 3) },
                { "철검", new Item("철검", ItemType.Weapon, 6) },
                { "하이랜더", new Item("하이랜더", ItemType.Weapon, 10) },
                { "천갑옷", new Item("천갑옷", ItemType.Armor, 5) },
                { "철갑옷", new Item("철갑옷", ItemType.Armor, 10) },
                { "덤불조끼", new Item("덤불조끼", ItemType.Armor, 15) }
            };
        }

        public Item GetRandomItem()
        {
            Random random = new Random();
            int index = random.Next(ItemList.Count);
            return new List<Item>(ItemList.Values)[index];
        }
    }
}
