using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level.chain;
namespace matchPuzzle.MVCS.model.level.chain
{
    public class RandomElementGenerator : IElementGenerator
    {
        [Inject]
        public RandomProxy random {
            get;
            set;
        }

        public ElementType GetNext()
        {
            return (ElementType)random.Get(0, 3);
        }
    }
}