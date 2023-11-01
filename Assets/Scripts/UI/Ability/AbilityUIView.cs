using System;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitiesWindow.AbilityUI
{
    public class AbilityUIView : View
    {
        internal event Action Clicked;

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

        internal void SetText(string text)
        {
            label.text = text;
        }

        internal void SetLearned(bool isLearned)
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

        internal void SetSelected(bool isSelected)
        {
            if (selectedRoot)
            {
                selectedRoot.SetActive(isSelected);
            }
        }
    }
}