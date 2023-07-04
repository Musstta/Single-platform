using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class Character2DController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float moveSpeed = 300.0f;

    [SerializeField]
    bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float jumpForce = 140.0F;

    [SerializeField]
    float jumpGraceTime = 0.20F;

    [SerializeField]
    float fallMultiplier = 3.0F;

    [Header("Extras")]
    [SerializeField]
    Animator animator;

    Rigidbody2D _rb;

    float _inputX;
    float _gravityY;
    float _lastTimeJumPressed;

    bool _isMoving;
    bool _isJumpPressed;
    bool _isJumping;


   


    public static bool isGameOver;
    public GameObject gameOverScreen;
    public bool isStarted;
    public GameObject Started;
    
   
   public void Awake()
    {

        _rb = GetComponent<Rigidbody2D>();
        _gravityY = -Physics2D.gravity.y;

        isGameOver = false;

       
    }

    void Update()
    {
        HandleInputs();

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            PauseGame();
        }
        if (isGameOver == false)
        {
            ResumeGame();
            
            
        }
    }

    void FixedUpdate()
    {
        HandleMove();
        HandleFlipX();
        HandleJump();
    }

     void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        ResetGame(); 
        Time.timeScale = 1f;
    }
    private void ResetGame()
    {
        
        isGameOver = false;
       
    }

    void HandleJump()
    {  
        if (_lastTimeJumPressed > 0.0F && Time.time- _lastTimeJumPressed <= jumpGraceTime)
        {
            //Coyote time: La tecla de salto se ha presionado sin embargo no se ha realizado el salto
            _isJumpPressed = true;
        }
        else
        {
            _lastTimeJumPressed = 0.0F;
        }

        if (_isJumpPressed)
        { 
            bool isGrounded = IsGrounded();
            if (isGrounded)
            {
                _rb.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
            }   
        }

        if(_rb.velocity.y < -0.01F)
        {
            _rb.velocity -= Vector2.up * _gravityY * fallMultiplier * Time.fixedDeltaTime;
        }

        _isJumping = !IsGrounded();
    }

    void HandleFlipX()
    {
        if (!_isMoving)
        {
            return;
        }
        bool facingRight = _inputX > 0.0F;
        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            transform.Rotate(0.0F, 180.0F, 0.0F);
        }
    }

    
    void HandleMove()
    {
        bool isMoving = animator.GetFloat("speed") > 0.01F;
        if (isMoving != _isMoving && !_isJumping)
        {
            animator.SetFloat("speed", Mathf.Abs(_inputX));
        }

        bool isJumping = animator.GetBool("isJumping");
        if (isJumping != _isJumping)
        {
            animator.SetBool("isJumping", _isJumping);
        }

        float velocityX = _inputX * moveSpeed * Time.fixedDeltaTime;
        Vector2 direction = new Vector2(velocityX, _rb.velocity.y);
        _rb.velocity = direction;
    }

    void HandleInputs()
    {
        _inputX = UnityEngine.Input.GetAxisRaw("Horizontal");
        _isMoving = _inputX != 0.0F;

        _isJumpPressed = UnityEngine.Input.GetButtonDown("Jump");
        if (_isJumpPressed)
        {
            // Comienza el calculo del coyote Time
            _lastTimeJumPressed = Time.time;
        }
    }

    bool IsGrounded()
    {
        return
            Physics2D.OverlapCapsule(
                groundCheck.position, new Vector2(0.63F, 0.4F),
                CapsuleDirection2D.Horizontal, 0.0F, groundMask);
    }

}
