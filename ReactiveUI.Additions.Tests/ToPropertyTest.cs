using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reactive.Subjects;

namespace ReactiveUI.Additions.Tests
{
    [TestClass]
    public class ToPropertyHelpers
    {
        /// <summary>
        /// Simple property, test that the initial value works.
        /// </summary>
        [TestMethod]
        public void SimplePropertyInitialValue()
        {
            var s = new Subject<int>();
            var bc = s.ToProperty();
            Assert.AreEqual(0, bc.Value, "Initial value");
        }

        /// <summary>
        /// Explicity set an initial value
        /// </summary>
        [TestMethod]
        public void SimplePropertySetInitialValue()
        {
            var s = new Subject<int>();
            var bc = s.ToProperty(10);
            Assert.AreEqual(10, bc.Value, "Initial value");
        }

        /// <summary>
        /// Send a good value down the pike.
        /// </summary>
        [TestMethod]
        public void SimplePropertySendGoodValues()
        {
            var s = new Subject<int>();
            var bc = s.ToProperty();
            s.OnNext(2);
            Assert.AreEqual(2, bc.Value, "First Set Value");
        }

        /// <summary>
        /// Send a good value down the pike, and then terminate the sequence.
        /// </summary>
        [TestMethod]
        public void SimplePropertySendGoodValueAndDone()
        {
            var s = new Subject<int>();
            var bc = s.ToProperty();
            s.OnNext(2);
            s.OnCompleted();
            Assert.AreEqual(2, bc.Value, "First Set Value");
        }

        /// <summary>
        /// An exception should not disturb the resulting values.
        /// It should be swallowed quietly. And the property is reset
        /// to its default value.
        /// </summary>
        [TestMethod]
        public void SimplePropertySendException()
        {
            var s = new Subject<int>();
            var bc = s.ToProperty();
            s.OnNext(2);
            s.OnError(new InvalidOperationException());
            Assert.AreEqual(0, bc.Value, "First Set Value");
        }
    }
}
