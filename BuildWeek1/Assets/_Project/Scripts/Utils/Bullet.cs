using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _bulletTime;
    [SerializeField] private bool _isHoming = false;
    private Transform _target;
    
    private Rigidbody2D _rb;
    private float _bornTime;

    private void Awake()
    {
        // Controllo del RigidBody2D, applicandone uno in caso di assenza
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f; 
        }
    }

    private void OnEnable()
    {
        _bornTime = Time.time; // Salva il tempo di nascita del proiettile
    }

    // Metodo per configurare il proiettile subito dopo l'istanza
    public void SetUp(Vector2 direction, int damage, Transform target = null, bool isHoming = false) 
    {
        _damage = damage;
        _isHoming = isHoming;
        _target = target;

        if (_isHoming && target != null)
        {
            _rb.velocity = Vector2.zero; // velocità iniziale azzerata, il movimento sarà gestito in FixedUpdate
        }
        else
        {
            _rb.velocity = direction.normalized * _speed; // Proiettile normale parte subito nella direzione data
        }
    }

    private void FixedUpdate() // Aggiornamento fisico per gestire il movimento homing
    {
        if (_isHoming)
        {
            if (_target == null) // Se il target viene distrutto prima che il proiettile arrivi, distrugge anche il proiettile
            {
                Destroy(gameObject);
                return;
            }
            Vector2 dir = ((Vector2)_target.position - _rb.position).normalized; // Calcola la direzione verso il target
            _rb.velocity = dir * _speed; // Muove il proiettile verso il target
        }
    }

    private void Update() // Controlla se il proiettile ha superato la sua durata massima e lo distrugge
    {
        if (Time.time > _bornTime + _bulletTime)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D (Collision2D collision) // Gestisce le collisioni con altri oggetti
    {
        LifeController life = collision.gameObject.GetComponent<LifeController>();
        if (life != null)
        {
            life.TakeDamage(_damage); // applico danno
        }

        // distrugge comunque il proiettile
        Destroy(gameObject);
    }
    public int GetDamage() => _damage;
}
