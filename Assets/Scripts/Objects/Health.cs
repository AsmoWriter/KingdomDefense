using System;
using UnityEngine;
using System.Collections;

namespace Objects
{
    public class Health : MonoBehaviour
    {

        public event Action DieEvent;
        public event Action<float> ChangeEvent;

        public float HitpointMax;
        public float FireResistance;
        public float IceResistance;
        public float ElecticResistance;

        public float Hitpoints { get; private set; }

        private int _timer = 5;

        private void OnEnable()
        {
            Hitpoints = HitpointMax;
        }

        public void PureDamage(float value)
        {
            Hitpoints = Mathf.Max(0f, Hitpoints - value);

            ChangeEvent?.Invoke(Hitpoints);

            if (Hitpoints > 0f)
                return;

            DieEvent?.Invoke();
        }
        public void FireDamage(float value)
        {
            Hitpoints = Mathf.Max(0f, Hitpoints - (value - value * (FireResistance / 100)));
            StartCoroutine(TickDamageRoutine(value));

            ChangeEvent?.Invoke(Hitpoints);

            if (Hitpoints > 0f)
                return;

            DieEvent?.Invoke();
        }
        public void IceDamage(float value)
        {
            Hitpoints = Mathf.Max(0f, Hitpoints - (value - value * (IceResistance / 100)));
            var movement = GetComponentInParent<Movement>();
            if (movement.Speed > 0)
            {
                movement.SetSpeed(movement.Speed - (movement.Speed * 0.1f));
            }

            ChangeEvent?.Invoke(Hitpoints);

            if (Hitpoints > 0f)
                return;

            DieEvent?.Invoke();
        }
        public void ElecticDamage(float value)
        {
            Hitpoints = Mathf.Max(0f, Hitpoints - (value - value * (ElecticResistance / 100)));

            ChangeEvent?.Invoke(Hitpoints);

            if (Hitpoints > 0f)
                return;

            DieEvent?.Invoke();
        }
        private IEnumerator TickDamageRoutine(float damageValue)
        {
            {
                yield return new WaitForSeconds(1);
                for (int i = _timer; i > 0; i--)
                {
                    Hitpoints -= (damageValue * 0.05f);
                    if (Hitpoints <= 0)
                    {
                        DieEvent?.Invoke();
                    }
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }
}
