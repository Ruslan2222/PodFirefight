using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class RedEnemy : MonoBehaviour
{
    [Header("Health")]
    [Space]
    [SerializeField] private HealthBar _healthBar;
    private int _maxHealth = 100;
    private int _currentHealth;

    [Header("Particle")]
    [Space]
    [SerializeField] private ParticleSystem _selfdestruction;
    [SerializeField] private ParticleSystem _death;
    [SerializeField] private ParticleSystem _blood;

    [Header("Move Speed")]
    [SerializeField] private float _moveSpeed;

    private Player _player;
    private UI _uiScript;

    private NavMeshAgent _navAgent;
    private Vector3 _playerTransform;
    private bool _attack;

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
        StartCoroutine(Attack());
    }

    private void Update()
    {
        if (_player.gameObject.activeSelf)
        {
            _playerTransform = _player.gameObject.transform.position;
        }

        if (_attack)
        {
            transform.DOLookAt(_playerTransform, 0.1f);
            _navAgent.SetDestination(_playerTransform);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(15);
            Instantiate(_selfdestruction, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        Instantiate(_blood, transform.position, Quaternion.identity);
        _currentHealth -= damage;
        _healthBar.SetHealtlh(_currentHealth);

        if (_currentHealth <= 0)
        {
            _player.AddPower(15);
            _uiScript.redKilled += 1;
            Instantiate(_death, transform.position, Quaternion.identity);
            DOTween.KillAll();
            Destroy(gameObject);
        }
    }

    private IEnumerator Attack()
    {
        transform.DOMove(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), 0.5f);
        yield return new WaitForSeconds(2f);
        transform.DOMove(_playerTransform, 2f);
        yield return new WaitForSeconds(0.5f);
        _navAgent.enabled = true;
        _attack = true;
    }


}
