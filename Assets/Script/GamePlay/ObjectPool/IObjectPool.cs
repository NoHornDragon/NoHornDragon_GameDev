namespace NHD.GamePlay.ObjectPool
{
    public interface IObjectPool
    {
        PoolableObjectBase GetObjectFromPool();
        void ReturnObjectToPool(PoolableObjectBase obj);
        void SupplyObjectPool();
    }
}