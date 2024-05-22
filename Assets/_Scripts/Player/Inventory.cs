using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [SerializeField] private int _initialGold = 1000;
        [SerializeField] private int _maxItems = 20;
        [SerializeField] private List<SOGameItem> _items;

        private int _currentGold;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError("instance already exist");

            _currentGold = _initialGold;
        }


        public List<SOGameItem> Data
        {
            get => new (_items);
            set => _items = value;
        }

        public int Gold => _currentGold;
    }
}
