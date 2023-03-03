using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Speed")]
    [SerializeField] private float _bulletSpeed;

    private Rigidbody _rigidbody;
    private Player _player;
    public int maxRicochets = 3;
    private int ricochetCount = 0;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = transform.forward * _bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out BlueEnemy blueEnemy))
        {
            blueEnemy.TakeDamage(100);

            if (ricochetCount > 1)
            {
                _player.AddHealth(50);
                _player.AddPower(10);
            }
        }
        else if (collision.gameObject.TryGetComponent(out RedEnemy redEnemy))
        {
            redEnemy.TakeDamage(100);

            if (ricochetCount > 1)
            {
                _player.AddHealth(50);
                _player.AddPower(10);
            }
        }
        else if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(10);
        }

        if (ricochetCount < maxRicochets)
        {
            Vector3 newDirection = CalculateRicochetDirection(collision.contacts[0].point, _rigidbody.velocity, collision.contacts[0].normal);
            var ricoshet = _rigidbody.velocity = newDirection.normalized * _bulletSpeed;
            if (collision.gameObject.tag == "RicochetSurface")
            {
                _rigidbody.velocity = ricoshet;
                ricochetCount++;
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                float rand = Random.value;

                if (rand <= 0.2f || _player.currentHealth < rand * 100)
                {
                    if (rand <= 0.1f)
                    {
                        _rigidbody.velocity = ricoshet;
                    }
                    else
                    {
                        GameObject nearestEnemy = FindNearestEnemy();
                        if (nearestEnemy != null)
                        {
                            Vector3 enemyDirection = (nearestEnemy.transform.position - transform.position).normalized;
                            _rigidbody.velocity = enemyDirection * _bulletSpeed;
                        }
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }

            Destroy(gameObject, 2f);

        }
        else
        {
            Destroy(gameObject);
        }

    }

    private Vector3 CalculateRicochetDirection(Vector3 hitPoint, Vector3 currentDirection, Vector3 hitNormal)
    {
        RaycastHit hit;
        if (Physics.Raycast(hitPoint, currentDirection, out hit))
        {
            return Vector3.Reflect(currentDirection, hit.normal);
        }
        return currentDirection;
    }

    private GameObject FindNearestEnemy()
    {
        float distanceToClose = Mathf.Infinity;
        GameObject nearestEnemy = null;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length >= 0)
        {
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < distanceToClose)
                {
                    distanceToClose = distance;
                    nearestEnemy = enemy;
                }
            }
            return nearestEnemy;
        }
        return null;
    }

}