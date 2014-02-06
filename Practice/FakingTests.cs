using FakeItEasy;
using NUnit.Framework;

namespace Practice
{
    [TestFixture]
    public class FakingTests
    {
        [Test]
        public void ShouldReturn() {
            var f = A.Fake<IExample>();

            A.CallTo(() => f.Method()).Returns("test");

            Assert.That(f.Method(), Is.EqualTo("test"));
        }

        [Test]
        public void ShouldPropertyGet() {
            var f = A.Fake<IExample>();

            A.CallTo(() => f.Prop).Returns(5);

            Assert.That(f.Prop, Is.EqualTo(5));
        }

        [Test]
        public void ShouldCheckArguments() {
            var f = A.Fake<IExample>();

            A.CallTo(() => f.Method(45)).Returns("correct");

            A.CallTo(() => f.Method(2)).Returns(null);

            Assert.That(f.Method(45), Is.EqualTo("correct"));
            Assert.That(f.Method(2), Is.Null);
        }

    }

    public interface IExample
    {
        string Method();

        string Method(double value);

        int Prop { get; set; }
    }
}