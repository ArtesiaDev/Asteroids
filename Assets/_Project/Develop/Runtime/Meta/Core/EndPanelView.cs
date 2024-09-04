using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class EndPanelView : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _rewardedButton;
        [SerializeField] private GameObject _statsPanel;
        [SerializeField] private GameObject _endPanelView;
        [SerializeField] private TextMeshProUGUI _finalScore;
        [SerializeField] private TextMeshProUGUI _adsBanMessage;

        private EndPanelPresenter _endPanelPresenter;
        private CoreBackendPresenter _backendPresenter;

        [Inject]
        private void Construct(EndPanelPresenter endPanelPresenter, CoreBackendPresenter backendPresenter)
        {
            _endPanelPresenter = endPanelPresenter;
            _backendPresenter = backendPresenter;
        }

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(_endPanelPresenter.ReloadGame);
            _retryButton.onClick.AddListener(_backendPresenter.ReloadGameBackend);
            _menuButton.onClick.AddListener(_endPanelPresenter.ToMenu);
            _menuButton.onClick.AddListener(_backendPresenter.ToMenuBackend);
            _rewardedButton.onClick.AddListener(_backendPresenter.StartRewardedVideo);
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
            _rewardedButton.onClick.RemoveAllListeners();
        }

        public void RenderFinalScore(int newValue) =>
            _finalScore.text = $"Score: {newValue}";

        public void SwitchPanelsRendering( bool statsPanel, bool endPanel)
        {
            _statsPanel.SetActive(statsPanel);
            _endPanelView.SetActive(endPanel);
        }

        public void ShowAdsBanMessage()
        {
            _rewardedButton.gameObject.SetActive(false);
            _adsBanMessage.gameObject.SetActive(true);
        }
    }
}