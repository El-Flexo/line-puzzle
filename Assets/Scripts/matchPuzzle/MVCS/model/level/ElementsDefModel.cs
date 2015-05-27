using System.Collections.Generic;
using UnityEngine;
using matchPuzzle.utils;

namespace matchPuzzle.MVCS.model.level
{
    public class ElementsDefModel : IElementsDefModel
    {
        Dictionary<string, string> elements;

        [PostConstruct]
        public void Construct()
        {
            var elementsSource = Resources.Load<TextAsset>("defs/elements");
            elements = Json.Parse<Dictionary<string, string>>(elementsSource.text);
        }

        public string GetTextureId(ElementType element)
        {
            var elementId = (int)element;
            return elements[elementId.ToString()];
        }
    }

    public interface IElementsDefModel
    {
        string GetTextureId(ElementType element);
    }
}