using Memory.Model;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Memory.View
{
    public class TileView : ViewBaseClass<Tile>, IPointerDownHandler
    {
        private Tile _tileModel;
        public void SetModel(Tile tile)
        {
            _tileModel = tile;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _tileModel.Board.PrewingTiles.Add(_tileModel);
        }

        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(Model.TileState))
            {
                //startAnimation
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
