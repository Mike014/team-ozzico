using UnityEngine;

public class TopDownMover2D : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Rigidbody2D rb;
    private Vector2 dir;

    public void SetInput(Vector2 dir)
    {
        this.dir = dir;
    }

    public void SetInputNormalized(Vector2 dir)
    {
        SetInput(dir.normalized);
    }

    void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * (speed * Time.fixedDeltaTime));
    }
}