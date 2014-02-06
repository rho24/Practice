using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Practice
{
    [TestFixture]
    public class CalculatorNewTests
    {
        [Test]
        public void ShouldDefaultToZero() {
            var calc = Calculator.New();

            Assert.That(calc.Result(), Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnArgument() {
            var calc = Calculator.New(5);

            Assert.That(calc.Result(), Is.EqualTo(5));
        }
    }

    [TestFixture]
    public class EquationTests
    {
        [Test]
        public void ShouldAddValue()
        {
            var eq = Calculator.New(2);

            var added = eq.Add(2);

            Assert.That(added.Result(), Is.EqualTo(4));
        }

        [Test]
        public void ShouldSubtractValue()
        {
            var eq = Calculator.New(2);

            var added = eq.Subtract(1);

            Assert.That(added.Result(), Is.EqualTo(1));
        }

        [Test]
        public void AddShouldUndo()
        {
            var eq = Calculator.New(2);

            var second = eq.Add(45);

            var eq2 = second.Undo();

            Assert.That(eq2, Is.EqualTo(eq));
        }

        [Test]
        public void SubtractShouldUndo()
        {
            var eq = Calculator.New(2);

            var second = eq.Subtract(45);

            var eq2 = second.Undo();

            Assert.That(eq2, Is.EqualTo(eq));
        }

        [Test]
        public void ShouldThrowWhenCantUndo() {
            var eq = Calculator.New();

            Assert.Throws<InvalidOperationException>(() => eq.Undo(), "Cannot undo");
        }
    }

    public static class Calculator
    {
        public static Equation New() {
            return new Equation();
        }

        public static Equation New(int value) {
            return new Equation(value);
        }
    }

    public class Equation
    {
        readonly int _value;
        readonly Equation _previousEquation;

        public Equation(int value, Equation previousEquation) {
            _value = value;
            _previousEquation = previousEquation;
        }

        public Equation(int value) : this(value, null) {}

        public Equation() : this(0) {}

        public int Result() {
            return _value;
        }

        public Equation Add(int value) {
            return new Equation(_value + value, this);
        }

        public Equation Subtract(int value) {
            return new Equation(_value - value, this);
        }

        public Equation Undo() {
            if(_previousEquation == null)
                throw new InvalidOperationException("Cannot undo");
            return _previousEquation;
        }
    }
}
