using System.Collections;
using System.Collections.Generic;

namespace Memory.Model
{
    public class Tile : ModelBaseClass
    {
       public int Row { get; set; }
       public int Column { get; set; }
       public MemoryBoard Board { get; set; }
       public int MemoryCardId { get; set; }
        public Tile(int row, int column, MemoryBoard board)
        {
            Row = row;
            Column = column;
            Board = board;
        }

        public override string ToString()
        {
            return $"Tile: row:{Row}, column:{Column}, id:{MemoryCardId}";
        }
    }
}