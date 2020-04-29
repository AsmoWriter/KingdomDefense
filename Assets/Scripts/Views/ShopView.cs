using Controllers;
using UnityEngine;
using UnityEngine.UI;
using Objects;

namespace Views
{
    public class ShopView : MonoBehaviour
    {
        public GameController ViewController;
        public Transform GridElement;
        public GameObject[] Towers;
        public Button ShopItemButtonPrefab;

        private void Awake()
        {
            foreach (GameObject tower in Towers)
            {
                ShopItemButtonPrefab.GetComponent<ShopItemByButton>().NameTextOnButton.text = tower.GetComponent<TowerShopItem>().TowerName;
                ShopItemButtonPrefab.GetComponent<ShopItemByButton>().CostTextOnButton.text = tower.GetComponent<TowerShopItem>().BuildCost.ToString();
                ShopItemButtonPrefab.GetComponent<ShopItemByButton>().TowerPrefab = tower.gameObject;
                GameObject ItemButton = Instantiate(ShopItemButtonPrefab.gameObject);
                ItemButton.transform.SetParent(GridElement, false);
            }
        }
        public void OnCloseButtonClick()
        {
            gameObject.SetActive(false);
        }
    }
}

