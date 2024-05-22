using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [SerializeField] private int _initialGold = 1000;
        [SerializeField] private int _maxItems = 20;

        [SerializeField] private AvatarColletion _avatarPreview;
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Button _openInvBtn;
        [SerializeField] private Button _closeBtn;

        [Header("Equips Buttons")]
        [SerializeField] private TMP_Text _equipNameTxt;
        [SerializeField] private Button _helmetBtn;
        [SerializeField] private Button _armorBtn;
        [SerializeField] private Button _shoulderBtn;
        [SerializeField] private Button _gloveBtn;
        [SerializeField] private Button _bootsBtn;
        [SerializeField] private Button _weaponBtn;

        [Header("Products")]
        [SerializeField] private Transform[] _containers;
        [SerializeField] private TMP_Text _pagesTxt;
        [SerializeField] private Button _prevBtn, _nextBtn;

        private int _currentGold;
        private List<SOGameItem> _dataSource = new();
        private List<SOGameItem> _equipDataSource = new();
        private int _pagesCount = 0;
        private int _currentPage = 0;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError("instance already exist");

            _currentGold = _initialGold;
            _mainPanel.SetActive(false);
        }

        private void Start()
        {
            _prevBtn.onClick.AddListener(Prev);
            _nextBtn.onClick.AddListener(Next);
            _openInvBtn.onClick.AddListener(() => ActiveScreen(true));
            _closeBtn.onClick.AddListener(Close);

            _helmetBtn.onClick.AddListener(() => SwitchEquipType(TEquip.Helmet));
            _armorBtn.onClick.AddListener(() => SwitchEquipType(TEquip.Armor));
            _shoulderBtn.onClick.AddListener(() => SwitchEquipType(TEquip.Shoulder));
            _gloveBtn.onClick.AddListener(() => SwitchEquipType(TEquip.Glove));
            _bootsBtn.onClick.AddListener(() => SwitchEquipType(TEquip.Boots));
            _weaponBtn.onClick.AddListener(() => SwitchEquipType(TEquip.Weapon));

            for (int i = 0; i < _containers.Length; i++)
            {
                var index = i;
                var button = _containers[i].GetComponent<Button>();
                button.onClick.AddListener(() => OnButtonContainer(index));
            }
        }

        private void OnDestroy()
        {
            _prevBtn.onClick.RemoveAllListeners();
            _nextBtn.onClick.RemoveAllListeners();
            _openInvBtn.onClick.RemoveAllListeners();
            _closeBtn.onClick.RemoveAllListeners();

            _helmetBtn.onClick.RemoveAllListeners();
            _armorBtn.onClick.RemoveAllListeners();
            _shoulderBtn.onClick.RemoveAllListeners();
            _gloveBtn.onClick.RemoveAllListeners();
            _bootsBtn.onClick.RemoveAllListeners();
            _weaponBtn.onClick.RemoveAllListeners();

            for (int i = 0; i < _containers.Length; i++)
            {
                var button = _containers[i].GetComponent<Button>();
                button.onClick.RemoveAllListeners();
            }
        }


        public List<SOGameItem> Data
        {
            get => new (_dataSource);
            set => _dataSource = value;
        }

        /// <summary>
        /// activate the menu screen
        /// </summary>
        public void ActiveScreen(bool value)
        {
            _mainPanel.SetActive(value);
            GameManager.State = value ? TState.Dialog : TState.FreeRoam;

            if (value)
            {
                SwitchEquipType(TEquip.Helmet);
            }
        }

        private void RenderContainers(int page)
        {
            // trunk the current page
            if (_equipDataSource == null)
            {
                Debug.LogError("trying to render an empty list : products");
                return;
            }
            if (_pagesCount == 0) _currentPage = 0;
            else _currentPage = Mathf.Clamp(page, 0, _pagesCount - 1);

            // start index for the current page
            var startIndex = _containers.Length * _currentPage;

            for (int i = 0; i < _containers.Length; i++)
            {
                var container = _containers[i];
                var dataIndex = startIndex + i;

                // exits a product to show in this container
                if (dataIndex < _equipDataSource.Count)
                {
                    container.gameObject.SetActive(true);
                    var icon = container.GetComponent<Image>();
                    icon.sprite = _equipDataSource[dataIndex].Icon(0);
                }
                else
                {
                    container.gameObject.SetActive(false);
                }
            }

            // text of the current page
            _pagesTxt.text = string.Format("Page {0}/{1}", _currentPage + 1, _pagesCount);
        }

        private void SwitchEquipType(TEquip type)
        {
            _equipDataSource.Clear();
            for (int i = 0; i < _dataSource.Count; i++)
            {
                var equipType = (TEquip)_dataSource[i].Code;
                if (equipType == type)
                    _equipDataSource.Add(_dataSource[i]);
            }

            _equipNameTxt.text = type.ToString();
            _pagesCount = Mathf.CeilToInt((float)_equipDataSource.Count / _containers.Length);
            RenderContainers(page: 0);
        }

        public void OnButtonContainer(int index)
        {
            var itemIndex = (_containers.Length * _currentPage) + index;
            var item = _equipDataSource[itemIndex];
            var equipType = (TEquip)item.Code;

            switch (equipType)
            {
                case TEquip.Helmet:
                    _avatarPreview.SetHelmet(item);
                    break;
                case TEquip.Armor:
                    _avatarPreview.SetBodyArmor(item);
                    break;
                case TEquip.Shoulder:
                    _avatarPreview.SetShoulder(item);
                    break;
                case TEquip.Glove:
                    _avatarPreview.SetGlove(item);
                    break;
                case TEquip.Boots:
                    _avatarPreview.SetBoots(item);
                    break;
                case TEquip.Weapon:
                    _avatarPreview.SetWeapon(item);
                    break;
            }
        }

        private void Next() => RenderContainers(_currentPage + 1);

        private void Prev() => RenderContainers(_currentPage - 1);
        private void Close() => ActiveScreen(false);
        
        public int Gold
        {
            get => _currentGold;
            set => _currentGold = value;
        }
    }
}
