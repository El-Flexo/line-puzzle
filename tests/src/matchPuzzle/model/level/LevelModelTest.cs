using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using matchPuzzle.MVCS.controller.signal;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
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
            this.level = CreateLevel("level_model_test_common.json");
        }

        [TestCase]
        public void ThrowOnExecuteNotFullChain()
        {
            var chain = new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2)
            };

            level.CanEliminate(chain).Should().Be(false, "Chain length less than expected");

            Action exec = () => level.Eliminate(chain);
            exec.ShouldThrow<ArgumentException>("Chain length less than expected")
            .WithMessage("Required chain length > 4, executable chain length: 3", "");
        }

        [TestCase]
        public void Eliminate()
        {
            var chain = new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2),
                new Point(3, 3)
            };
            level.Eliminate(chain);

            var expectedMap = new int[][] {
                new int[] {0, 1, 3, 1},
                new int[] {0, 1, 3, 2},
                new int[] {0, 1, 3, 0},
                new int[] {1, 1, 1, 0},
                new int[] {1, 1, 3, 1}
            };

            Assert.That(level.Map, Is.EqualTo(expectedMap));
            Assert.That(level.MovesLast, Is.EqualTo(2));
        }

        [TestCase]
        public void EliminateChain()
        {
            var chain = new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2),
                new Point(3, 3)
            };

            var expectedMap = new int[][] {
                new int[] {0, 1, 3, -1},
                new int[] {0, 1, 3, -1},
                new int[] {0, 1, 3, -1},
                new int[] {1, 1, 1, -1},
                new int[] {1, 1, 3, 1}
            };

            var level = (LevelModel) this.level;
            level.EliminateElements(chain);

            Assert.That(level.Map, Is.EqualTo(expectedMap));
        }

        [TestCase]
        public void SquashMap()
        {
            var chain = new Point[]{
                new Point(3, 1),
                new Point(3, 2),
                new Point(3, 3),
                new Point(2, 3),
                new Point(1, 3),
            };

            var expectedMap = new int[][] {
                new int[] {0, -1, -1, -1},
                new int[] {0, 1, 3, -1},
                new int[] {0, 1, 3, -1},
                new int[] {1, 1, 3, 1},
                new int[] {1, 1, 3, 1}
            };

            var level = (LevelModel) this.level;
            level.EliminateElements(chain);
            level.SquashMap();

            Assert.That(level.Map, Is.EqualTo(expectedMap));
        }

        static void PrintMap(int[][] map) {
            for (var y = 0; y < map.Length; y++)
            {
                var str = "[\t";
                for (var x = 0; x < map[y].Length; x++) {
                    var value = map[y][x];

                    str += (value >= 0 ? " " + value : value + "") + ",\t";
                }
                Console.WriteLine(str + " ],");
            }
        }

        static void PrintIndexes(int[][] map) {
            for (var y = 0; y < map.Length; y++)
            {
                var str = "[\t";
                for (var x = 0; x < map[y].Length; x++) {
                    var value = string.Format("[{0}, {1}]", x, y);

                    str += value + ",\t";
                }
                Console.WriteLine(str + " ],");
            }
        }

        public static ILevelModel CreateLevel(string dataPath)
        {
            var provider = DefLevelProviderTest.CreateProvider(dataPath);
            var generator = new RandomElementGenerator();
            generator.random = new RandomProxy(123);

            var level = new LevelModel();
            level.provider = provider;
            level.generator = generator;
            level.eliminateElements = new EliminateElementsSignal();
            level.moveElements = new MoveElementsSignal();
            level.addElements = new AddElementsSignal();
            level.Construct();
            return level;
        }
    }
}