using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface ISerializer : IuvoInterfaceBase
        {
            string Serialize<T>(T data);
            T Deserialize<T>(string data);
        }

        public interface IJsonSerializer : ISerializer { }

        public interface IStringSerializer : ISerializer { }

        public interface IBinarySerializer : ISerializer { }

        public interface IEncryptedSerializer : IStringSerializer { }
    }
}
