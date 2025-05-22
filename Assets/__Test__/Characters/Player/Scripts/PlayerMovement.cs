using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;

    public bool movementFreeze = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!movementFreeze)
        {
            groundedPlayer = controller.isGrounded;

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(playerSpeed * Time.deltaTime * move);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }

    public void ResetAnimation()
    {
        animator.SetBool("isRunning", false);
    }
}