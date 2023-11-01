namespace AbilitiesWindow
{
    public abstract class Model
    {
        public void Initialize()
        {
            InitializeInternal();
        }

        protected virtual void InitializeInternal() { }
    }
}