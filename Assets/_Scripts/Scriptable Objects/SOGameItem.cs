using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    [CreateAssetMenu(fileName = "item", menuName = "MyGame/item", order = 1)]
    public class SOGameItem : ScriptableObject
    {

        [SerializeField] TGameItem _type;
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _icons;
        [SerializeField] private int _price;
        [SerializeField] private int _code;

        public TGameItem Type => _type;
        public string ItemName => _name;
        public int Price => _price;

        public int Code => _code;
        public Sprite Icon(int index)
        {
            Assert.IsFalse(_icons.Length == 0, "No icons included in game item");

            index = Mathf.Clamp(index, 0, _icons.Length - 1);
            return _icons[index];
        }
    }

    
}
