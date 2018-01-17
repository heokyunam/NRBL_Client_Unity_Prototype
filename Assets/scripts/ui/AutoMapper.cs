using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.scripts.ui
{
    //tilemap cannot drawing completely
    //we must draw it precisely. so we do that in code.
    public class AutoMapper : MonoBehaviour
    {
        public GameObject[] tiles;
        public GameObject selected, selected2;

        private const string map_file_name = "datas/map";
        
        void Awake()
        {
            Screen.SetResolution(600, 900, false);
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
            GameObject player = GameObject.Find("Player");

            this.selected = player.transform.Find("selected").gameObject;
            this.selected.transform.localScale = new Vector3(1 / 1.25f, 1 / 1.25f);

            this.selected2 = player.transform.Find("selected2").gameObject;
            this.selected2.transform.localScale = new Vector3(1 / 1.25f, 1 / 1.25f);
        }

        void MakeTile()
        {
            TextAsset txt = Resources.Load<TextAsset>(map_file_name);
            MapParser parser = new MapParser(new StringReader(txt.text));

            for (int i = 0; i < MapParser.X_NUM; i++)
            {
                for (int j = 0; j < MapParser.Y_NUM; j++)
                {
                    int idx = parser.GetData(i, j);
                    GameObject obj = Instantiate(tiles[idx], this.transform);
                    obj.transform.Translate(new Vector3(i - 2f, j - MapParser.Y_NUM / 2));
                    obj.GetComponent<AutoTile>().move(i, j-2);
                    obj.name = "autotile(" + i + "," + (j-2) + ")";
                    if (j >= 2 && j < 4)
                        obj.tag = "placable";
                    else
                        obj.tag = "unplacable";
                }
            }
        }

        void Update()
        {
        }
    }
}
