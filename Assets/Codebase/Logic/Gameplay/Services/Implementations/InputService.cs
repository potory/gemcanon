using UnityEngine;

namespace Codebase.Logic.Gameplay.Services.Implementations
{
    public class InputService : IInputService
    {
        private readonly Camera _camera;

        public InputService()
        {
            _camera = Camera.main;
        }
        public Vector3 GetInputWorldPosition()
        {
            var screenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y)
            {
                z = -_camera.gameObject.transform.position.z
            };

            return _camera.ScreenToWorldPoint(screenPoint);
        }
    }
}