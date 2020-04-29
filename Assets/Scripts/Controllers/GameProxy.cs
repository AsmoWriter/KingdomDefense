using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Controllers {
    public interface IGameView
    {
        event Action DefendedTowerDeadEvent;
        event Action<float> DefendedTowerHealthChangeEvent;


        void StartGame();
        void StopGame();
    }

    [CreateAssetMenu(menuName = "GameProxy")]
    public class GameProxy : ScriptableObject
    {
        public event Action EndGameEvent;
        public event Action<int> CoinsChangedEvent;
        public event Action<float> DefendedTowerHealthChangeEvent;

        public Character DefendedTower { get; set; }
        public int StartCoins;
        private readonly List<GameObject> _objects = new List<GameObject>();

        private IGameView _view;
        public int Coins { get; set; }

        public void AddObject(GameObject obj)
        {
            _objects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            _objects.Remove(obj);
        }

        public void AddCoins(int value)
        {
            Coins += value;
            CoinsChangedEvent?.Invoke(Coins);
        }

        public void SubtractCoins(int value)
        {
            Coins -= value;
            CoinsChangedEvent?.Invoke(Coins);
        }
        public void ResetCoins()
        {
            Coins = StartCoins;
            CoinsChangedEvent?.Invoke(StartCoins);
        }

        public void NewGame()
        {

            _view?.StartGame();
        }

        public void OnOpen(IGameView view)
        {
            view.DefendedTowerDeadEvent += OnKingTowerDead;
            view.DefendedTowerHealthChangeEvent += OnKingTowerHealthChange;
            _view = view;
        }

        public void OnClose(IGameView view)
        {
            view.DefendedTowerDeadEvent -= OnKingTowerDead;
            view.DefendedTowerHealthChangeEvent -= OnKingTowerHealthChange;
            _view = null;
        }

        private void OnKingTowerDead()
        {
            _view?.StopGame();

            foreach (var o in _objects.ToArray())
                Destroy(o);
            _objects.Clear();

            EndGameEvent?.Invoke();
        }

        private void OnKingTowerHealthChange(float value)
        {
            DefendedTowerHealthChangeEvent?.Invoke(value);
        }
    }

}
