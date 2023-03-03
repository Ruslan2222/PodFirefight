using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class BlueEnemy : MonoBehaviour
{
    [Header("Health")]
    [Space]
    [SerializeField] private HealthBar _healthBar;
    private int _maxHealth = 100;
    private int _currentHealth;

    [Header("Bullet")]
    [Space]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawn;

    [Header("Move Speed")]
    [SerializeField] private float _moveSpeed;

    [Header("Particle")]
    [Space]
    [SerializeField] private ParticleSystem _death;
    [SerializeField] private ParticleSystem _blood;

    private Player _player;
    private UI _uiScript;
    private NavMeshAgent _navAgent;

    private bool readyToShot;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _uiScript = FindObjectOfType<UI>();
        _navAgent = GetComponent<NavMeshAgent>();
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    private void Start()
    {
        readyToShot = true;
    }


    private void FixedUpdate()
    {
        Vector3 playerTransform = _player.gameObject.transform.position;
        float distance = Vector3.Distance(transform.position, playerTransform);

        if (gameObject.activeSelf)
        {
            transform.DOLookAt(playerTransform, 0.1f);
        }

        if (distance > 3)
        {
            _navAgent.isStopped = false;
            _navAgent.SetDestination(playerTransform);
        }
        else
        {
            _navAgent.isStopped = true;
        }


        if (readyToShot)
        {
            StartCoroutine(Shoot());
            readyToShot = false;
        }

    }

    public void TakeDamage(int damage)
    {
        Instantiate(_blood, transform.position, Quaternion.identity);
        _currentHealth -= damage;
        _healthBar.SetHealtlh(_currentHealth);

        if(_currentHealth <= 0)
        {
            _player.AddPower(50);
            _uiScript.blueKilled += 1;
            Instantiate(_death, transform.position, Quaternion.identity);
            DOTween.KillAll();
            Destroy(gameObject);
        }

    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(_bullet, _bulletSpawn.position, Quaternion.Euler(0, transform.eulerAngles.y, 0));
        yield return new WaitForSeconds(3f);
        readyToShot = true;
    }

}
