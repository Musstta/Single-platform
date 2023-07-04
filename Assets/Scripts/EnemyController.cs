using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D[] _colliders;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Character2DController.isGameOver = true;
        }
    }

  
}
