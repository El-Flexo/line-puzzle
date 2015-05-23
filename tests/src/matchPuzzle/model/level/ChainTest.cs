using NUnit.Framework;
using matchPuzzle.MVCS.model.level;

namespace matchPuzzle.model.level {
    [TestFixture]
    public class ChainTest
    {
        IChain chain;

        [SetUp]
        public void Setup()
        {
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
            Assert.That(chain.CanPin(new Point(3, 0)), Is.EqualTo(true));   // can't pin, already chaned, >= 2 step
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
    }
}