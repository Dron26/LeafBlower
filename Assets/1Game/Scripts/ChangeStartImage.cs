using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStartImage : MonoBehaviour
{
    [SerializeField] private Image _screenDim;
    [SerializeField] private Image _logoNameImage;
    [SerializeField] private ScenePanel _panel;
    [SerializeField] private Button _button;

     private LogoPanel _logoPanel;

    private Color _colorScreen;
    private Color _colorLogo;
    private float _waitTime;

    private void Awake()
    {
        _logoPanel = GetComponentInChildren<LogoPanel>();
        _logoPanel.gameObject.SetActive(true);
        _panel.gameObject.SetActive(false);
        _colorScreen = _screenDim.color;
        _colorLogo = _logoNameImage.color;

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
        while (_colorScreen.a < 1)
        {
            _waitTime = Time.fixedDeltaTime;
            yield return new WaitForSeconds(_waitTime);
            _colorScreen.a += _waitTime;
            _screenDim.color = _colorScreen;
            _colorLogo.a += _waitTime;
            _logoNameImage.color = _colorLogo;
        }

        _logoPanel.gameObject.SetActive(false);
        StopCoroutine(ChangeColorExitScene(index));
    }

    private IEnumerator ChangeColorEnterScene()
    {
        while (_colorScreen.a > 0)
        {
            _waitTime = Time.fixedDeltaTime;
            yield return new WaitForSeconds(_waitTime);
            _colorScreen.a -= _waitTime;
            _colorLogo.a -= _waitTime;
            _screenDim.color = _colorScreen;
            _logoNameImage.color = _colorLogo;
        }

        _panel.gameObject.SetActive(true);      

        StopCoroutine(ChangeColorEnterScene());
    }

    public void StartChange()
    {
        _button.gameObject.SetActive(false);
        StartCoroutine(ChangeColorEnterScene());
    }
}
