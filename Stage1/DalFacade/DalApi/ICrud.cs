namespace DalApi;
public interface ICrud<T>
{
    public int Add(T item);
    //public T Read(int id);
    public T ReadSingle(Func<T, bool> func);
    public IEnumerable<T> Read(Func<T, bool>? func = null);
    public void Delete(int id);
    public void Update(T item);
}
