namespace ViteCent.Core.Register;

/// <summary>
/// </summary>
public class BaseService
{
    /// <summary>
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static ServiceConfig? GetServiceRandom(List<ServiceConfig> list)
    {
        if (list.Count == 0) return null;

        if (list.Count == 1) return list?.FirstOrDefault();

        var random = new Random();

        var index = random.Next(0, list.Count);

        return list[index];
    }
}