using System;
using System.Collections.Generic;
using _1Game.Scripts.Core.SaveLoad.Data;
using _1Game.Scripts.Empty;
using _1Game.Scripts.Item;
using _1Game.Scripts.Particle;
using _1Game.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private GameObject _tutorialWall;
        [SerializeField] private FuelChanger _fuelChanger;
        [SerializeField] private FuelPlace _fuelPlace;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private CharacterMove _characterMove;
        [SerializeField] private Collider _fuelTankPoint;
        [SerializeField] private Collider _workPlacePoin;

        private StageController _stageController;
        private StageData _stageData;
        private CharacterTrashBagPicker _characterTrashBagPicker;
        private CharacterTutorial _characterTutorial;
        private Vector3 _startPosition;
        private bool _isMoveActive = true;
        private bool _isGamePaused = false;

        private bool _isTutorialCompleted;

        private void Awake()
        {
            _stageController = GetComponent<StageController>();
            _stageData = GetComponent<StageData>();
            _characterTrashBagPicker = _character.gameObject.GetComponent<CharacterTrashBagPicker>();
            _characterTutorial = GetComponentInChildren<CharacterTutorial>();
            _fuelTankPoint.enabled = false;
            _startPosition = _character.transform.position;
        }

        private void Start()
        {
            if (_stageData.IsTutorialCompleted)
            {
                OnTutorialClose();
            }
            else
            {
                _isTutorialCompleted = false;
            }
        }

        private void OnEnable()
        {
            _stageController.SetStage += OnSetStage;
            _stageController.CatchAllParticle += OnCleanWorkPlace;
            _characterTrashBagPicker.TakeTrashBag += OnTakeTrashBag;
            _characterTutorial.ReachedPoint += OnReachedWorkPlace;
            _characterTutorial.ReachedCart += OnReachedCart;
            _characterTutorial.ReachedRefuel += OnReachedRefuel;
            _characterTutorial.ReachedSecondPoint += OnReachedSecondWorkPlace;
            _fuelChanger.ReachedMinLevel += OnEndFuel;
            _fuelChanger.ReachedMaxLevel += OnFullFuel;
            _tutorialPanel.OnSetScreenDim += OnSetScreenDim;
            _tutorialPanel.TutorialClose += OnTutorialClose;
            _tutorialPanel.SetStartPosition += OnSetStartPosition;
            _tutorialPanel.TutorialCompleted += OnTutorialClose;
        }

        private void OnSetStage(GameObject target)
        {
            if (_isTutorialCompleted == false)
            {
                InitializeTutorial();
            }

            _stageController.SetStage -= OnSetStage;
        }

        private void OnTakeTrashBag()
        {
            _tutorialPanel.OnTakeTrashBag();
            _directionArrow.SetLookPoint(_cart.transform.position);
            _characterTrashBagPicker.TakeTrashBag -= OnTakeTrashBag;
        }

        private void OnReachedCart()
        {
            _tutorialPanel.OnReachedCart();
            _characterTutorial.ReachedCart -= OnReachedCart;
        }

        private void OnReachedWorkPlace()
        {
            _tutorialPanel.OnReachedWorkPlace();
            _workPlacePoin.enabled = false;
            _characterTutorial.ReachedPoint -= OnReachedWorkPlace;
        }

        private void OnEndFuel()
        {
            _tutorialPanel.OnEndFuel();
            _fuelTankPoint.enabled = true;
            _directionArrow.SetLookPoint(_fuelPlace.transform.position);
            _fuelChanger.ReachedMinLevel -= OnEndFuel;
        }

        private void OnReachedRefuel()
        {
            _tutorialPanel.OnReachedFuelPlace();
            _characterTutorial.ReachedRefuel -= OnReachedRefuel;
        }

        private void OnFullFuel()
        {
            _tutorialPanel.OnFullFuel();
            _fuelChanger.ReachedMaxLevel -= OnFullFuel;
        }

        private void OnCleanWorkPlace()
        {
            _tutorialPanel.OnClearWorkPlace();
            _tutorialWall.gameObject.SetActive(false);
            _directionArrow.SetLookPoint(_workPlaceTutorialSecondPoint.transform.position);
            _stageController.CatchAllParticle -= OnCleanWorkPlace;
        }

        private void OnReachedSecondWorkPlace()
        {
            _tutorialPanel.OnTutorialCompleted();
            _characterTutorial.ReachedSecondPoint -= OnReachedSecondWorkPlace;
        }

        private void InitializeTutorial()
        {
            _directionArrow.SetLookPoint(_workPlaceTutorialPoint.transform.localPosition);
            _directionArrow.StartTutorial();

            _tutorialPanel.InitializePanel();
        }

        private void DisableAll()
        {
            _stageController.SetStage -= OnSetStage;
            _stageController.CatchAllParticle -= OnCleanWorkPlace;
            _characterTrashBagPicker.TakeTrashBag -= OnTakeTrashBag;
            _characterTutorial.ReachedPoint -= OnReachedWorkPlace;
            _characterTutorial.ReachedCart -= OnReachedCart;
            _characterTutorial.ReachedRefuel -= OnReachedRefuel;
            _characterTutorial.ReachedSecondPoint -= OnReachedSecondWorkPlace;
            _fuelChanger.ReachedMinLevel -= OnEndFuel;
            _fuelChanger.ReachedMaxLevel -= OnFullFuel;
            _tutorialPanel.OnSetScreenDim -= OnSetScreenDim;
            _tutorialPanel.TutorialClose -= OnTutorialClose;
            _tutorialPanel.SetStartPosition -= OnSetStartPosition;
            _tutorialPanel.TutorialCompleted -= OnTutorialClose;
        }

        private void OnSetScreenDim()
        {
            SetTimeScale();
            SetControlCharacterMove();
        }

        private void SetTimeScale()
        {
            _isGamePaused = !_isGamePaused;
            Time.timeScale = _isGamePaused ? 0 : 1;
        }

        private void SetControlCharacterMove()
        {
            _isMoveActive = !_isMoveActive;
            _characterMove.enabled = _isMoveActive;
        }

        public void OnSetStartPosition()
        {
            _character.transform.position = _startPosition;
        }

        private void OnTutorialClose()
        {
            _fuelTankPoint.enabled = true;
            _stageData.SetFinishTutorial();
            DisableAll();
            _isTutorialCompleted = true;
            _tutorialWall.SetActive(false);
            _workPlaceTutorialPoint.gameObject.SetActive(false);
            _workPlaceTutorialSecondPoint.gameObject.SetActive(false);
            _characterTutorial.enabled = false;
            _directionArrow.gameObject.SetActive(false);
            _tutorialPanel.gameObject.SetActive(false);
            this.enabled = false;
        }
    }
}