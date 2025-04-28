using System;
using System.Collections.Generic;
using Steamworks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class Movement : MonoBehaviour
    {
        

        public static void MoveCharacter(GameObject gameObject, int moveDirection)
        {
            Rigidbody2D body = gameObject.transform.GetComponent<Rigidbody2D>();
            body.linearVelocityX = moveDirection * ConstController.Manager.MoveSpeed;
        }
    }
}