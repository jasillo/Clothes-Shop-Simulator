using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static PlayerInput Input;

        private void Awake()
        {
            Input = new PlayerInput();
            Input.Enable();
        }
    }
}
