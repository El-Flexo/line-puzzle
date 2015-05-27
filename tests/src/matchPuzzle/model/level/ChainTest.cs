using NUnit.Framework;
using System;
using System.Collections.Generic;
using matchPuzzle.MVCS.controller.signal;
using matchPuzzle.MVCS.model;
using matchPuzzle.MVCS.model.level;
using matchPuzzle.MVCS.model.level.chain;
using matchPuzzle.model.level.provider;

namespace matchPuzzle.model.level {

    [TestFixture]
    public class ChainTest
    {
        IChainModel chain;

        [SetUp]
        public void Setup()
        {
            var level = LevelModelTest.CreateLevel("level_model_test_common.json");
            var chain = new ChainModel();
            chain.level = level;
            chain.pinElement = new PinElementSignal();
            chain.unpinElement = new UnpinElementSignal();
            this.chain = chain;
        }

        [TestCase]
        public void PinChainCorrectly4parts()
        {
            chain.Pin(new Point(3, 0));
            chain.Pin(new Point(3, 1));
            chain.Pin(new Point(3, 2));
            chain.Pin(new Point(3, 3));

            var expectedChain = new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2),
                new Point(3, 3)
            };

            Assert.That(chain.Value, Is.EqualTo(expectedChain));
        }

        [TestCase]
        public void CanPin()
        {
            Assert.That(chain.CanPin(new Point(3, 0)), Is.EqualTo(true));   // first item
            chain.Pin(new Point(3, 0));

            Assert.That(chain.CanPin(new Point(3, 1)), Is.EqualTo(true));   // same type, neighboring
            Assert.That(chain.CanPin(new Point(3, 2)), Is.EqualTo(false));  // same type, but not neighboring
            Assert.That(chain.CanPin(new Point(2, 0)), Is.EqualTo(false));  // different type, neighboring
            Assert.That(chain.CanPin(new Point(0, 0)), Is.EqualTo(false));  // different type, not neighboring

            chain.Pin(new Point(3, 1));                                     // explore new neighbors
            Assert.That(chain.CanPin(new Point(3, 2)), Is.EqualTo(true));   // same type, neighboring

            Assert.That(chain.CanPin(new Point(3, 0)), Is.EqualTo(true));   // can pin, already chaned, < 2 step

            chain.Pin(new Point(3, 2));
            Assert.That(chain.CanPin(new Point(3, 0)), Is.EqualTo(true));   // can pin, already chaned, < 2 step

            chain.Pin(new Point(3, 3));
            Assert.That(chain.CanPin(new Point(3, 0)), Is.EqualTo(false));   // can't pin, already chaned, >= 2 step
        }

        [TestCase]
        public void CropTail()
        {
            chain.Pin(new Point(3, 0));
            chain.Pin(new Point(3, 1));
            chain.Pin(new Point(3, 2));
            chain.Pin(new Point(3, 3));

            chain.Pin(new Point(3, 2));

            Assert.That(chain.Value, Is.EqualTo(new Point[]{
                new Point(3, 0),
                new Point(3, 1),
                new Point(3, 2)
            }));

            chain.Pin(new Point(3, 3));
            chain.Pin(new Point(3, 1));

            Assert.That(chain.Value, Is.EqualTo(new Point[]{
                new Point(3, 0),
                new Point(3, 1)
            }));
        }

        [TestCase]
        public void Clean()
        {
            chain.Pin(new Point(3, 0));
            chain.Pin(new Point(3, 1));

            Assert.That(chain.Value, Is.EqualTo(new Point[]{
                new Point(3, 0),
                new Point(3, 1)
            }));

            chain.Clean();

            Assert.That(chain.Value, Is.EqualTo(new Point[]{
            }));
        }

        [TestCase]
        public void ThrowOnPinDifferentTypeParts()
        {
            chain.Pin(new Point(3, 0));

            Assert.That(() => chain.Pin(new Point(2, 0)), Throws.Exception);
        }

        [TestCase]
        public void ThrowOnPinToNotNeighboringPart()
        {
            chain.Pin(new Point(3, 0));

            Assert.That(() => chain.Pin(new Point(3, 2)), Throws.Exception);
        }

        [TestCase]
        public void ThrowOnPinToFarChainedPart()
        {
            chain.Pin(new Point(3, 0));
            chain.Pin(new Point(3, 1));
            chain.Pin(new Point(3, 2));
            chain.Pin(new Point(3, 3));

            Assert.That(() => chain.Pin(new Point(3, 0)), Throws.Exception);
        }

        [TestCase]
        public void pinElementSignal()
        {
            var chain = (ChainModel) this.chain;
            var poins = new List<Point>();
            chain.pinElement.AddListener((point) => poins.Add(point));

            chain.Pin(new Point(3, 0));
            Assert.That(poins, Is.EqualTo(new List<Point>{
                new Point(3, 0)
            }));

            chain.Pin(new Point(3, 1));
            Assert.That(poins, Is.EqualTo(new List<Point>{
                new Point(3, 0),
                new Point(3, 1)
            }));
        }

        [TestCase]
        public void unpinElementSignal()
        {
            var chain = (ChainModel) this.chain;
            var poins = new List<Point>();
            chain.unpinElement.AddListener((point) => poins.Add(point));

            chain.Pin(new Point(3, 0));
            chain.Pin(new Point(3, 1));
            chain.Pin(new Point(3, 2));
            chain.Pin(new Point(3, 3));
            chain.Pin(new Point(3, 2));
            Assert.That(poins, Is.EqualTo(new List<Point>{
                new Point(3, 3)
            }));
        }
    }
}