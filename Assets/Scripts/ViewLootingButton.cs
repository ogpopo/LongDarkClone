using System;
using UnityEngine;

namespace LongDark
{
    public class ViewLootingButton : MonoBehaviour
    {
        public static event Action<InventoryHolder> OnLooting; 

        [SerializeField] private GameObject _lootinButton;

        [SerializeField] private ExtractionInventoryUIChannel _extractionInventoryUIChannel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == GameObject.FindWithTag("Player"))
            {
                OnLooting?.Invoke(gameObject.GetComponent<InventoryHolder>());
                _lootinButton.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == GameObject.FindWithTag("Player"))
            {
                _lootinButton.SetActive(false);
            }
        }
    }
}