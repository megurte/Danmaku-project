namespace Enemy
{
    public interface IDamageable
    {
        void OnTakingDamage(float damage, int enemyID);
    }
}