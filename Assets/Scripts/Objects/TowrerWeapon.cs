using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class TowrerWeapon : MonoBehaviour
    {
        public enum DamageMode
        {
            None,
            Fire,
            Ice,
            Electic
        }

        public enum WeaponType
        {
            None,
            Ranged,
            Area
        }

        public DamageMode UsedDamageMode;
        public WeaponType UsedWeaponType;

        public float AttackDistance = 10f;
        public float AttackSpeed = 2f;
        public float Damage = 10f;
        public GameObject BulletPrefab;

        private List<GameObject> _enemys = new List<GameObject>();
        private GameObject _closeEnemy;
        private float _attackTimer;

        private void OnEnable()
        {
            CreateAttackTrigger();
            _attackTimer = AttackSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.tag == "Enemy")
            {
                _enemys.Add(other.gameObject);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_enemys.Contains(other.gameObject))
            {
                _enemys.Remove(other.gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (UsedWeaponType == WeaponType.None)
            {
                return;
            }
            else if (UsedWeaponType == WeaponType.Ranged)
            {
                FiendCloseEnemy();
                if (_closeEnemy != null && _enemys.Contains(_closeEnemy))
                {
                    RangeAttackOpenFire();
                }
            }
            else if (UsedWeaponType == WeaponType.Area)
            {
                AreaAttackOpenFire();
            }
        }
        

        public void CreateAttackTrigger()
        {
            SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = AttackDistance;
            sphereCollider.isTrigger = true;
        }


        public GameObject FiendCloseEnemy()
        {

            float distance = Mathf.Infinity;
            foreach (GameObject ga in _enemys)
            {
                if (ga == null)
                {
                    _enemys.Remove(ga);
                    _closeEnemy = null;
                }
                else
                {
                    Vector3 anotherDistanceVector = ga.transform.position - transform.position;
                    float anotherDistance = anotherDistanceVector.sqrMagnitude;
                    if (anotherDistance < distance)
                    {

                        _closeEnemy = ga;
                        distance = anotherDistance;
                    }
                }
                return _closeEnemy;
            }
            return _closeEnemy;
        }
        public void RangeAttackOpenFire()
        {
            if (_closeEnemy != null && _enemys.Contains(_closeEnemy))
            {
                if (_attackTimer > 0)
                {
                    _attackTimer -= Time.deltaTime;
                    if (_attackTimer <= 0)
                    {
                        _attackTimer = AttackSpeed;
                        var bullet = BulletPrefab.GetComponent<Bullet>();
                        bullet.SetTarget(_closeEnemy);
                        bullet.SetDamage(Damage);
                        bullet.SetDamageMode(UsedDamageMode);
                        Instantiate(BulletPrefab, transform.position, transform.rotation);
                    }
                }
            }
        }

        public void AreaAttackOpenFire()
        {

                if (_attackTimer > 0)
                {
                    _attackTimer -= Time.deltaTime;
                    if (_attackTimer <= 0)
                    {
                        _attackTimer = AttackSpeed;
                        foreach (GameObject ga in _enemys)
                        {
                        if (ga == null)
                        {
                            _enemys.Remove(ga);
                            return;
                        }
                        else
                        {
                            var health = ga.gameObject.GetComponentInParent<Health>();
                            if (health != null && health.Hitpoints >= 0)
                            {
                                if (UsedDamageMode == DamageMode.None) health.PureDamage(Damage);
                                else if (UsedDamageMode == DamageMode.Fire) health.FireDamage(Damage);
                                else if (UsedDamageMode == DamageMode.Ice) health.IceDamage(Damage);
                                else if (UsedDamageMode == DamageMode.Electic) health.ElecticDamage(Damage);
                            }
                        }
                    }
                }
            }
        }
    }
}

