using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangrColorWorkPlace : MonoBehaviour
{
    [SerializeField] private WorkPlacesSwitcher _workPlacesSwitcher;

    List<Image> _images;

    private void Awake()
    {
        _images = new List<Image>();

        foreach (Transform child in transform)
        {
            _images.Add(child.GetComponent<Image>());
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
}
