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
        public event Action<int> ScoreChangedEvent;
        public event Action<float> PlayerHealthChangeEvent;

        
        public Character DefendedTower { get; set; }
        public int StartCoins;
        private readonly List<GameObject> _objects = new List<GameObject>();

        private IGameView _view;
        private int _coins;

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
            _coins += value;
            ScoreChangedEvent?.Invoke(_coins);
        }

        public void SubtractCoins(int value)
        {
            _coins -= value;
            ScoreChangedEvent?.Invoke(_coins);
        }

        public void NewGame()
        {
            _coins = StartCoins;
            _view?.StartGame();
        }

        public void OnOpen(IGameView view)
        {
            view.DefendedTowerDeadEvent += OnPlayerDead;
            view.DefendedTowerHealthChangeEvent += OnPlayerHealthChange;
            _view = view;
        }

        public void OnClose(IGameView view)
        {
            view.DefendedTowerDeadEvent -= OnPlayerDead;
            view.DefendedTowerHealthChangeEvent -= OnPlayerHealthChange;
            _view = null;
        }

        private void OnPlayerDead()
        {
            _view?.StopGame();

            foreach (var o in _objects.ToArray())
                Destroy(o);
            _objects.Clear();

            EndGameEvent?.Invoke();
        }

        private void OnPlayerHealthChange(float value)
        {
            PlayerHealthChangeEvent?.Invoke(value);
        }
    }

}
