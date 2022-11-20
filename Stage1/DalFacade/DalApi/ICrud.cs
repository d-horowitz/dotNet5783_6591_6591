namespace DalApi;
public interface ICrud<T>
{
    public int Add(T item);
    public T Read(int id);
    public IEnumerable<T> Read();
    public void Delete(int id);
    public void Update(T item);
}
