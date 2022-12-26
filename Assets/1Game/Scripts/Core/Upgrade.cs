using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class Upgrade
    {
        public List<Level> Levels;
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => _maxLevel;

        public int Number=>_number;
        
        private int _currentLevel;
        private int _maxLevel;
        private int _number;
        private bool isSetLevelEnd;
        
        public Upgrade(List<Level> levels,int currentLevel,int maxLevel)
        {
            Levels = new List<Level>();
            
            foreach (Level level in levels)
            {
                Levels.Add(level);
            }
            
            _currentLevel = currentLevel;
            _maxLevel = maxLevel;
        }
        
        public void UpLevel()
        {
            if (CurrentLevel <= MaxLevel)
                _currentLevel++;
        }

        public void SetLevel(int number)
        {
            if (isSetLevelEnd != false) return;
            _currentLevel = number;
            isSetLevelEnd = true;
        }

        public void SetNumber(int number)
        {
            _number = number;
        }
    }
}