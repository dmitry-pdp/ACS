namespace ACS.Models.Test.Utils
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    [TestClass]
    public class RangeConstraintTest
    {
        [TestMethod]
        public void RangeConstraint_Constructor_WithMinMaxValues_AssignValues()
        {
            uint min = 10;
            uint max = 30;
            var constraint = new RangeConstraint<uint>(min, max);
            Assert.AreEqual(min, constraint.MinValue);
            Assert.AreEqual(max, constraint.MaxValue);
        }

        [TestMethod]
        public void RangeConstraint_InRange_WithMinMaxValues_ReturnOrder()
        {
            var constraint = new RangeConstraint<ushort>(10, 100);
            Assert.IsTrue(constraint.InRange(50), "Value should be in range.");
            Assert.IsTrue(constraint.InRange(10), "Value should be in range.");
            Assert.IsTrue(constraint.InRange(100), "Value should be in range.");
            Assert.IsFalse(constraint.InRange(9), "Value should be in range.");
            Assert.IsFalse(constraint.InRange(101), "Value should be in range.");
        }
    }
}
