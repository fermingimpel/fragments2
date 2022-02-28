using System;
using UnityEngine;
using Items;

namespace Items
{
    [Serializable]
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "ScriptableObjects/ItemInfo", order = 1)]
    public class ItemInfo : ScriptableObject
    {
        public ItemData item;

        public void SetId(int id)
        {
            item.id = id;
        }
    }
}