namespace NHD.GamePlay.ObjectPool
{
    public interface IObjectPool
    {
        PoolableObjectBase GetObjectFromPool();
        void InsertObjectToPool(PoolableObjectBase obj);
        void SupplyObjectPool();
    }
}