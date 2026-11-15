using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Scch.ModelBasedTesting.Tests;

[TestClass]
public class DomainTest
{
    private ModelWrapper _wrapper;

    [TestInitialize]
    public void Setup()
    {
        _wrapper = new ModelWrapper(typeof(ModelDomain));
        ModelDomain.Reset();
    }

    [TestMethod]
    public void TestDomainMissing()
    {
        ThrowsExactly<InvalidOperationException>(() => new ModelWrapper(typeof(DomainMissingModel)));
    }

    private static class DomainMissingModel
    {
        [Action]
        public static void MissingDomain([Domain("number")] int number, KnownColor color)
        {
        }
    }

    [TestMethod]
    public void TestDomainNames()
    {
        var domainNames = _wrapper.DomainNames("DomainAction");

        Assert.AreEqual(2, domainNames.Count);
        Assert.IsTrue(domainNames.Contains("number"));
        Assert.IsTrue(domainNames.Contains("color"));
    }

    [TestMethod]
    public void TestDomainNamesException()
    {
        ThrowsExactly<ArgumentException>(() =>
        {
            var domainNames = _wrapper.DomainNames("x");
            Assert.AreEqual(2, domainNames.Count);
            Assert.IsTrue(domainNames.Contains("number"));
            Assert.IsTrue(domainNames.Contains("color"));
        });
    }

    [TestMethod]
    public void TestDomainName()
    {
        Assert.AreEqual("number", _wrapper.DomainName("DomainAction", 0));
        Assert.AreEqual("color", _wrapper.DomainName("DomainAction", 1));
    }

    [TestMethod]
    public void TestDomainNameException()
    {
        ThrowsExactly<ArgumentOutOfRangeException>(() => _wrapper.DomainName("DomainAction", -1));
    }

    [TestMethod]
    public void TestDomainNameException2()
    {
        ThrowsExactly<ArgumentOutOfRangeException>(() => _wrapper.DomainName("DomainAction", 2));
    }

    [TestMethod]
    public void TestDomainValue()
    {
        Assert.AreEqual("NumberDomain()", _wrapper.DomainCode("DomainAction", 0));
        Assert.AreEqual(1, _wrapper.DomainValue("DomainAction", 0, 0));
        Assert.AreEqual(3, _wrapper.DomainValue("DomainAction", 0, 1));
        Assert.AreEqual(5, _wrapper.DomainValue("DomainAction", 0, 2));
        Assert.AreEqual(7, _wrapper.DomainValue("DomainAction", 0, 3));

        Assert.AreEqual("ColorDomain", _wrapper.DomainCode("DomainAction", 1));
        Assert.AreEqual(KnownColor.Red, _wrapper.DomainValue("DomainAction", 1, 0));
        Assert.AreEqual(KnownColor.Green, _wrapper.DomainValue("DomainAction", 1, 1));
        Assert.AreEqual(KnownColor.Blue, _wrapper.DomainValue("DomainAction", 1, 2));
    }

    [TestMethod]
    public void TestDomainValueException()
    {
        ThrowsExactly<ArgumentOutOfRangeException>(() => _wrapper.DomainValue("DomainAction", 0, -1));
    }

    [TestMethod]
    public void TestDomainValueException2()
    {
        ThrowsExactly<ArgumentOutOfRangeException>(() => _wrapper.DomainValue("DomainAction", 0, 4));
    }

    [TestMethod]
    public void TestPerformActionException()
    {
        ThrowsExactly<InvalidOperationException>(() => _wrapper.PerformAction("DomainAction"));
    }

    [TestMethod]
    public void TestPerformAction()
    {
        _wrapper.PerformAction("DomainAction", 1, 1);
        Assert.AreEqual(1, ModelDomain.CalledActions.Count);
        Assert.AreEqual(3, ModelDomain.CalledActions[0].Item1);
        Assert.AreEqual(KnownColor.Green, ModelDomain.CalledActions[0].Item2);
    }
}
