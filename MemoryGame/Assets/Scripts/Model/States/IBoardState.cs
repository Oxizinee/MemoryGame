using System.Collections;
using System.Collections.Generic;

namespace Memory.Model.States
{
    public interface IBoardState 
    {
        BoardStates BoardState { get; }
        MemoryBoard Board { get; set; }
        void AddPreview(Tile tile);
        void TileAnimationEnded(Tile tile);
    }
}
