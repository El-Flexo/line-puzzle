using JsonFx.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using matchPuzzle.utils;

namespace matchPuzzle.MVCS.model.level.provider
{
    public class DefLevelProvider : ILevelProvider
    {
        int[][] initMap;

        public void SetDef(string def)
        {
            var definition = Json.Parse<Dictionary<String, object>>(def);

            initMap = (int[][])definition["initMap"];
            Moves = (int)definition["moves"];
            RequiredScore = (int)definition["requiredScore"];
            Name = (string)definition["name"];
            View = (string)definition["view"];
        }

        public int[][] InitMap
        {
            get {
                return DeepCopy<int[][]>(initMap);
            }
        }

        public int Moves
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public int RequiredScore
        {
            get;
            private set;
        }

        public string View
        {
            get;
            private set;
        }

        public static T DeepCopy<T>(object objectToCopy)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, objectToCopy);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}