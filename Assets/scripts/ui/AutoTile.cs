using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.ui
{
    public class AutoTile : MonoBehaviour
    {
        public int x, y;
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }
        public void move(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public void attach(GameObject selected)
        {
            if(isInArea())
            {
                Vector3 temp = this.transform.localPosition;
                temp.z = -1;
                selected.transform.localPosition = temp;
                selected.SetActive(true);
            }
            else
            {
                selected.SetActive(false);
            }
        }
        public bool isInArea()
        {
            return y >= 0 && y < 5;
        }
    }
}
