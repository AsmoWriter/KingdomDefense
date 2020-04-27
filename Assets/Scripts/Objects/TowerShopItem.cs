using UnityEngine;
using Controllers;

namespace Objects
{
    public class TowerShopItem : MonoBehaviour
    {
        public GameProxy Controller;
        public TowrerWeapon Weapon;
        public int BuildCost;
        public int DestroyCost;
        private void OnEnable()
        {
            OnBuilt();
        }

        private void OnDisable()
        {
            OnDestroyed();
        }

        private void OnBuilt()
        {
            Controller.SubtractCoins(BuildCost);
        }

        private void OnDestroyed()
        {
            Controller.AddCoins(DestroyCost);
        }
    }
}

