using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using UnityEngine.AI;

namespace Objects
{
    public class Enemy : MonoBehaviour
    {
        public enum WeaponType
        {
            None,
            Ranged,
            Melee
        }

        public GameProxy Controller;
        public Character Character;
        public NavMeshAgent NavAgent;
        public GameObject BulletPrefab;
        public Vector3 WeaponOffset = new Vector3(1, 1, 0);

        public WeaponType UsedWeaponType;
        public float AttackDistance = 5f;
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
                if (dist > AttackDistance)
                {
                    Character.Movement.MovePosition(Controller.DefendedTower.transform.position);
                }
                else if (dist <= AttackDistance)
                {
                    Character.Movement.MovePosition(gameObject.transform.position);
                    if (UsedWeaponType == WeaponType.None) return;
                    else if (UsedWeaponType == WeaponType.Ranged)
                    {
                        if (_attackTimer > 0)
                        {
                            _attackTimer -= Time.deltaTime;
                            if (_attackTimer <= 0)
                            {
                                _attackTimer = AttackSpeed;
                                gameObject.transform.LookAt(Controller.DefendedTower.transform.position + new Vector3(0, 1, 0));
                                Instantiate(BulletPrefab, (transform.position + WeaponOffset), transform.rotation);
                            }
                        }
                    }
                    else if (UsedWeaponType == WeaponType.Melee)
                    {
                        if (_attackTimer > 0)
                        {
                            _attackTimer -= Time.deltaTime;
                            if (_attackTimer <= 0)
                            {
                                _attackTimer = AttackSpeed;
                                var health = Controller.DefendedTower.GetComponentInParent<Health>();
                                health.PureDamage(Damage);
                                Debug.Log(health.Hitpoints);
                            }
                        }
                    }
                }
            }
        }
    }
}