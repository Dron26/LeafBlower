using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStartImage : MonoBehaviour
{
    [SerializeField] private Image _screenDim;
    [SerializeField] private ScenePanel _panel;
    [SerializeField] private Button _button;

    private Color _color;
    private float _waitTime;

    private void Awake()
    {
        _screenDim.gameObject.SetActive(true);
        _color = _screenDim.color;
    }

    private void Start()
    {
        _panel.gameObject.SetActive(false);
    }

    public void ChangeScene(int index)
    {
        StartCoroutine(ChangeColorExitScene(index));
    }

    public void Exit()
    {
        Application.Quit();
    }

    private IEnumerator ChangeColorExitScene(int index)
    {
        _panel.gameObject.SetActive(false);
        _screenDim.gameObject.SetActive(true);

        while (_color.a < 1)
        {
            _waitTime = Time.fixedDeltaTime;
            yield return new WaitForSeconds(_waitTime);
            _color.a += _waitTime;
            _screenDim.color = _color;
        }

        StopCoroutine(ChangeColorExitScene(index));
    }

    private IEnumerator ChangeColorEnterScene()
    {
        while (_color.a > 0)
        {
            _waitTime = Time.fixedDeltaTime;
            yield return new WaitForSeconds(_waitTime);
            _color.a -= _waitTime;
            _screenDim.color = _color;
        }

        _screenDim.gameObject.SetActive(false);
        _panel.gameObject.SetActive(true);

        StopCoroutine(ChangeColorEnterScene());
    }

    public void StartChange()
    {
        _button.gameObject.SetActive(false);
        StartCoroutine(ChangeColorEnterScene());
    }
}
