using EventBusEvents;
using EventBusExtension;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Audio
{
    public class CustomButton : Button
    {
        [Inject] private EventBus _eventBus;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            _eventBus.Invoke(new UiButtonPressed());
        }
    }
}