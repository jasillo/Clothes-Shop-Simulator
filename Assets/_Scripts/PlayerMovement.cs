using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Animator _animator;

        private void Update()
        {
            var input = GameManager.Input.Player.Move.ReadValue<Vector2>();

            if (input.sqrMagnitude < Mathf.Epsilon)
            {
                _animator.SetBool("IsMoving", false);
                return;
            }

            _animator.SetBool("IsMoving", true);
            transform.Translate(_speed * Time.deltaTime * input);

            // look direction
            if (input.x < -Mathf.Epsilon)
                _animator.transform.localScale = new Vector3(-1, 1, 1);
            else if (input.x > Mathf.Epsilon)
                _animator.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
