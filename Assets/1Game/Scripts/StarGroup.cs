using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StarGroup : MonoBehaviour
{
    
    private readonly List<Image> _images= new List<Image>();

    private void Awake()
    {
        InitializeStars();
    }

    public void SetStars(int count)
    {
        InitializeStars();
        
        float alfa = 1f;
        
        for (int i = 0; i < count; i++)
        {
            _images[i].enabled = true;
            Fade(_images[i],alfa);
        }
    }

    private void InitializeStars()
    {
        float alfa = 0.3f;
        
        foreach (Image image in transform.GetComponentsInChildren<Image>())
        {
            Fade(image,alfa);
            _images.Add(image);
        }
    }

    private void Fade(Image image,float alfa)
    {
        Tween tween = image.DOFade(alfa, 1f);
    }
}
