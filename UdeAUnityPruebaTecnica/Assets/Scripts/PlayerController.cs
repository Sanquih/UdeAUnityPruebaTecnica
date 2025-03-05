using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 100f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");  // Rotation
        float moveZ = Input.GetAxis("Vertical");    // Movement forward

        transform.Rotate(0, moveX * rotationSpeed * Time.deltaTime, 0);

        if (moveZ >= 0)
        {
            Vector3 moveDirection = transform.forward * moveZ;

            transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

            animator.SetFloat("movement", Mathf.Abs(moveZ));
        }
    }
}
