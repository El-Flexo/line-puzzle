using NUnit.Framework;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.provider;
using matchPuzzle.model.level.provider;
using tests.utils;

namespace matchPuzzle.model.level
{
    [TestFixture]
    public class LevelModelTest
    {
        ILevelModel level;

        [SetUp]
        public void Setup()
        {
            DefLevelProviderTest.CreateProvider("level_model_test_common.json");
        }

        [TestCase]
        public void ThrowOnExecuteNotFullChain()
        {
            var chain = new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2)
            };
            Assert.That(() => level.Execute(chain), Throws.Exception);
        }

        [TestCase]
        public void ExecuteChainCorrectly()
        {
            var chain = new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2),
                new Point(3, 3)
            };
            level.Execute(chain);
            Assert.That(level.MovesLast, Is.EqualTo(2));
        }
    }
}