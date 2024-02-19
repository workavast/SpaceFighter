using UnityEngine;

namespace Control
{
    public class DesktopInput : IInput
    {
        private readonly Camera _camera;

        public DesktopInput(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 Position() => _camera.ScreenToWorldPoint(Input.mousePosition) - _camera.transform.position;
    }
}