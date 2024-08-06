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
        [SerializeField] private GameObject _statsPanel;
        [SerializeField] private GameObject _endPanelView;
        [SerializeField] private TextMeshProUGUI _finalScore;

        private EndPanelViewPresenter _presenter;

        [Inject]
        private void Construct(EndPanelViewPresenter presenter) =>
            _presenter = presenter;

        public void RenderFinalScore(int newValue) =>
            _finalScore.text = $"Score: {newValue}";

        public void SwitchPanelsRendering()
        {
            _statsPanel.SetActive(false);
            _endPanelView.SetActive(true);
        }

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(_presenter.ReloadGame);
            _menuButton.onClick.AddListener(_presenter.ToMenu);
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
        }
    }
}