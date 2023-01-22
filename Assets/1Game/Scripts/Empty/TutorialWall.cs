using _1Game.Scripts.Item;
using _1Game.Scripts.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace _1Game.Scripts.Empty
{
    public class TutorialWall : MonoBehaviour
    {
        [SerializeField]private TutorialAlarmPanel _panel;
        [SerializeField]private ChangerPanel _changerPanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out LeaveBlowerStandart standart))
            {
                _panel.gameObject.SetActive(true);
                _changerPanel.PauseGame();
            }
        }
    }
}
