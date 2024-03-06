using System.Collections;
using System.Collections.Generic;

namespace Memory.Model.States
{
    public abstract class TileStateBaseClass : ITileState
    {
        public abstract TileStates State { get; }
        public Tile Tile { get; set; }
        public TileStateBaseClass(Tile tile)
        {
            Tile = tile;
        }
    }

    public class TileHiddenState : TileStateBaseClass
    {
        public override TileStates State { get; } = TileStates.Hidden;
        public TileHiddenState(Tile tile) :base(tile)
        {

        }
    }

    public class TilePreviewState : TileStateBaseClass
    {
        public override TileStates State { get; } = TileStates.Preview;
        public TilePreviewState(Tile tile) : base(tile)
        {

        }
    }
    public class TileFoundState : TileStateBaseClass
    {
        public override TileStates State { get; } = TileStates.Found;
        public TileFoundState(Tile tile): base(tile)
        {

        }
    } 
}