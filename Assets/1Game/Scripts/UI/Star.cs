using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _1Game.Scripts.UI
{
    [RequireComponent(typeof(Animator))]
    public class Star : MonoBehaviour
    {
        private readonly int _hashNameCanChange = Animator.StringToHash("CanChange");
        private Animator _animator;
        private Image _image;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _image = GetComponent<Image>();
        }

        public void Fade(float alfa, float time)
        {
            Tween tween = _image.DOFade(alfa, time);
        }

        public void ChangeSize()
        {
            _animator.SetBool(_hashNameCanChange, true);
        }
    }
}