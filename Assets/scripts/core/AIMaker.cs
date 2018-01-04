using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.scripts.core
{
    public class AIMaker
    {
        private Enemy enemy;

        public AIMaker(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public int BlankX()
        {
            int[] map = new int[5];
            foreach(Unit unit in enemy.UnitGroup)
            {
                if(unit.Y == 4)
                {
                    map[unit.X] = -1;
                }
            }
            
            for(int i = 0; i < map.Length; i++)
            {
                if(map[i] != -1)
                {
                    return i;
                }
            }

            return -1;
        }

        public XElement Judge()
        {
            int blank = BlankX();
            if(blank == -1)
            {
                XElement xe = new XElement("aisteps");
                xe.Add(makeTurn());
                return xe;
            }

            enemy.Coin += enemy.UnitGroup.Count(0);
            Debug.Log("Enemy.Coin : " + enemy.Coin);
            //Debug.Log("Judge : " + enemy.UnitGroup.Count(1) + "/" + enemy.UnitGroup.Count(3));
            //집의 개수 * 2가 최대 수용량. 이보다 병사개수가 적으면 만들 수 있다
            if (enemy.Coin >= 1 && enemy.UnitGroup.Count(1) <= enemy.UnitGroup.Count(3) * 2) 
            {
                return makeUnitAndNT(1, blank, 4);
            }
            else if(enemy.Coin == 0)
            {
                return makeUnitAndNT(0, blank, 4);
            }
            else
            {
                return makeUnitAndNT(3, blank, 4);
            }
        }

        public XElement makeUnitAndNT(int unit, int x, int y)
        {
            XElement elements = new XElement("aisteps");
            elements.Add(makeUnit(unit, x, y));
            elements.Add(makeTurn());
            return elements;
        }

        public XElement makeUnit(int unit, int x, int y)
        {
            XElement xe = new XElement("step");
            //xe.Add(makeData("id", id + ""));
            //id++;
            xe.Add(makeData("unit", unit + ""));
            xe.Add(makeData("x", x + ""));
            xe.Add(makeData("y", y + ""));
            //xe.Add(makeData("turn", turn + ""));
            xe.Add(makeData("type", "unit"));

            return xe;
        }

        public XElement makeTurn()
        {
            XElement xe = new XElement("step");
            //xe.Add(makeData("id", id + ""));
            //id++;
            //xe.Add(makeData("turn", turn + ""));
            xe.Add(makeData("type", "turn"));
            //turn++;
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
