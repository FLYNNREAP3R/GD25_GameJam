public interface IEnemyAbility
{
    void Initialize(Enemy enemy);
    void UpdateAbility();
    void OnDeath();  // opcional para habilidades como explotar al morir
}
