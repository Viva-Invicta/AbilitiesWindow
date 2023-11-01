namespace AbilitiesWindow
{
    public abstract class Presenter<TModel, TView>
        where TModel : Model
        where TView : View
    {
        protected readonly TModel model;
        protected readonly TView view;

        public TModel Model => model;
        public TView View => view;

        public Presenter(TView view, TModel model)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            model?.Initialize();
            view?.Initialize();

            InitializeInternal();
        }

        protected virtual void InitializeInternal()
        { }
    }
}