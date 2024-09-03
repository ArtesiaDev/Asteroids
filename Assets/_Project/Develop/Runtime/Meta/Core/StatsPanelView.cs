using System.Text;
using TMPro;
using UnityEngine;

namespace Develop.Runtime.Meta.Core
{
    public class StatsPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coordinates;
        [SerializeField] private TextMeshProUGUI _rotation;
        [SerializeField] private TextMeshProUGUI _velocity;
        [SerializeField] private TextMeshProUGUI _laserAmmunition;
        [SerializeField] private TextMeshProUGUI _laserCooldown;
        
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void RerenderCoordinates(float posX, float posY)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Coordinates: ({0:F0}, {1:F0})", posX, posY);
            _coordinates.text = _stringBuilder.ToString();
        }

        public void RerenderRotation(float rotation)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Rotation: {0:F0}", rotation);
            _rotation.text = _stringBuilder.ToString();
        }

        public void RerenderVelocity(float magnitude)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("Velocity: {0:F1}", magnitude);
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