using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorkPlaceImageChanger : MonoBehaviour
{ 
    [SerializeField] private List<Image> _images;

    public UnityAction AppearImageEnd;
    public UnityAction FadeImageEnd;

    public void ChangeImage(float volume)
    {
        if (volume>0)
        {
            AppearImageEnd?.Invoke();
        }
        else
        {
            FadeImageEnd?.Invoke();
        }
        float duration = 1f;

        for (int i = 0; i < _images.Count; i++)
        {
            Tween tween = _images[i].DOFade(volume, duration);
        }
    }
}
