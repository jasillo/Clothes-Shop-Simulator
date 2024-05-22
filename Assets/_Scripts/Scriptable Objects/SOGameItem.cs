using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public Sprite[] Icons => _icons;
        public int Price => _price;

        public int Code => _code;
    }

    
}
