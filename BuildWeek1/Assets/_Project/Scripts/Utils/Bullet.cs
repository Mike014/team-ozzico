using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _bulletTime;
    private Rigidbody2D _rb;

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
    public void SetUp(Vector2 direction, int damage)
    // Diciamo al proiettile in quale direzione andare
    {
        _rb.velocity = direction.normalized * _speed;
        _damage = damage; // Imposta il danno direttamente
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        // Base LifeController sul gameobject che colpiamo
        LifeController life = collision.gameObject.GetComponent<LifeController>();

        if (life)
        {
            life.TakeDamage(_damage); // Applichiamo danno
            Destroy(gameObject); // Distrugiamo il proiettile
        }
        else
        {
            Destroy(gameObject, _bulletTime);
        }
    }

    public int GetDamage() => _damage;
}
