namespace Sergi.Pooling {
    public interface IPool<T> {
        T GetInstance();
    }
}