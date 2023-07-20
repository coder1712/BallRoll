using UnityEngine;

public class bossEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Rigidbody rb;

    public GameObject projectile;
    public Transform target;

    public float followRange = 10f;
    public float attackRange = 5f;
    public float attackDamage = 20f;
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;
    private float nextJumpTime = 0f;
    public float jumpForce = 5.0f;

    public float movementSpeed = 2f;
    public float rotationSpeed = 5f;

    // private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!isDead)
        {
            // Follow and face the player
            if (target != null)
            {
                Vector3 directionToTarget = target.position - transform.position;
                if (directionToTarget.magnitude <= followRange)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    // Move towards the player if they are in attack range
                    if (directionToTarget.magnitude <= attackRange)
                    {
                        AttackPlayer();
                    }
                    else
                    {
                        MoveTowardsPlayer();
                    }
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToMove = target.position - transform.position;
        directionToMove.y = 0f;
        directionToMove.Normalize();
        transform.Translate(directionToMove * movementSpeed * Time.deltaTime, Space.World);
        if (Time.time >= nextJumpTime)
        {
            rb.velocity = Vector3.up * jumpForce;
            nextJumpTime = Time.time + attackCooldown;
        }
    }

    private void AttackPlayer()
    {
        if (Time.time >= nextAttackTime)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Destroy(gameObject, 2f);
    }
}
