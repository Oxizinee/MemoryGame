using Memory.Data;
using Memory.Model.States;
using Memory.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Memory.Model
{
    public class MemoryBoard : ModelBaseClass
    {
        private IBoardState _boardState;

        public PlayFabLogin PlayFabScript { get; set; }
      
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
        public Player CurrentPlayer {  get; set; }  

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
        public MemoryBoard(int rows, int columns, PlayFabLogin playFabScript)
        {
            Tiles = new List<Tile>();
            PrewingTiles = new List<Tile>();
            PlayFabScript = playFabScript;

            Rows = rows;
            Columns = columns;

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
            ImageRepository repository = ImageRepository.Instance;
            repository.ProcessImageIds(AssignMemoryCardIds);
        }
        private void AssignMemoryCardIds(List<int> memoryCardIDs)
        {
            memoryCardIDs = memoryCardIDs.Shuffle();
            List<Tile> ShuffledCards = ExtensionMethods.Shuffle(Tiles);

            int memoryCardIndex = 0;
            bool first = true;

           foreach(Tile tile in ShuffledCards) 
            {
                tile.MemoryCardId = memoryCardIDs[memoryCardIndex];
                if (first)
                {
                    first = false;
                }
                else
                {
                    memoryCardIndex++;
                    first = true;
                }
            }
        }

        public override string ToString()
        {
            return $"Board: {Rows},{Columns}";
        }
        public void ToggleActivePlayer()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer.IsActive = false;
                CurrentPlayer = Player2;
                CurrentPlayer.IsActive = true;
            }
            else
            {
                CurrentPlayer.IsActive = false;
                CurrentPlayer = Player1;
                CurrentPlayer.IsActive = true;
            }
        }

        public void FinishGame()
        {
            Player1.IsActive = false;
            Player2.IsActive = false;
            CurrentPlayer = null;
        }

        public void AddScore()
        {
            CurrentPlayer.Score++;
        }
    }
}
