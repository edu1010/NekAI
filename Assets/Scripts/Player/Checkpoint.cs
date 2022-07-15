using UnityEngine;

[RequireComponent(typeof(CollisionDetector2D))]
public class Checkpoint : MonoBehaviour
{
    private CollisionDetector2D _playerDetector;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        _playerDetector = GetComponent<CollisionDetector2D>();
    }

    private void OnEnable()
    {
        _playerDetector.OnCollisionEnter += AddCheckpoint;
    }

    private void OnDisable()
    {
        _playerDetector.OnCollisionEnter -= AddCheckpoint;
    }

    private void AddCheckpoint(Collider2D collision)
    {
        var playerHealthSystem = collision.gameObject.GetComponent<PlayerHealthSystem>();
        if (playerHealthSystem is { }) {
            playerHealthSystem.CheckPoint = transform;
            AudioManager.PlaySFX("checkpoint");
            }

        animator.SetBool("isActivated", true);
    }
}