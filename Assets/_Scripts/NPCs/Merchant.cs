using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// General class for NPC
    /// </summary>
    public class Merchant : MonoBehaviour
    {
        [SerializeField] private GameObject _interactionMark;
        [SerializeField] private GameObject _panel;

        private void Awake()
        {
            _interactionMark.SetActive(false);
            _panel.SetActive(false);
        }

        public void ShowPanel()
        {
            
        }

        private void HidePanel()
        {
            _panel.SetActive(false);
            GameManager.State = TState.Dialog;
        }

        public void InteractMark(bool value) => _interactionMark.SetActive(value);
    }
}
