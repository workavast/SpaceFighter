using UnityEngine;

namespace Control
{
    public class MobileInput : IInput
    {
        private readonly MobileInputDetector _inputDetector;
        
        public MobileInput(MobileInputDetector inputDetector, Transform playerSpaceshipTransform)
        {
            _inputDetector = inputDetector;
            _inputDetector.SetPlayerTransform(playerSpaceshipTransform);
        }
        
        public Vector3 Position() => _inputDetector.Position;
    }
}