using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TopDownMover2D mover;

    void Awake()
    {
        mover = GetComponent<TopDownMover2D>();
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mover.SetInputNormalized(input);
    }
}
