using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Assets.scripts.ui
{
    //not used anymore
    //just we use for map parsing
    //but now we use prefab data
    //that time, we try to map data parsing automatically. but we fail. so don't use this anymore
    public class MapParser
    {
        public static int X_NUM = 5, Y_NUM = 9;
        private string[] datas;
        public MapParser(string filename)
        {
            datas = new string[Y_NUM];
            StreamReader reader = new StreamReader(filename);
            for (int i = 0; i < Y_NUM; i++)
                datas[Y_NUM - i - 1] = reader.ReadLine();
        }

        public MapParser(string[] datas)
        {
            this.datas = datas;
        }

        public int GetData(int x, int y)
        {
            return datas[y][x] - '0';
        }

        public bool Equals(MapParser mp)
        {
            for(int i = 0; i < Y_NUM; i++)
            {
                if (datas[i].Equals(mp.datas[i]) == false)
                    return false;
            }
            return true;
        }
    }
}
