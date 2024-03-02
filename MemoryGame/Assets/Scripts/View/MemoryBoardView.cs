using Memory.Model;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace Memory.View
{
    public class MemoryBoardView : ViewBaseClass<MemoryBoard>
    {
        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public void SetUpMemoryBoardView(MemoryBoard model, GameObject tilePrefab)
        {
            foreach (Tile tile in model.Tiles)
            {
                GameObject tileInstance = Instantiate(tilePrefab, new Vector3(tile.Row *(tilePrefab.transform.localScale.x + 0.5f) ,0.1f,tile.Column * (tilePrefab.transform.localScale.z + 0.5f)), Quaternion.identity);
                tileInstance.transform.SetParent(transform);
                tileInstance.GetComponent<TileView>().SetModel(tile);
            }
        }
    }
}