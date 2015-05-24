using FluentAssertions;
using NUnit.Framework;
using System;
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
            var provider = DefLevelProviderTest.CreateProvider("level_model_test_common.json");
            var level = new LevelModel();
            level.provider = provider;
            this.level = level;
        }

        [TestCase]
        public void ThrowOnExecuteNotFullChain()
        {
            var chain = new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2)
            };

            level.CanExecute(chain).Should().Be(false, "Chain length less than expected");

            Action exec = () => level.Execute(chain);
            exec.ShouldThrow<ArgumentException>("Chain length less than expected")
            .WithMessage("Required chain length > 4, executable chain length: 3", "");
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

            var expectedMap = new int[][] {
                new int[] {0, 1, 3, 0},
                new int[] {0, 1, 3, 0},
                new int[] {0, 1, 3, 0},
                new int[] {1, 1, 1, 0},
                new int[] {1, 1, 3, 1}
            };
            level.Map.Should().BeSameAs(expectedMap, "Execute chain and fill new elemets");

            Assert.That(level.MovesLast, Is.EqualTo(2));
        }
    }
}