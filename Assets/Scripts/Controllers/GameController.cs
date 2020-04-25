using System;
using Controllers;
using Objects;
using UnityEngine;

namespace Views
{
    public class GameController : MonoBehaviour, IGameView
    {
        public GameProxy Controller;
        public Character DefendedTowerPrefab;
        public Transform StartBuildPoint;
        public GameObject[] GameObjects;

        public event Action DefendedTowerDeadEvent;
        public event Action<float> DefendedTowerHealthChangeEvent;

        private void OnEnable()
        {
            Controller.OnOpen(this);
            SpawnPlayer();
        }

        private void OnDisable()
        {
            Controller.OnClose(this);
            
        }

        private void SpawnPlayer()
        {
            var kingTower = Instantiate(DefendedTowerPrefab, StartBuildPoint.position, StartBuildPoint.rotation);
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
            SpawnPlayer();
            foreach (var o in GameObjects)
                o.SetActive(true);
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

            foreach (var o in GameObjects)
                o.SetActive(false);
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
