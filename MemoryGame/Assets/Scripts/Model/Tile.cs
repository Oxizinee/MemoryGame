using Memory.Model.States;
using System.Collections;
using System.Collections.Generic;

namespace Memory.Model
{
    public class Tile : ModelBaseClass
    {
        private ITileState _tileState;
        public ITileState TileState 
        { 
            get
                { return _tileState; }
            set 
            {
                if (_tileState == value) return;
                _tileState = value;
                OnPropertyChanged();
            } 
        }
        private int _row;
       public int Row
        { 
            get { return _row; }
            set 
            { 
                if(_row == value) return;
                _row = value;   
                OnPropertyChanged();
            }
        }

        private int _column;
       public int Column
        {
            get { return _column; }
            set
            {
                if (_column == value) return;
                _column = value;
                OnPropertyChanged();
            }
        }

        private MemoryBoard _board;
        public MemoryBoard Board
        {
            get { return _board; }
            set
            {
                if (_board == value) return;
                _board = value;
                OnPropertyChanged();
            }
        }

        private int _memoryCardId;
        public int MemoryCardId
        {
            get { return _memoryCardId; }
            set
            {
                if (_memoryCardId == value) return;
                _memoryCardId = value;
                OnPropertyChanged();
            }
        }

        public Tile(int row, int column, MemoryBoard board)
        {
            Row = row;
            Column = column;
            Board = board;
            TileState = new TileHiddenState(this);
        }

        public override string ToString()
        {
            return $"Tile: row:{Row}, column:{Column}, id:{MemoryCardId}";
        }
    }
}