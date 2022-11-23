using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI
{
    public class StagesGroup : MonoBehaviour
    {
        private List<GroupStages> _groups = new();

        private void Awake()
        {
            InitializeGroup();
        }

        private void Start()
        {
            SetActiveGroup();
            gameObject.SetActive(false);
        }

        public void SetGroup(int numberGroup)
        {
            _groups[numberGroup].gameObject.SetActive(true);
        }

        private void InitializeGroup()
        {
            foreach (GroupStages group in transform.GetComponentsInChildren<GroupStages>())
            {
                _groups.Add(group);
            }
        }

        private void SetActiveGroup()
        {
            for (int i = 0; i < _groups.Count; i++)
            {
                _groups[i].gameObject.SetActive(false);
            }
        }
    }
}
