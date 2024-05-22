using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// General class for NPC
    /// </summary>
    public class Merchant : MonoBehaviour
    {
        [SerializeField] private Sprite _avatarIcon;
        [SerializeField] private string _greetingMsm;
        [SerializeField] private GameObject _interactionMark;
        [SerializeField] private List<SOGameItem> _products;

        private void Awake()
        {
            _interactionMark.SetActive(false);
        }

        public void Interact()
        {
            if (_products.Count > 0)
            {
                var storeScreen = StoreScreen.Instance;

                storeScreen.StoreData = _products;
                storeScreen.SetMerchantData(icon: _avatarIcon, greeting: _greetingMsm);
                storeScreen.ActiveScreen(true);
            }
        }

        public void InteractionMark(bool value) => _interactionMark.SetActive(value);
    }
}
