using System.Linq;

namespace AbilitiesWindow.AbilityMap
{
    public class AbilityMapPresenter : Presenter<AbilityMapModel, AbilityMapView>
    {
        private readonly AbilityFactory abilitiesFactory;

        public AbilityMapPresenter(AbilityMapView view, AbilityMapModel model, AbilityFactory abilitiesFactory) 
            : base(view, model)
        {
            this.abilitiesFactory = abilitiesFactory;
        }

        protected override void InitializeInternal()
        {
            view.ForgetButtonPressed += HandleForgetButtonPressed;
            view.ForgetAllButtonPressed += HandleForgetAllButtonPressed;
            view.LearnButtonPressed += HandleLearnButtonPressed;

            UpdateView();

            model.AbilitySelected += UpdateView;

            var abilitiesModels = abilitiesFactory.Create();
             
            model.SetAbilityModels(abilitiesModels);
        }

        private void HandleForgetButtonPressed()
        {
            model.ForgetSelectedAbility();
        }

        private void HandleForgetAllButtonPressed()
        {
            model.ForgetAllAbilities();
        }

        private void HandleLearnButtonPressed()
        {
            model.LearnSelectedAbility();
        }

        private void UpdateView()
        {
            var selectedAbilityModel = model.SelectedAbilityModel;
            if (selectedAbilityModel == null)
            {
                view.SetLearnButtonActive(false);
                view.SetForgetButtonActive(false);
                view.SetCost(0);

                return;
            }

            view.SetLearnButtonActive(selectedAbilityModel.CanLearn);
            view.SetForgetButtonActive(selectedAbilityModel.CanForget);
            view.SetCost(selectedAbilityModel.Cost);
        }
    }
}