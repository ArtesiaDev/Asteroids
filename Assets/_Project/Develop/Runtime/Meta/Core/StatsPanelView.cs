using System.Text;
using TMPro;
using UnityEngine;

namespace Develop.Runtime.Meta.Core
{
    public class StatsPanelView : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private TextMeshProUGUI _coordinates;
        [SerializeField] private TextMeshProUGUI _rotation;
        [SerializeField] private TextMeshProUGUI _velocity;
        [SerializeField] private TextMeshProUGUI _laserAmmunition;
        [SerializeField] private TextMeshProUGUI _laserCooldown;

        private Rigidbody2D _rb;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private void Awake() => 
            _rb = _player.GetComponent<Rigidbody2D>();
        
        public void RerenderCoordinates()
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Coordinates: ({0:F0}, {1:F0})", _player.transform.position.x,
                _player.transform.position.y);
            _coordinates.text = _stringBuilder.ToString();
        }

        public void RerenderRotation()
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Rotation: {0:F0}", _player.transform.eulerAngles.z);
            _rotation.text = _stringBuilder.ToString();
        }

        public void RerenderVelocity()
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Velocity: {0:F1}", _rb.velocity.magnitude);
            _velocity.text = _stringBuilder.ToString();
        }

        public void RerenderLaserAmmunition(int currentLaserShots)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Laser Ammunition: {0:F0}", currentLaserShots);
            _laserAmmunition.text = _stringBuilder.ToString();
        }

        public void RerenderLaserCooldown(float cooldown)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Laser Cooldown: {0:F1}", cooldown);
            _laserCooldown.text = _stringBuilder.ToString();
        }
    }
}