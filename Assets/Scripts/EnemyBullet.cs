using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Speed")]
    [SerializeField] private float _bulletSpeed;

    private Transform _playerTransform;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (gameObject.activeSelf && _playerTransform.gameObject.activeInHierarchy)
        {
            transform.DOMove(_playerTransform.position, 1f).SetEase(Ease.Linear);
            transform.DOLookAt(_playerTransform.position, 0.1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakePower(25);
        }
        else if (collision.gameObject.TryGetComponent(out BlueEnemy blueEnemy))
        {
            blueEnemy.TakeDamage(100);
        }
        else if (collision.gameObject.TryGetComponent(out RedEnemy redEnemy))
        {
            redEnemy.TakeDamage(100);
        }


        DOTween.KillAll();
        Destroy(gameObject);
    }
}
