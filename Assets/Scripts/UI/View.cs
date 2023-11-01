using UnityEngine;

namespace AbilitiesWindow
{
    public abstract class View : MonoBehaviour
    {
        public void Initialize()
        {
            InitializeInternal();
        }

        protected virtual void InitializeInternal() { }
    }
}