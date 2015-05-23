using NUnit.Framework;
using System;
using matchPuzzle.MVCS.model.level.provider;
using tests.utils;

namespace matchPuzzle.model.level.provider
{
    [TestFixture]
    public class DefLevelProviderTest
    {
        [TestCase]
        public void LoadDataCorrectly()
        {
            var provider = CreateProvider("provider_test_common.json");

            var expected = new int[][] {
                new int[] {0, 1, 3, 1},
                new int[] {0, 1, 3, 1},
                new int[] {0, 1, 3, 1},
                new int[] {1, 1, 1, 1},
                new int[] {1, 1, 3, 1}
            };

            Assert.That(provider.InitMap, Is.EqualTo(expected));
        }

        [TestCase]
        public void ThrowOnInconsistentData()
        {
            Assert.That(() => CreateProvider("provider_test_broken.json"), Throws.Exception);
            Assert.That(() => CreateProvider("provider_test_invalid.json"), Throws.Exception);
        }

        public static ILevelProvider CreateProvider(string dataPath)
        {
            var defProvider = new DefLevelProvider();
            defProvider.SetDef(TestResources.Get(dataPath));
            return defProvider;
        }
    }
}