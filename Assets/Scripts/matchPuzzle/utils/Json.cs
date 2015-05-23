using JsonFx.Json;

namespace matchPuzzle.utils
{
    public static class Json
    {
        public static T Parse<T>(string source) where T: class
        {
            return new JsonReader(source).Deserialize<T>();
        }
    }
}