namespace AbilitiesWindow.AbilityUI
{
    public class AbilityUIPresenter : Presenter<AbilityUIModel, AbilityUIView>
    {
        public AbilityUIPresenter(AbilityUIModel model, AbilityUIView view) 
            : base(view, model)
        { }

        protected override void InitializeInternal()
        {
            model.SelectionStatusUpdated += HandleSelectionStatusUpdated;
            model.LearnedStatusUpdated += HandleLearnedStatusUpdated;

            view.Clicked += HandleViewClicked;

            view.SetLearned(model.IsLearned);
        }

        private void HandleLearnedStatusUpdated()
        {
            view.SetLearned(model.IsLearned);
        }

        private void HandleSelectionStatusUpdated()
        {
            view.SetSelected(model.IsSelected);
        }

        private void HandleViewClicked()
        {
            model.IsSelected = true;
        }
    }
}