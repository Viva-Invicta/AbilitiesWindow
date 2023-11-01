using UnityEngine;
using UnityEngine.UI.Extensions;

namespace AbilitiesWindow
{
    public class AbilityConnectionView : View
    {
        [SerializeField]
        private UILineRenderer line;

        public void SetCoordinates(Vector2 start, Vector2 end) => line.Points = new[] { start, end };
    }
}