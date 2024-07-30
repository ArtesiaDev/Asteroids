using System.Collections;
using System.Text;
using Develop.Runtime.Meta.EventSignals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _player;

        private Rigidbody2D _rb;
        private TextMeshProUGUI _coordinates;
        private TextMeshProUGUI _rotation;
        private TextMeshProUGUI _velocity;
        private TextMeshProUGUI _laserAmmunition;
        private TextMeshProUGUI _laserCooldown;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private IPlayerSignals _playerSignals;
        private ILaserSignals _laserSignals;

        [Inject]
        private void Construct(IPlayerSignals playerSignals, ILaserSignals laserSignals)
        {
            _playerSignals = playerSignals;
            _laserSignals = laserSignals;
        }

        private void Awake()
        {
            _rb = _player.GetComponent<Rigidbody2D>();

            TextMeshProUGUI[] childComponents = GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI component in childComponents)
                switch (component.gameObject.name)
                {
                    case MetaConstants.COORDINATES:
                        _coordinates = component;
                        break;
                    case MetaConstants.ROTATION:
                        _rotation = component;
                        break;
                    case MetaConstants.VELOCITY:
                        _velocity = component;
                        break;
                    case MetaConstants.AMMUNITION:
                        _laserAmmunition = component;
                        break;
                    case MetaConstants.COOLDOWN:
                        _laserCooldown = component;
                        break;
                }
        }

        private void OnEnable()
        {
            _playerSignals.PlayerMoved += OnPlayerMoved;
            _playerSignals.PlayerSteered += OnPlayerSignals;
            _laserSignals.LaserAmmunitionChanged += OnLaserAmmunitionChanged;
            _laserSignals.LaserCooldownChanged += OnLaserCooldownChanged;
        }

        private void OnDisable()
        {
            _playerSignals.PlayerMoved -= OnPlayerMoved;
            _playerSignals.PlayerSteered -= OnPlayerSignals;
            _laserSignals.LaserAmmunitionChanged -= OnLaserAmmunitionChanged;
            _laserSignals.LaserCooldownChanged -= OnLaserCooldownChanged;
        }

        private void OnPlayerMoved()
        {
            ChangeCoordinates();
            ChangeVelocity();
        }

        private void OnPlayerSignals()
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Rotation: {0:F0}", _player.transform.eulerAngles.z);
            _rotation.text = _stringBuilder.ToString();
        }

        private void OnLaserAmmunitionChanged(int currentLaserShots)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("LaserAmmunition: {0}", currentLaserShots);
            _laserAmmunition.text = _stringBuilder.ToString();
        }

        private void OnLaserCooldownChanged(float cooldown) =>
            StartCoroutine(CooldownTimer(cooldown));

        private IEnumerator CooldownTimer(float time)
        {
            float step = 0.1f;
            float delay = 0.003f;

            for (float i = time; i >= 0; i -= step)
            {
                _stringBuilder.Clear();
                _stringBuilder.AppendFormat("LaserAmmunition: {0:F1}", i);
                _laserCooldown.text = _stringBuilder.ToString();
                yield return new WaitForSeconds(step - delay);
            }
        }

        private void ChangeCoordinates()
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Coordinates: ({0:F0}, {1:F0})", _player.transform.position.x,
                _player.transform.position.y);
            _coordinates.text = _stringBuilder.ToString();
        }

        private void ChangeVelocity()
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Velocity: {0:F1}", _rb.velocity.magnitude);
            _velocity.text = _stringBuilder.ToString();
        }
    }
}