using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using UnityEngine.AI;

namespace Objects
{
    public class Enemy : MonoBehaviour
    {

        public GameProxy Controller;
        public Character Character;
        public NavMeshAgent NavAgent;

        public float AttackSpeed = 5f;
        public float Damage = 10f;
        public int CostInCoins = 10;

        private float _attackTimer;

        private void OnEnable()
        {
            Character.Health.DieEvent += OnDead;

            Character.Movement.SetSpeed(Character.Movement.Speed);

            Controller.AddObject(gameObject);
            _attackTimer = AttackSpeed;
        }

        private void OnDisable()
        {
            Character.Health.DieEvent -= OnDead;

            Controller.RemoveObject(gameObject);
        }

        private void OnDead()
        {
            Controller.AddCoins(CostInCoins);
        }

        private void Update()
        {
            if (Controller.DefendedTower != null)
            {
                var v = Controller.DefendedTower.transform.position - gameObject.transform.position;
                var dist = v.magnitude;
                Character.Movement.MovePosition(Controller.DefendedTower.transform.position);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == Controller.DefendedTower.gameObject)
            {
                Controller.DefendedTower.Health.PureDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}