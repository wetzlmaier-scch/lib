using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scch.Common.Collections.Generic;

namespace Scch.Common.Tests.Runtime.Serialization;

/// <summary>
/// Summary description for UnitTest1
/// </summary>
[TestClass]
public class SerializableListTest
{
    public SerializableListTest()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
        get
        {
            return testContextInstance;
        }
        set
        {
            testContextInstance = value;
        }
    }

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    [TestInitialize()]
    public void TestInitialize()
    {
        _list = new StringList();
    }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    private SerializableList<string> _list;

    [Serializable]
    class StringList : SerializableList<string>
    {
        public StringList() { }
        protected StringList(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

    [TestMethod]
    public void TestSerialization()
    {
        _list.Add("string1");
        _list.Add("string2");
        _list.Add("string3");
        _list.Add("string4");

        // SoapFormatter does not exist on .NET, so the ISerializable contract is exercised directly:
        // GetObjectData fills a SerializationInfo and the serialization constructor rebuilds the list from it.
#pragma warning disable SYSLIB0050 // formatter-based serialization infrastructure is obsolete
        var info = new SerializationInfo(typeof(StringList), new FormatterConverter());
#pragma warning restore SYSLIB0050
        var context = default(StreamingContext);

        _list.GetObjectData(info, context);

        var list = (StringList)Activator.CreateInstance(typeof(StringList),
            BindingFlags.Instance | BindingFlags.NonPublic, null, [info, context], null);

        Assert.AreEqual(_list.Count, list.Count);

        for (int i = 0; i < _list.Count; i++)
            Assert.AreEqual(_list[i], list[i]);
    }
}
