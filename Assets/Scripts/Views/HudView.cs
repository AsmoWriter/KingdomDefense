using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class HudView : MonoBehaviour
    {
        public GameController ViewController;
        public GameObject HpBar;
        public Text HitpointsText;
        public Text CoinsText;

        private void OnEnable()
        {
            ViewController.DefendedTowerHealthChangeEvent += UpdateHpCount;
            ViewController.DefendedTowerHealthChangeEvent += UpdateHpBar;
            ViewController.Controller.CoinsChangedEvent += UpdateCoins;
        }

        private void UpdateHpCount(float count)
        {
            HitpointsText.text = count.ToString() + "%";
        }
        private void UpdateHpBar(float count)
        {
            HpBar.transform.localScale = new Vector3(count / ViewController.Controller.DefendedTower.Health.HitpointMax, 1, 1);
        }
        private void UpdateCoins(int count)
        {
            CoinsText.text = count.ToString();
        }
    }
}