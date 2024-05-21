using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;

        private Rigidbody2D _rb;
        private TLookDirection _lookDirection = TLookDirection.Rigth;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (GameManager.State != TState.FreeRoam) return;

            var input = GameManager.Input.Player.Move.ReadValue<Vector2>();

            if (input.sqrMagnitude < Mathf.Epsilon)
            {
                _animator.SetBool("IsMoving", false);
                return;
            }

            _animator.SetBool("IsMoving", true);
            _rb.MovePosition(_rb.position + _speed * Time.fixedDeltaTime * input);

            // look direction
            if (input.x < -Mathf.Epsilon)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                _lookDirection = TLookDirection.Left;
            }
            else if (input.x > Mathf.Epsilon)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _lookDirection = TLookDirection.Rigth;
            }
                
        }

        public TLookDirection LookDirection => _lookDirection;
    }
}
