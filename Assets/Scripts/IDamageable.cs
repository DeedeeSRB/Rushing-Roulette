public interface IDamageable<T>
{
    public T Health { get; set; }
    void Damage(T damage);
}
