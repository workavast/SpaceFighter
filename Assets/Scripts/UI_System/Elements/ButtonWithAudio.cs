using EventBusEvents;
using EventBusExtension;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace UI_System.Elements
{
    public class ButtonWithAudio : Button
    {
        [Inject] private readonly EventBus _eventBus;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            _eventBus.Invoke(new UiButtonPressed());
        }
    }
}