using UnityEngine;

public abstract class EnemyAbility : MonoBehaviour, IEnemyAbility
{
    protected Enemy enemy;

    public virtual void Initialize(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public abstract void UpdateAbility();
    public virtual void OnDeath() { }
}
