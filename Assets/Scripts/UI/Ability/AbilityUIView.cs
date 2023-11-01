using System;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitiesWindow.AbilityUI
{
    public class AbilityUIView : View
    {
        public event Action Clicked;

        [SerializeField] private Text label;

        [SerializeField] private GameObject learnedRoot;
        [SerializeField] private GameObject notLearnedRoot;
        [SerializeField] private GameObject selectedRoot;

        [SerializeField] private Button button;

        protected override void InitializeInternal()
        {
            if (button)
            {
                button.onClick.AddListener(() => Clicked?.Invoke());
            }
        }

        public void SetText(string text)
        {
            label.text = text;
        }

        public void SetLearned(bool isLearned)
        {
            if (learnedRoot)
            {
                learnedRoot.SetActive(isLearned);
            }
            if (notLearnedRoot)
            {
                notLearnedRoot.SetActive(!isLearned);
            }
        }

        public void SetSelected(bool isSelected)
        {
            if (selectedRoot)
            {
                selectedRoot.SetActive(isSelected);
            }
        }
    }
}