using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.ui
{
    //tilemap cannot drawing completely
    //we must draw it precisely. so we do that in code.
    public class AutoMapper : MonoBehaviour
    {
        public GameObject[] tiles;
        public GameObject selected, selected2;

        private string map_file_name = "assets/datas/map.txt";
        
        void Awake()
        {
            TileInit();
            MakeTile();
        }

        void TileInit()
        {
            this.tiles = Resources.LoadAll<GameObject>("tiles");

            for(int i = 0; i < tiles.Length; i++)
            {
                GameObject tile = this.tiles[i];
                Sprite sprite = tile.GetComponent<SpriteRenderer>().sprite;
                float width = sprite.bounds.size.x;
                float height = sprite.bounds.size.y;
                tile.transform.localScale = new Vector3(1.0f/width, 1.0f/height);
                //Debug.Log("AutoMapper Sprite Size Check : " + width + " : " + height);
            }
        }

        void Start()
        {
            this.selected = GameObject.Find("selected");
            this.selected.transform.localScale = new Vector3(1 / 1.25f, 1 / 1.25f);
            this.selected.GetComponent<SpriteRenderer>().enabled = false;
            this.selected2 = GameObject.Find("selected2");
            this.selected2.transform.localScale = new Vector3(1 / 1.25f, 1 / 1.25f);
            this.selected2.GetComponent<SpriteRenderer>().enabled = false;
        }

        void MakeTile()
        {
            MapParser parser = new MapParser(map_file_name);

            for (int i = 0; i < MapParser.X_NUM; i++)
            {
                for (int j = 0; j < MapParser.Y_NUM; j++)
                {
                    int idx = parser.GetData(i, j);
                    GameObject obj = Instantiate(tiles[idx], this.transform);
                    obj.transform.Translate(new Vector3(i - 2f, j - MapParser.Y_NUM / 2));
                    obj.GetComponent<AutoTile>().move(i, j-2);
                    obj.name = "autotile(" + i + "," + (j-2) + ")";
                }
            }
        }

        void Update()
        {
        }
    }
}
