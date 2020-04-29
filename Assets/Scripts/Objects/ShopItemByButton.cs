using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controllers;

namespace Objects
{
    public class ShopItemByButton : MonoBehaviour
    {
        public GameProxy Controller;
        public BuildManagerScriptableObject BuildManager;
        public Text NameTextOnButton;
        public Text CostTextOnButton;
        public GameObject TowerPrefab;

        public void OnBuildTowerButtonClick()
        {
            if (BuildManager.BuildingTarget != null && Controller.Coins >= TowerPrefab.GetComponent<TowerShopItem>().BuildCost)
            {
                Controller.SubtractCoins(TowerPrefab.GetComponent<TowerShopItem>().BuildCost);
                BuildManager.BuildingTarget.GetComponent<BuildingPlatform>().CanBuild = false;
                BuildManager.BuildTower(TowerPrefab);
            }
        }
    }
}

