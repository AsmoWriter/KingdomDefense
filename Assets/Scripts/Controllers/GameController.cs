using System;
using Objects;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour, IGameView
    {
        public GameProxy Controller;
        public Character DefendedTowerPrefab;
        public Transform DefendedTowerStartPoint;
        public GameObject EnemySpawner;

        public GameObject StartMenu;
        public GameObject EndGameWindow;
        public GameObject HUD;

        public event Action DefendedTowerDeadEvent;
        public event Action<float> DefendedTowerHealthChangeEvent;

        private void OnEnable()
        {
            StartMenu.SetActive(true);
            Controller.OnOpen(this);
        }
        private void OnDisable()
        {
            Controller.OnClose(this);
        }

        private void SpawnKingTower()
        {
            var kingTower = Instantiate(DefendedTowerPrefab, DefendedTowerStartPoint.position, DefendedTowerStartPoint.rotation);
            if (kingTower == null)
                return;
            var health = kingTower.Health;
            if (health == null)
                return;

            Controller.DefendedTower = kingTower;

            health.DieEvent += OnDefendedTowerDead;
            health.ChangeEvent += OnDefendedTowerHealthChange;
            OnDefendedTowerHealthChange(health.Hitpoints);
        }

        public void StartGame()
        {
            SpawnKingTower();
            EnemySpawner.SetActive(true);
            StartMenu.SetActive(false);
            HUD.SetActive(true);
            EndGameWindow.SetActive(false);
            Controller.ResetCoins();
        }

        public void StopGame()
        {
            var player = Controller.DefendedTower;
            if (player != null && player.Health != null)
            {
                player.Health.DieEvent -= OnDefendedTowerDead;
                player.Health.ChangeEvent -= OnDefendedTowerHealthChange;
            }

            Controller.DefendedTower = null;

            EnemySpawner.SetActive(false);
            EndGameWindow.SetActive(true);
            HUD.SetActive(false);
        }
        public void ExitToMenu()
        {
            EndGameWindow.SetActive(false);
            StartMenu.SetActive(true);
        }
        

        private void OnDefendedTowerDead()
        {
            DefendedTowerDeadEvent?.Invoke();
        }

        private void OnDefendedTowerHealthChange(float value)
        {
            DefendedTowerHealthChangeEvent?.Invoke(value);
        }
    }
}