using UnityEngine;

public class EnemyLifeController : MonoBehaviour
{
    [SerializeField] private int hp;
    private LifeController lifeController;

    private void Awake()
    {
        lifeController = GetComponent<LifeController>();
    }

    private void Start()
    {
        lifeController.SetHp(hp);
    }

    public bool IsAlive() => lifeController.IsAlive();
}
