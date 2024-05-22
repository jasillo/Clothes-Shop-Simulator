using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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

        [Header("Products")]
        [SerializeField] private Transform[] _containers;
        [SerializeField] private TMP_Text _pagesTxt;
        [SerializeField] private Button _prevBtn, _nextBtn;



        private SOGameItem[] _dataSource;
        private int _pagesCount = 0;
        private int _currentPage = 0;

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
        }

        private void OnDestroy()
        {
            _prevBtn.onClick.RemoveListener(Prev);
            _nextBtn.onClick.RemoveListener(Next);
            _closeBtn.onClick.RemoveListener(Close);
        }

        public SOGameItem[] Data
        {
            set
            {
                _dataSource = value;
                _pagesCount = Mathf.CeilToInt((float)_dataSource.Length / _containers.Length); ;
                _currentPage = 0;
                Debug.Log(_pagesCount);
            }
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
            _currentPage = Mathf.Clamp(page, 0, _pagesCount - 1);

            // start index for the current page
            var startIndex = _containers.Length * _currentPage;

            for (int i = 0; i < _containers.Length; i++)
            {
                var container = _containers[i];
                var dataIndex = startIndex + i;

                // exits a product to show in this container
                if (dataIndex < _dataSource.Length)
                {
                    container.gameObject.SetActive(true);
                    var icon = container.GetChild(0).GetComponent<Image>();
                    var name = container.GetChild(1).GetComponent<TMP_Text>();
                    var price = container.GetChild(2).GetChild(0).GetComponent<TMP_Text>();

                    icon.sprite = _dataSource[dataIndex].Icons[0];
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

        public void ActiveScreen(bool value)
        {
            _mainPanel.SetActive(value);
            GameManager.State = value ? TState.Dialog : TState.FreeRoam;

            if (value)
            {
                _currentPage = 0;
                RenderContainers(page: 0);
            }
        }

        private void Next() => RenderContainers(_currentPage + 1);

        private void Prev() => RenderContainers(_currentPage - 1);
        private void Close() => ActiveScreen(false);
    }
}
