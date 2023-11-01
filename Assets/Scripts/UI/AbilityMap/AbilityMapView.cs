using System;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitiesWindow.AbilityMap
{
    public class AbilityMapView : View
    {
        [SerializeField] private Button learnButton;
        [SerializeField] private Button forgetButton;
        [SerializeField] private Button forgetAllButton;
        [SerializeField] private Text costText;

        internal event Action LearnButtonPressed;
        internal event Action ForgetButtonPressed;
        internal event Action ForgetAllButtonPressed;

        protected override void InitializeInternal()
        {
            learnButton.onClick.AddListener(() => LearnButtonPressed?.Invoke());   
            forgetButton.onClick.AddListener(() => ForgetButtonPressed?.Invoke());
            forgetAllButton.onClick.AddListener(() => ForgetAllButtonPressed?.Invoke());
        }

        internal void SetLearnButtonActive(bool isActive)
        {
            learnButton.interactable = isActive;
        }

        internal void SetForgetButtonActive(bool isActive)
        {
            forgetButton.interactable = isActive;
        }

        internal void SetCost(int cost) 
        {
            costText.text = cost == 0 ? "-" : cost.ToString();
        }
    }
}