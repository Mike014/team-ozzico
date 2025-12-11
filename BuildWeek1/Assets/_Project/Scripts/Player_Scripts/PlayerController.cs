using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TopDownMover2D mover;
    private PlayerAnimationHandler _animController;
    Vector2 input;

    void Awake()
    {
        mover = GetComponent<TopDownMover2D>();
        _animController = GetComponentInChildren<PlayerAnimationHandler>();
    }

    void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mover.SetInputNormalized(input);
    }

    void Update()
    {
        _animController.MovementAnimation(input);
    }
}