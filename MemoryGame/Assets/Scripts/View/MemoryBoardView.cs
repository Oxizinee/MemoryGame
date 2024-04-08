using Memory.Model;
using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

namespace Memory.View
{
    public class MemoryBoardView : ViewBaseClass<MemoryBoard>
    {
        [SerializeField] private float _spacing = 1f;
        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public void SetUpMemoryBoardView(MemoryBoard model, GameObject tilePrefab)
        {
            float totalWidth = (tilePrefab.transform.localScale.x + _spacing) * model.Columns - _spacing;
            float totalHeight = (tilePrefab.transform.localScale.z + _spacing) * model.Rows - _spacing;

            float startX = -totalWidth / 2f + (tilePrefab.transform.localScale.x / 2f);
            float startY = totalHeight / 2f - (tilePrefab.transform.localScale.z / 2f);

            Vector2 offset = new Vector2(tilePrefab.transform.localScale.x + _spacing, tilePrefab.transform.localScale.z + _spacing);


            foreach (Tile tile in model.Tiles)
            {
                float xPos = startX + tile.Column * offset.x;
                float zPos = startY - tile.Row * offset.y;

                Vector3 position = new Vector3(xPos, 0.03f, zPos);

                GameObject tileInstance = Instantiate(tilePrefab, position, Quaternion.identity);
                tileInstance.transform.SetParent(transform);
                tileInstance.GetComponent<TileView>().SetModel(tile);
            }

        }

    }
}