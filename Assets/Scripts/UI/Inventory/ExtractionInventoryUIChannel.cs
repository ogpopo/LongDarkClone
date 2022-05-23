using UnityEngine;
using UnityEngine.Timeline;

namespace LongDark
{
    [CreateAssetMenu(menuName = "ScriptableObject/Inventory/ExtractionInventoryUIChannel")]
    public class ExtractionInventoryUIChannel : ScriptableObject
    {
        [HideInInspector] public InventoryHolder _extractioninventoryHolder;

        public delegate void ExtractionInventoryToggleCallback(InventoryHolder inventoryHolder);
        public ExtractionInventoryToggleCallback OnExtractionInventoryToggle;

        public void RaiseToggle()
        {
            OnExtractionInventoryToggle?.Invoke(_extractioninventoryHolder);
        }

    }
}