using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    public class Star : MonoBehaviour
    {
        private Image _image;
        


        public void Fade(float alfa)
        {
            _image = GetComponent<Image>();
            var color = _image.color;
            color.a = alfa;
            _image.color=color;
        }

    }
}