using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using Develop.Runtime.Meta.EventSignals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Develop.Runtime.Meta
{
    public class EndPanel : MonoBehaviour
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _statsPanel;
        [SerializeField] private GameObject _endPanelView;
        [SerializeField] private TextMeshProUGUI _finalScore;
        
        private IScoreCountable _scorePanel;
        private IStateMachine _stateMachine;
        private IPlayerSignals _playerSignals;

        [Inject]
        private void Construct(IStateMachine stateMachine, IScoreCountable scorePanel, IPlayerSignals playerSignals)
        {
            _stateMachine = stateMachine;
            _scorePanel = scorePanel;
            _playerSignals = playerSignals;
        }

        private void OnEnable()
        {
            _retryButton.onClick.AddListener(ReloadGame);
            _menuButton.onClick.AddListener(ToMenu);
            _playerSignals.PlayerDied += EndGame;
            _playerSignals.PlayerDied += ShowFinalScore;
        }

        private void OnDisable()
        {
            _retryButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
            _playerSignals.PlayerDied -= EndGame;
            _playerSignals.PlayerDied -= ShowFinalScore;
        }

        private void EndGame()
        {
            Time.timeScale = 0;
            _statsPanel.SetActive(false);
            _endPanelView.SetActive(true);
        }

        private void ToMenu()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<MenuState>();
        }

        private void ReloadGame()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<LoadLevelState>();
        }

        private void ShowFinalScore() =>
            _finalScore.text = $"Score: {_scorePanel.ScoreCount}";
    }
}