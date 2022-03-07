using System;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [Serializable]
    public struct ItemData
    {
        public string name;
      /*  [CustomUtils.ReadOnly]*/ public int id;
        public Sprite inventoryImage;
        public Sprite description;

        public ItemData(int id, string name, Sprite inventoryImage, Sprite description)
        {
            this.id = id;
            this.name = name;
            this.inventoryImage = inventoryImage;
            this.description = description;
        }
    }
}