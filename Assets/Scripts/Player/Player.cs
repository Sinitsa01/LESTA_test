using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Vector2 playerMovementInput;
    private Vector2 playerMouseInput;
    private bool isJump;
    private bool isGrounded = true;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private Rigidbody playerBody;

    [SerializeField] private float baseSpeed;
    [SerializeField] private float jumpForse;

    [SerializeField] private InputReader inputReader;

    [SerializeField] private Transform mainCameraTransform;

    [SerializeField] private HealthSystem healthSystem;


    [SerializeField] private Animator animator;
   
    private MovementPlayer movementPlayer;

    public static  Action Victory;
    public static  Action GameOver;

    private void OnEnable()
    {
        HealthSystem.Death += OnDeath;
    }

    private void OnDeath()
    {
        GameOver?.Invoke();
    }

    private void Awake()
    {
        movementPlayer = GetComponent<MovementPlayer>();
    }

    private void Update()
    {
        playerMovementInput = inputReader.GetAxis();
        isJump = inputReader.GetIsJump();
        Anim();

    }

    private void FixedUpdate()
    {
        movementPlayer.GroundCheck();
        movementPlayer.Move();
        movementPlayer.Jump();
    }

    public Vector2 PlayerMovementInput => playerMovementInput;

    public bool IsJump => isJump;
    public bool IsGrounded => isGrounded;
    public float BaseSpeed => baseSpeed;
    public float JumpForse => jumpForse ;

    public void SetIsJump(bool isJump) {  this.isJump = isJump; }

    public void SetIsGrounded(bool isOnGround) {  this.isGrounded = isOnGround; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            Victory?.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            GameOver?.Invoke();

        }
    }

    private void Anim()
    {
        if (playerMovementInput != Vector2.zero)
        {
            animator.SetBool("IsMove", true);
        }else animator.SetBool("IsMove", false);
    }
}
