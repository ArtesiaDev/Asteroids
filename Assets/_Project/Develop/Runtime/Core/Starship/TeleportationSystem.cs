using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class TeleportationSystem : IFixedTickable
    {
        private readonly Transform _transform;
        private readonly Camera _camera;

        public TeleportationSystem(Transform transform)
        {
            _transform = transform;
            _camera = Camera.main;
        }

        public void FixedTick() =>
            Teleport();

        private void Teleport()
        {
            Vector3 pointPos = _camera.WorldToViewportPoint(_transform.position);
            _transform.position = GetNewPos(pointPos, _transform.position);
        }

        private Vector3 GetNewPos(Vector3 pointPos, Vector3 newPos)
        {
            if (pointPos.x > 1)
                newPos.x = ConvertToWorldPoint(0, pointPos.y).x;

            if (pointPos.x < 0)
                newPos.x = ConvertToWorldPoint(1, pointPos.y).x;

            if (pointPos.y > 1)
                newPos.y = ConvertToWorldPoint(pointPos.x, 0).y;

            if (pointPos.y < 0)
                newPos.y = ConvertToWorldPoint(pointPos.x, 1).y;
            return newPos;
        }

        private Vector3 ConvertToWorldPoint(float pointPosX, float pointPosY) =>
            _camera.ViewportToWorldPoint(new Vector3(pointPosX, pointPosY));
    }
}