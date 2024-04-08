using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Memory.Model.States
{
    public abstract class BoardStateBaseClass : IBoardState
    {
        public abstract BoardStates State { get; } 
        public MemoryBoard Board { get; set; }
        public BoardStateBaseClass(MemoryBoard board)
        {
            Board = board;
        }
        public abstract void AddPreview(Tile tile);
        public abstract void TileAnimationEnded(Tile tile);
       
    }

    public class BoardNoPreviewState : BoardStateBaseClass
    {
        public override BoardStates State { get; } = BoardStates.NoPreview;
        public BoardNoPreviewState(MemoryBoard board): base(board)
        {

        }
        public override void TileAnimationEnded(Tile tile)
        {
            Board.ToggleActivePlayer();
        }

        public override void AddPreview(Tile tile)
        {
            if(tile.TileState.State != TileStates.Hidden) return;

            tile.TileState = new TilePreviewState(tile);
            Board.PrewingTiles.Add(tile);
            Board.BoardState = new BoardOnePreviewState(Board);
        }
    }

    public class BoardOnePreviewState : BoardStateBaseClass
    {
        public override BoardStates State { get; } = BoardStates.OnePreview;
        public BoardOnePreviewState(MemoryBoard board): base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {
            if(tile.TileState.State != TileStates.Hidden) return;

            tile.TileState = new TilePreviewState(tile);
            Board.PrewingTiles.Add(tile);
            if (Board.IsCombinationFound)
            {
                Board.BoardState = new BoardTwoFoundState(Board);
                foreach (Tile t in Board.PrewingTiles)
                {
                    t.TileState = new TileFoundState(t);
                }
            }
            else
            {
                Board.BoardState = new BoardTwoPreviewState(Board);
                tile.TileState = new TilePreviewState(tile);
            }
        }

        public override void TileAnimationEnded(Tile tile)
        {
        }
    }

    public class BoardTwoPreviewState : BoardStateBaseClass
    {
        public override BoardStates State { get; } = BoardStates.TwoPreview;
        public BoardTwoPreviewState(MemoryBoard board): base(board)
        {

        }

        public override void AddPreview(Tile tile)
        {
        }

        public override void TileAnimationEnded(Tile tile)
        {
            if (tile == Board.PrewingTiles[1])
            {
                Board.BoardState = new BoardTwoHiddingState(Board);
                foreach (Tile t in Board.PrewingTiles)
                {
                    t.TileState = new TileHiddenState(t);
                }
            }
        }
    }
    public class BoardTwoFoundState : BoardStateBaseClass
    {
        public override BoardStates State { get; } = BoardStates.TwoFound;
        public BoardTwoFoundState(MemoryBoard board): base(board)
        {

        }

        public override void AddPreview(Tile tile)
        {
        }

        public override void TileAnimationEnded(Tile tile)
        {
            Board.PrewingTiles.Remove(tile);
            Board.CurrentPlayer.Score += 1;

            if (Board.PrewingTiles.Count <= 0 &&
                Board.Tiles.Where(t => t.TileState.State == TileStates.Hidden).Count() < 2)
            {
                Board.BoardState = new BoardFinishedState(Board);
            }
            else if (Board.PrewingTiles.Count <= 0)
            {
                Board.BoardState = new BoardNoPreviewState(Board);
            }
        }
    }
    public class BoardTwoHiddingState : BoardStateBaseClass
    {
        public override BoardStates State { get; } = BoardStates.TwoHiding;
        public BoardTwoHiddingState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {
        }

        public override void TileAnimationEnded(Tile tile)
        {
            Board.PrewingTiles.Remove(tile);
            if (Board.PrewingTiles.Count == 0)
            {
                Board.BoardState = new BoardNoPreviewState(Board);
            }
        }
    }
    public class BoardFinishedState : BoardStateBaseClass
    {
        public override BoardStates State { get; } = BoardStates.Finished;
        public BoardFinishedState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {
        }

        public override void TileAnimationEnded(Tile tile)
        {
        }
    }
}
