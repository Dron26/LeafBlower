using UnityEngine;
using UnityEngine.EventSystems;

namespace _1Game.Scripts
{
    public class JoystickCtrl : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public RectTransform background;
        public RectTransform handle;

        public float handleLimit = 1f;
        protected Vector2 inputVector = Vector2.zero;

        public float Horizontal { get { return inputVector.x; } }
        public float Vertical { get { return inputVector.y; } }
        public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

        public virtual void OnDrag(PointerEventData eventData)
        {}

        public virtual void OnPointerDown(PointerEventData eventData)
        {}

        public virtual void OnPointerUp(PointerEventData eventData)
        {}
    }
}
