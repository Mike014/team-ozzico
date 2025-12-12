using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAnimationHandler : MonoBehaviour
{
    [SerializeField] private string _verticalSpeedName = "vSpeed";
    [SerializeField] private string _horizontalSpeedName = "hSpeed";
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void SetVerticalSpeed(float speed)
    {
        _animator.SetFloat(_verticalSpeedName, speed);
    }

    private void SetHorizontalSpeed(float speed)
    {
        _animator.SetFloat(_horizontalSpeedName, speed);
    }

    public void MovementAnimation(Vector2 speed)
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            SetVerticalSpeed(speed.y);
            SetHorizontalSpeed(speed.x);
        }
    }

    public void PlayDamageAnimation()
    {
        _animator.SetBool("isDamaged", true);
    }

    public void StopDamageAnimation()
    {
        _animator.SetBool("isDamaged", false);
    }

    public void DeathAnimation()
    {
        _animator.SetBool("isDead", true);
    }

    public void DeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}
