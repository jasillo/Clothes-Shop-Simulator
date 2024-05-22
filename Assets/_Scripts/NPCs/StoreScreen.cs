using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.ComponentModel;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Singleton
    /// </summary>
    public class StoreScreen : MonoBehaviour
    {
        public static StoreScreen Instance;

        [SerializeField] private Image _avatarImg;
        [SerializeField] private TMP_Text _greetingTxt;
        [SerializeField] private Button _closeBtn;
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private Button _buyBtn, _sellBtn;
        [SerializeField] private TMP_Text _goldTxt;

        [Header("Products")]
        [SerializeField] private Transform[] _containers;
        [SerializeField] private TMP_Text _pagesTxt;
        [SerializeField] private Button _prevBtn, _nextBtn;

        private List<SOGameItem> _dataSource;
        private List<SOGameItem> _storeSource;
        private List<SOGameItem> _inventorySource;
        private int _pagesCount = 0;
        private int _currentPage = 0;
        private bool _isBuying = true;
        private int _gold = 0;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError("instance already exist");
            _mainPanel.SetActive(false);
        }

        private void Start()
        {
            _prevBtn.onClick.AddListener(Prev);
            _nextBtn.onClick.AddListener(Next);
            _closeBtn.onClick.AddListener(Close);
            _buyBtn.onClick.AddListener(SwitchBuy);
            _sellBtn.onClick.AddListener(SwitchSell);

            for (int i = 0; i < _containers.Length; i++)
            {
                var index = i;
                var button = _containers[i].GetChild(2).GetComponent<Button>();
                button.onClick.AddListener(() => OnButtonContainer(index));
            }
        }

        private void OnDestroy()
        {
            _prevBtn.onClick.RemoveListener(Prev);
            _nextBtn.onClick.RemoveListener(Next);
            _closeBtn.onClick.RemoveListener(Close);
            _buyBtn.onClick.RemoveListener(SwitchBuy);
            _sellBtn.onClick.RemoveListener(SwitchSell);

            for (int i = 0; i < _containers.Length; i++)
            {
                var button = _containers[i].GetChild(2).GetComponent<Button>();
                button.onClick.RemoveAllListeners();
            }
        }

        public List<SOGameItem> StoreData
        {
            set => _storeSource = new List<SOGameItem>(value);
        }

        public void SetMerchantData(Sprite icon, string greeting)
        {
            _greetingTxt.text = greeting;
            _avatarImg.sprite = icon;
        }

        private void RenderContainers(int page)
        {
            // trunk the current page
            if (_dataSource == null)
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
                if (dataIndex < _dataSource.Count)
                {
                    container.gameObject.SetActive(true);
                    var icon = container.GetChild(0).GetComponent<Image>();
                    var name = container.GetChild(1).GetComponent<TMP_Text>();
                    var price = container.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

                    icon.sprite = _dataSource[dataIndex].Icon(0);
                    name.text = _dataSource[dataIndex].ItemName;
                    price.text = _dataSource[dataIndex].Price.ToString();
                }
                else
                {
                    container.gameObject.SetActive(false);
                }
            }

            // text of the current page
            _pagesTxt.text = string.Format("Page {0}/{1}", _currentPage + 1, _pagesCount);
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
                _inventorySource = Inventory.Instance.Data;
                _isBuying = true;
                SwitchBuy();
                RenderContainers(page: 0);
                _gold = Inventory.Instance.Gold;
                _goldTxt.text = _gold.ToString();
            }
        }

        public void SwitchBuy()
        {
            // source data
            _isBuying = true;
            _dataSource = _storeSource;
            _pagesCount = Mathf.CeilToInt((float)_dataSource.Count / _containers.Length);

            // update button selector
            _buyBtn.transform.localScale = Vector3.one * 1.25f;
            _sellBtn.transform.localScale = Vector3.one;
            _buyBtn.GetComponent<Image>().color = Color.white;
            _sellBtn.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);

            RenderContainers(page: 0);
        }

        public void SwitchSell()
        {
            // source data
            _isBuying = false;
            _dataSource = _inventorySource;
            _pagesCount = Mathf.CeilToInt((float)_dataSource.Count / _containers.Length);

            // Update button selector
            _buyBtn.transform.localScale = Vector3.one;
            _sellBtn.transform.localScale = Vector3.one * 1.25f;
            _buyBtn.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1);
            _sellBtn.GetComponent<Image>().color = Color.white;

            RenderContainers(page: 0);
        }

        public void OnButtonContainer(int index)
        {
            var itemIndex = (_containers.Length * _currentPage) + index;
            if (_isBuying)
            {
                var item = _dataSource[itemIndex];

                // update gold
                if (_gold < item.Price) return;
                _gold -= item.Price;
                _goldTxt.text = _gold.ToString();

                // add to internal inventory list
                _inventorySource.Add(item);
            }
            else
            {
                var item = _dataSource[itemIndex];

                // update gold
                _gold += item.Price;
                _goldTxt.text = _gold.ToString();

                // remove from internal inventory list and update render page
                _dataSource.RemoveAt(itemIndex);
                _pagesCount = Mathf.CeilToInt((float)_dataSource.Count / _containers.Length);
                if (_currentPage >= _pagesCount) _currentPage = _pagesCount - 1;
                RenderContainers(_currentPage);
            }
        }

        private void Next() => RenderContainers(_currentPage + 1);

        private void Prev() => RenderContainers(_currentPage - 1);
        private void Close()
        {
            // save data in the inventory
            Inventory.Instance.Data = new (_inventorySource);
            ActiveScreen(false);
        }
    }
}
