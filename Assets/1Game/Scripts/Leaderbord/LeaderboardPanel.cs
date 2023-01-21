//
// using TMPro;
// using UnityEngine;
//
// namespace _1Game.Scripts.UI
// {
//     public class LeaderboardPanel : MonoBehaviour
//     {
//         [SerializeField] private TMP_Text _rank;
//         [SerializeField] private TMP_Text _score;
//         [SerializeField] private LeaderBoard _leaderBoard;
//
//         private void OnEnable()
//         {
//             UpdateData();
//         }
//
//         private void UpdateData()
//         {
//             _leaderBoard.UpdateData();
//             _rank.text = _leaderBoard.Rank.ToString();
//             _score.text = _leaderBoard.Score.ToString();
//         }
//     }
//}