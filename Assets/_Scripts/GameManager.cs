using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        
        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;

            Input = new PlayerInput();
            Input.Enable();
        }

        public static PlayerInput Input { private set; get; }
        public static TState State { get; set; } = TState.FreeRoam;
    }
}
