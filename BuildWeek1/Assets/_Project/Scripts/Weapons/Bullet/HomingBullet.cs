using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField] public float speed = 7f;
    [SerializeField] private int damage;
    [SerializeField] private float _bulletTime;
    private Rigidbody2D rb;
    private Transform target;

    public void Init(Transform t, int dmg)
    {
        target = t;
        damage = dmg;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (target == null) { Destroy(gameObject); return; }
        Vector2 dir = ((Vector2)target.position - rb.position).normalized;
        rb.velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var life = collision.gameObject.GetComponent<LifeController>();
        if (life != null) life.TakeDamage(damage);
        Destroy(gameObject);
    }
}
