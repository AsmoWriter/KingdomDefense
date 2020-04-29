using UnityEngine;
using Controllers;

namespace Objects
{
    public class TowerShopItem : MonoBehaviour
    {
        public GameProxy Controller;

        public TowrerWeapon Weapon;
        public string TowerName;
        public int BuildCost;
        public int DestroyCost;
        private void OnEnable()
        {
            Controller.AddObject(gameObject);
        }

        private void OnDisable()
        {
            Controller.RemoveObject(gameObject);
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

