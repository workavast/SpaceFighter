using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI_System.Elements
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private VolumeType volumeType;

        [Inject] private AudioManager _audioManager;
        
        private Slider _slider;
        private event Action<float> OnValueChange;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(ChangeValue);
        }

        private void Start()
        {
            switch (volumeType)
            {
                case VolumeType.Master:
                    _slider.value = _audioManager.Volume.MasterVolume;
                    OnValueChange += SetMasterVolume;
                    break;
                case VolumeType.Ost:
                    _slider.value = _audioManager.Volume.OstVolume;
                    OnValueChange += SetOstVolume;
                    break;
                case VolumeType.Effects:
                    _slider.value = _audioManager.Volume.EffectsVolume;
                    OnValueChange += SetEffectsVolume;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ChangeValue(float newValue) => OnValueChange?.Invoke(newValue);
        
        private void SetMasterVolume(float newVolume) => _audioManager.Volume.SetMasterVolume(newVolume);

        private void SetEffectsVolume(float newVolume) => _audioManager.Volume.SetEffectsVolume(newVolume);
        
        private void SetOstVolume(float newVolume) => _audioManager.Volume.SetOstVolume(newVolume);

        private void OnDestroy() => _slider.onValueChanged.RemoveListener(ChangeValue);

        private enum VolumeType
        {
            Master = 0,
            Ost = 10,
            Effects = 20
        }
    }
}