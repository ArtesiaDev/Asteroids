using System;
using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Develop.Runtime.Meta
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _backButton;

        private IStateMachine _stateMachine;
        private GameObject _buttons;
        private GameObject _settingsPanel;

        [Inject]
        private void Construct(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _buttons = _startButton.transform.parent.gameObject;
            _settingsPanel = _backButton.transform.parent.gameObject;
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
            _settingsButton.onClick.AddListener(OpenSettings);
            _exitButton.onClick.AddListener(ExitGame);
            _backButton.onClick.AddListener(BackToMenu);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        private void StartGame() =>
            _stateMachine.Enter<LoadLevelState>();

        private void OpenSettings() =>
            SwitchPanel(false, true);

        private void ExitGame() =>
            Application.Quit();

        private void BackToMenu() =>
            SwitchPanel(true, false);

        private void SwitchPanel(bool first, bool second)
        {
            _buttons.SetActive(first);
            _settingsPanel.SetActive(second);
        }
    }
}