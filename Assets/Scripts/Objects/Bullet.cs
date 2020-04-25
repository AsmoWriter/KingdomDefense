using UnityEngine;

namespace Objects
{
    public class Bullet : MonoBehaviour
    {
        public TowrerWeapon.DamageMode UsedDamageMode;

        public float Speed = 5f;
        public float LifeTime = 5f;
        public GameObject ShootTarget;
        public float Damage;

        public Rigidbody Rigidbody;

        private float _timer;

        private void OnEnable()
        {
            _timer = LifeTime;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
                Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            gameObject.transform.LookAt(ShootTarget.transform.position + new Vector3(0, 1, 0));
            Rigidbody.velocity = transform.TransformDirection(Vector3.forward * Speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var health = collision.gameObject.GetComponentInParent<Health>();
            if (health != null && health.Hitpoints >= 0)
            {
                if (UsedDamageMode == TowrerWeapon.DamageMode.None) health.PureDamage(Damage);
                else if (UsedDamageMode == TowrerWeapon.DamageMode.Fire) health.FireDamage(Damage);
                else if (UsedDamageMode == TowrerWeapon.DamageMode.Ice) health.IceDamage(Damage);
                else if (UsedDamageMode == TowrerWeapon.DamageMode.Electic) health.ElecticDamage(Damage);
            }
            Destroy(gameObject);
        }

        public void SetTarget(GameObject target)
        {
            ShootTarget = target;
        }

        public void SetDamage(float damage)
        {
            Damage = damage;
        }
        public void SetDamageMode(TowrerWeapon.DamageMode damageMode)
        {
            UsedDamageMode = damageMode;
        }
    }
}
