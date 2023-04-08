using _1Game.Scripts.Core.Data;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Authorization : MonoBehaviour
{
    [SerializeField] private GameInitializer _initializer;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
#if !UNITY_EDITOR
        _button.onClick.AddListener(Execute);
#endif
    }

    private void OnDisable()
    {
#if !UNITY_EDITOR
        _button.onClick.RemoveListener(Execute);
#endif
    }

    private void Execute()
    {
        PlayerAccount.Authorize(Disable);
    }

    private void Disable()
    {
        _initializer.LoadCloudData();
        gameObject.SetActive(false);
    }
}