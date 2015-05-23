using JsonFx.Json;
using System;
using System.Collections.Generic;
using matchPuzzle.utils;

namespace matchPuzzle.MVCS.model.level.provider
{
    public class DefLevelProvider : ILevelProvider
    {
        int[][] initMap;
        int moves;

        public void SetDef(string def)
        {
            var definition = Json.Parse<Dictionary<String, object>>(def);

            initMap = (int[][]) definition["initMap"];
            moves = (int) definition["moves"];
        }

        public int[][] InitMap {
            get {
                return initMap;
            }
        }

        public int Moves {
            get {
                return moves;
            }
        }
    }
}