using UnityEngine;


namespace Service
{
public class FuelPlace : MonoBehaviour
{
    private WorkPlaceImageChanger _workPlaceImage;
    private FuelTank _tank;

    private void Awake()
    {
        //_workPlaceImage = GetComponentInParent<WorkPlaceImageChanger>();
        //_tank = GetComponentInChildren<FuelTank>();
    }

    private void OnEnable()
    {
        //_workPlaceImage.AppearImageEnd += OnAppearImageEnd;
        //_workPlaceImage.FadeImageEnd += OnFadeImageEnd;
    }

    private void OnDisable()
    {
        //_workPlaceImage.AppearImageEnd -= OnAppearImageEnd;
        //_workPlaceImage.FadeImageEnd -= OnFadeImageEnd;
    }

    //private void OnAppearImageEnd()
    //{
    //    _tank.gameObject.SetActive(true);
    //}

    //private void OnFadeImageEnd()
    //{
    //    _tank.gameObject.SetActive(false);
    //}
}
}