using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorkPlaceImageChanger : MonoBehaviour
{
     private InsideController _insideController;

    [SerializeField] private List<Image> _images;

    public UnityAction AppearImageEnd;
    public UnityAction FadeImageEnd;


    private void Awake()
    {
        _insideController = GetComponentInChildren<InsideController>();
    }

    private void OnEnable()
    {
        _insideController.CharacterInside += OnCharacterInside;
    }

    private void OnDisable()
    {
        _insideController.CharacterInside -= OnCharacterInside;
    }

    private void OnCharacterInside(bool isInside)
    {
        float volume;

        if (isInside==true)
        {
            volume = 255f;

            ChangeImage(volume);

            AppearImageEnd?.Invoke();
        }
        else
        {
            volume = 0;
          
            ChangeImage(volume);

            FadeImageEnd?.Invoke();
        }
    }


    private void ChangeImage(float volume)
    {
        float duration = 1f;

        for (int i = 0; i < _images.Count; i++)
        {
            Tween tween = _images[i].DOFade(volume, duration);
        }
    }
}
