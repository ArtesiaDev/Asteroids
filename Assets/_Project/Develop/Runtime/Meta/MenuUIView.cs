using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Develop.Runtime.Meta
{
    public class MenuUIView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _backButton;
        
        private GameObject _buttons;
        private GameObject _settingsPanel;
        private MenuUIPresenter _presenter;

        [Inject]
        private void Construct(MenuUIPresenter presenter)
        {
            _presenter = presenter;
            _buttons = _startButton.transform.parent.gameObject;
            _settingsPanel = _backButton.transform.parent.gameObject;
        }

        public void SwitchPanelsRendering(bool buttons, bool settingsPanel)
        {
            _buttons.SetActive(buttons);
            _settingsPanel.SetActive(settingsPanel);
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(_presenter.StartGame);
            _settingsButton.onClick.AddListener(_presenter.OpenSettings);
            _exitButton.onClick.AddListener(_presenter.ExitGame);
            _backButton.onClick.AddListener(_presenter.BackToMenu);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }
    }
}