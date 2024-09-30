using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DeepCopier
{
    public static T DeepCopy<T>(this T source)
    {
        if (!typeof(T).IsSerializable)
        {
            Debug.Log("NotSerializable");
        }

        if (source is null) return default;

        using var stream = new MemoryStream();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, source);
        stream.Seek(0, SeekOrigin.Begin);
        return (T)formatter.Deserialize(stream);
    }
}
