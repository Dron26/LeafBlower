using System;
using _1Game.Scripts.Particle;
using _1Game.Scripts.UI;
using UnityEngine;

namespace _1Game.Scripts.Core
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private DirectionArrow _directionArrow; 
        [SerializeField] private TutorialPanel _tutorialPanel; 
        [SerializeField] private Character _character;
        [SerializeField] private Cart _cart;
        [SerializeField] private Canvas _canvas; 
        [SerializeField] private WorkPlaceTutorialPoint _workPlaceTutorialPoint;    
        [SerializeField] private WorkplaceTutorialSecondPoint _workPlaceTutorialSecondPoint;    
        [SerializeField] private TutorialWall _tutorialWall;    
       
        private StageController _stageController;
        private StageData _stageData;
        private CharacterTrashBagPicker _characterTrashBagPicker;
        private CharacterTutorial _characterTutorial;

        private bool _isTutorialCompleted;
        private void Awake()
        {
            _stageController=GetComponent<StageController>();
            _stageData=GetComponent<StageData>();
            _characterTrashBagPicker = _character.gameObject.GetComponent<CharacterTrashBagPicker>();
            _tutorialPanel.gameObject.SetActive(false);
            _characterTutorial=GetComponentInChildren<CharacterTutorial>();
        }

        private void Start()
        {
            _isTutorialCompleted = _stageData.IsTutorialCompleted;
            _tutorialWall.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _stageController.SetStage+=OnSetStage;
            _characterTrashBagPicker.TakeTrashBag += OnTakeTrashBag;
            _characterTrashBagPicker.TakeMaxQuantityTrashBag += OnTakeMax;
            _stageController.CatchAllParticle+=OnCleanWorkPlace;
            _characterTutorial.ReachedPoint += OnReachedWorkPlace;
            _characterTutorial.ReachedCart += OnReachedCart;
        }

        private void OnDisable()
        {
            _stageController.SetStage-=OnSetStage;
            _characterTutorial.ReachedPoint -= OnReachedWorkPlace;
            _characterTutorial.ReachedCart -= OnReachedCart;
            _characterTrashBagPicker.TakeMaxQuantityTrashBag -= OnTakeMax;
            _stageController.CatchAllParticle-=OnCleanWorkPlace;
        }

        private void OnSetStage(GameObject target)
        {
            if (_isTutorialCompleted==false)
            {
                InitializeTutorial();
            }
        }

        private void OnTakeTrashBag()
        {
            _tutorialPanel.OnTakeTrashBag();
            _characterTrashBagPicker.TakeMaxQuantityTrashBag -= OnTakeMax;
            _directionArrow.SetLookPoint(_cart.transform.position);
        }
        
        private void OnReachedCart()
        {
            _tutorialPanel.OnReachedCart();
            _directionArrow.SetLookPoint(_workPlaceTutorialPoint.transform.position);
        }
        
        private void OnReachedWorkPlace()
        {
            _tutorialPanel.OnReachedWorkPlace();
        }

        private void OnTakeMax()
        {
            _tutorialPanel.OnClearWorkPlace();
            _directionArrow.SetLookPoint(_cart.transform.position);
        }
        
        private void OnCleanWorkPlace()
        {
            
            _tutorialWall.gameObject.SetActive(false);
            _directionArrow.SetLookPoint(_workPlaceTutorialSecondPoint.transform.position);
        }
        
        private void OnReachedSecondWorkPlace()
        {
            _directionArrow.SetBoolIsLook();
        }

        private void InitializeTutorial()
        {
            _directionArrow.SetLookPoint(_workPlaceTutorialPoint.transform.position);

            _tutorialPanel.gameObject.SetActive(true);
            
            _tutorialWall.gameObject.SetActive(true);
        }
    }
}
