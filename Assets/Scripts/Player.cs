using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private HealthBar _healthBar;
    private int _maxHealth = 100;
    private int _currentHealth;

    [Header("Ultra")]
    [SerializeField] private UltraBar _ultraBar;
    private int _currentUltra;

    [Header("Bullet")]
    [Space]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private float _bulletSpeed;

    [Header("Speed")]
    [SerializeField] private float _speed;

    [Header("Particle")]
    [Space]
    [SerializeField] private ParticleSystem _death;
    [SerializeField] private ParticleSystem _blood;

    private UI _uiScript;

    private Button _hitButton;
    private Button _ultraButton;


    private void Awake()
    {
        _uiScript = FindObjectOfType<UI>();
        _hitButton = _uiScript.hitButton;
        _ultraButton = _uiScript.ultraButton;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        _currentUltra = 50;
        _ultraBar.SetUltra(_currentUltra);
        _hitButton.onClick.AddListener(Shoot);
        _ultraButton.onClick.AddListener(Ultra);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealtlh(_currentHealth);
        Instantiate(_blood, transform.position, Quaternion.identity);

        if (_currentHealth <= 0)
        {
            Instantiate(_death, transform.position, Quaternion.identity);
            Destroy(gameObject);
            _uiScript.FinishGame();
        }
    }

    public void AddHealth(int count)
    {
        _currentHealth += count;
        _healthBar.SetHealtlh(_currentHealth);
    }

    public void AddPower(int count)
    {
        _currentUltra += count;
        _ultraBar.SetUltra(_currentUltra);

        if (_currentUltra >= 100)
        {
            _uiScript.ultraButton.interactable = true;
        }
    }

    public void TakePower(int count)
    {
        Instantiate(_blood, transform.position, Quaternion.identity);
        _currentUltra -= count;
        _ultraBar.SetUltra(_currentUltra);
    }

    private void Shoot()
    {
        Debug.Log("shoot");
        Instantiate(_bullet, _bulletSpawn.position, Quaternion.Euler(0, transform.eulerAngles.y, 0));
    }

    private void Ultra()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }

        _currentUltra = 0;
        _ultraBar.SetUltra(_currentUltra);
    }

}
