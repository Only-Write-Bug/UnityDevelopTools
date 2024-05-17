
public class ToolBase<T> where T : class, new()
{
    private static T _instance = null;
    public static T Instance => _instance ??= new T();
    
    protected ToolBase() {}
}