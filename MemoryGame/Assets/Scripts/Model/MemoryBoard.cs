using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Memory.Model
{
    public class MemoryBoard : ModelBaseClass
    {
        private IBoardState _boardState;
        public IBoardState BoardState
        {
            get { return _boardState; }
            set
            {
                if(_boardState == value) return;
                _boardState = value;
                OnPropertyChanged();
            }
        }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        private int _rows;
        public int Rows
        {
            get { return _rows; }
            set
            {
                if (_rows == value) return;
                _rows = value;
                OnPropertyChanged();
            }
        }
        private int _columns;
        public int Columns
        {
            get { return _columns; }
            set 
            {
                if(_columns == value) return;
                _columns = value;
                OnPropertyChanged();
            }
        }
        public List<Tile> Tiles { get; set; }
        public List<Tile> PrewingTiles { get; set; }
        public bool IsCombinationFound
        {
            get
            {
                if (PrewingTiles.Count != 2) return false;
                return PrewingTiles[0].MemoryCardId == PrewingTiles[1].MemoryCardId;
            }
        }
        public MemoryBoard(int rows, int columns, Player player1, Player player2)
        {
            Tiles = new List<Tile>();
            PrewingTiles = new List<Tile>();

            Rows = rows;
            Columns = columns;

            Player1 = player1;
            Player2 = player2;

            Player1.IsActive = true;
            Player2.IsActive = false;

            BoardState = new BoardNoPreviewState(this);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Tile tile = new Tile(i, j, this);
                    Tiles.Add(tile);
                }
            }

            AssignMemoryCardIds();
        }
        private void AssignMemoryCardIds()
        {
            int maxID = Tiles.Count /2;

            if ((Tiles.Count) % 2 == 0) //even
            {
                for (int i = 0; i < (Tiles.Count) / 2; i++)
                {
                    Tiles[i].MemoryCardId = i;
                    Tiles[(Tiles.Count - 1) - i].MemoryCardId = i;
                }
            }
            else if((Tiles.Count) % 2 == 1) //uneven
            {
                int i = 0;
                for (i = 0; i < (Tiles.Count - 1) / 2; i++)
                {
                    Tiles[i].MemoryCardId = i;
                    Tiles[(Tiles.Count - 1) - i].MemoryCardId = i;
                }
                Tiles[Tiles.Count - 1].MemoryCardId = i;
            }
        }

        public override string ToString()
        {
            return $"Board: {Rows},{Columns}";
        }
    }
}
