using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public PlayerMovement playerMovement;
    public Animator animator;

    private void Awake()
    {
        HandleSingletonInstance();
    }

    void Start()
    {
        InitializePlayerComponents();
    }

    void FixedUpdate()
    {
        UpdatePlayerMovement();
    }

    void LateUpdate()
    {
        UpdateAnimatorState();
    }

    private void HandleSingletonInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePlayerComponents()
    {
        playerMovement = GetComponent<PlayerMovement>();

        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null)
        {
            animator = engineEffect.GetComponent<Animator>();
        }
    }

    private void UpdatePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.Move();
            playerMovement.MoveBound(); 
        }
    }

    private void UpdateAnimatorState()
    {
        if (animator != null && playerMovement != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
