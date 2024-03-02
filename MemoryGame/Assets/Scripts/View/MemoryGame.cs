using Memory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memory.View
{
    public class MemoryGame : MonoBehaviour
    {
        // Start is called before the first frame update
        private MemoryBoard _board;
        private List<Memory.Model.Tile> _tiles;

        void Start()
        {
            _board = new MemoryBoard(3, 3);
            _tiles = new List<Memory.Model.Tile>();
            _tiles = _board.Tiles;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
