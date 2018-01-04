using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Assets.scripts.core
{
    public class AIMaker
    {
        private XElement elements = new XElement("aisteps");
        private int id = 0, turn = 0;

        public void write()
        {
            makeUnitAndNT(0, 0, 4);
            makeUnitAndNT(1, 1, 4);
            makeUnitAndNT(0, 2, 4);
            makeUnitAndNT(3, 4, 4);

            for(int i = 0; i < 10; i++)
            {
                makeUnitAndNT(1, 3, 4);
                makeUnitAndNT(1, 1, 4);
            }
             
            elements.Save("Assets/datas/ai2.xml");
        }

        public void makeUnitAndNT(int unit, int x, int y)
        {
            elements.Add(makeUnit(unit, x, y));
            elements.Add(makeTurn());
        }

        public XElement makeUnit(int unit, int x, int y)
        {
            XElement xe = new XElement("step");
            xe.Add(makeData("id", id + ""));
            id++;
            xe.Add(makeData("unit", unit + ""));
            xe.Add(makeData("x", x + ""));
            xe.Add(makeData("y", y + ""));
            xe.Add(makeData("turn", turn + ""));
            xe.Add(makeData("type", "unit"));

            return xe;
        }

        public XElement makeTurn()
        {
            XElement xe = new XElement("step");
            xe.Add(makeData("id", id + ""));
            id++;
            xe.Add(makeData("turn", turn + ""));
            xe.Add(makeData("type", "turn"));
            turn++;
            return xe;
        }

        public XElement makeData(string name, string value)
        {
            XElement xe = new XElement(name);
            xe.Value = value;

            return xe;
        }
    }
}
