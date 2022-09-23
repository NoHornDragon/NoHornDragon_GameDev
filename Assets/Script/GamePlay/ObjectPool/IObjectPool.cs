namespace NHD.GamePlay.ObjectPool
{
    public interface IObjectPool
    {
        IPoolableObject GetObjectFromPool();
        void ReturnObjectToPool(IPoolableObject obj);
        void SupplyObjectPool();
    }
}