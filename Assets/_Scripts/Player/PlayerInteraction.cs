using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    /// <summary>
    /// the interactable NPC must be distanced
    /// </summary>
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private LayerMask _mask;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private Vector2 _size;

        private PlayerMovement _movement;
        private Merchant _currentNpc;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            // detect interaction if player is in freeroam state
            if (GameManager.State != TState.FreeRoam) return;

            // calculate de area point detection
            Vector2 originPoint;
            if (_movement.LookDirection == TLookDirection.Rigth)
                originPoint = (Vector2)transform.position + _offset;
            else
                originPoint = (Vector2)transform.position + new Vector2(-_offset.x, _offset.y);

            // detect NPC in from the player
            var npcCol = Physics2D.OverlapBox(originPoint, _size, 0, _mask);
            if (npcCol != null)
            {
                Assert.IsNotNull(npcCol.GetComponent<Merchant>(), "Npc layed object has no Merchant script");

                // show the interactable mark
                var npc = npcCol.GetComponent<Merchant>();
                if (npc != _currentNpc && _currentNpc != null)
                    _currentNpc.InteractionMark(false);
                _currentNpc = npc;
                _currentNpc.InteractionMark(true);
            }
            else 
            {
                // hide the interactable mark 
                if ( _currentNpc != null)
                {
                    _currentNpc.InteractionMark(false);
                    _currentNpc = null;
                }
            }

            // if interact button is pressed and npc is in front
            if (_currentNpc != null && GameManager.Input.Player.Interact.WasPressedThisFrame())
            {
                _currentNpc.Interact();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var right = (Vector2)transform.position + _offset;
            var left = (Vector2)transform.position + new Vector2(-_offset.x, _offset.y);
            Gizmos.DrawWireCube(right, _size);
            Gizmos.DrawWireCube(left, _size);
        }
    }
}
