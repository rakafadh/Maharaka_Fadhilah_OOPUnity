using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;  // Singleton instance
    public PlayerMovement playerMovement;  // Reference to PlayerMovement script
    public Animator animator;  // Reference to Animator component

    void Awake()
    {
        // Setup Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Get reference to PlayerMovement and Animator
        playerMovement = GetComponent<PlayerMovement>();
        animator = transform.Find("Engine/EngineEffect").GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Call Move method from PlayerMovement
        playerMovement.Move();
    }

    void LateUpdate()
    {
        // Set Animator parameter based on PlayerMovement's IsMoving
        animator.SetBool("IsMoving", playerMovement.IsMoving());
    }
}