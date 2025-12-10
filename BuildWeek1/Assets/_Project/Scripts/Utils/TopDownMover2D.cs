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
        if (dir.sqrMagnitude < 0.01f)   //se il modulo del vettore è minore di 0.01
        {
            SetInput(Vector2.zero);     //non normalizzi e fermi il gameobject
            return;
        }
        SetInput(dir.normalized);       //altrimenti normalizza
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