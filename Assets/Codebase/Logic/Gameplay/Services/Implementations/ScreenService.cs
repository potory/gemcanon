using UnityEngine;

namespace Codebase.Logic.Gameplay.Services.Implementations
{
    public class ScreenService : IScreenService
    {
        private readonly Camera _camera;
        private readonly Vector2 _screenWorldSize;

        public ScreenService()
        {
            _camera = Camera.main;
            _screenWorldSize = CalculateScreenWorldSize();
        }

        public Vector2 GetScreenWorldSize() => 
            _screenWorldSize;

        private Vector2 CalculateScreenWorldSize()
        {
            var cameraZ = _camera.gameObject.transform.position.z;
            
            var startPosition = new Vector3(0, 0)
            {
                z = -cameraZ
            };

            var endPosition = new Vector3(Screen.width, Screen.height)
            {
                z = -cameraZ
            };

            var startWorldPosition = _camera.ScreenToWorldPoint(startPosition);
            var endWorldPosition = _camera.ScreenToWorldPoint(endPosition);

            var diff = endWorldPosition - startWorldPosition;
            
            return diff;
        }
    }
}