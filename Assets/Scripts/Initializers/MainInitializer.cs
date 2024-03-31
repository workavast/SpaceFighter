using UnityEngine;
using UnityEngine.SceneManagement;

namespace Initializers
{
    public class MainInitializer : MonoBehaviour
    {
        [Tooltip("Scene that will be load after initializations")]
        [SerializeField] private int sceneIndex;
        
        private int _inits;

        private readonly InitializerBase[] _initializers =
        {
            new YandexSdkInitializer(new InitializerBase[]
                {
                    new PlayerGlobalDataInitializer(new InitializerBase[]
                    {
                        new LocalizationInitializer(),
                        new PlatformTypeInitializer()
                    }),
                }
            )
        };
        
        private void Awake()
        {
            _inits = _initializers.Length;
            
            foreach (var initializer in _initializers)
                initializer.OnInit += UpdateInits;
        }

        private void Start()
        {
            foreach (var initializer in _initializers)
                initializer.Init();
        }

        private void UpdateInits()
        {
            _inits -= 1;

            if (_inits <= 0)
                SceneManager.LoadScene(sceneIndex);
        }
    }
}