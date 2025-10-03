using System.Xml.Serialization;

namespace WebApplication1.Models
{
    public class GameData
    {
        [XmlAttribute("X")]
        public int X { get; set; } = 0;
        [XmlAttribute("Y")]
        public int Y { get; set; } = 0;
    }
}
