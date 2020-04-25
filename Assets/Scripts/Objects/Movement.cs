using System;
using UnityEngine;
using UnityEngine.AI;

namespace Objects
{
    public enum MovementType
    {
        None,
        Direction,
        Position
    }

    public class Movement : MonoBehaviour
    {
        public Rigidbody Rigidbody;
        public NavMeshAgent Agent;

        public float Speed = 3f;
        public float LookSlerp = 10f;

        private MovementType _type;
        private Vector3 _direction;
        private Vector3 _look;

        public float RealVelocity
        {
            get { return Rigidbody.velocity.magnitude; }
        }

        private void Update()
        {
            if (_look.sqrMagnitude > 0.01f)
            {
                Rigidbody.rotation = Quaternion.Slerp(Rigidbody.rotation, Quaternion.LookRotation(_look), Time.deltaTime * LookSlerp);
            }
        }

        private void FixedUpdate()
        {
            if (_type == MovementType.Direction)
            {
                Rigidbody.angularVelocity = Vector3.zero;
                Rigidbody.velocity = _direction * Speed;
            }
        }

        public void MoveDirection(Vector3 direction)
        {
            _type = MovementType.Direction;
            _direction = direction.normalized;
        }

        public void MovePosition(Vector3 position)
        {
            _type = MovementType.Position;
            Agent.SetDestination(position);
        }

        public void LookDirection(Vector3 direction)
        {
            _look = direction.normalized;
        }

        public void SetSpeed(float value)
        {
            Speed = value;
            if (Agent != null)
            {
                Agent.speed = value;
            }
        }
    }
}
