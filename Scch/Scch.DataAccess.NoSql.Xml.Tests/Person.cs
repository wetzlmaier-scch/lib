using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using Scch.Common.Xml.Serialization;
using Scch.DomainModel.NoSql.Xml;

namespace Scch.DataAccess.NoSql.Xml.Tests;

[Serializable]
//[XmlInclude(typeof(Bitmap))]
public class Person : XmlNoSqlEntity
{
    public Person()
    {
        Address = new Address();
        Children = new List<Person>();
    }

    public string Name { get; set; }
    public int Age { get; set; }

    public Address Address { get; set; }

    [XmlIgnore]
    public Image Image { get; set; }

    /// <summary>
    /// Serialized form of <see cref="Image"/>. The XmlSerializer of .NET does not apply the implicit conversion
    /// operators of <see cref="ImageSerializer"/>, so the surrogate needs a property of its own.
    /// </summary>
    [XmlElement("Image")]
    public ImageSerializer ImageXml
    {
        get { return Image; }
        set { Image = value; }
    }

    public List<Person> Children { get; set; }
}
