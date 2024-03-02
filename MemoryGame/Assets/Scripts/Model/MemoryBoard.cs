using System.Collections;
using System.Collections.Generic;

namespace Memory.Model
{
    public class MemoryBoard : ModelBaseClass
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<Tile> Tiles { get; set; }
        public List<Tile> PrewingTiles { get; set; }
        //public bool IsCombinationFound
        //{
        //    get
        //    {
        //        if (PrewingTiles.Equals(Tile))
        //            return true;
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public MemoryBoard(int rows, int columns)
        {
            Tiles = new List<Tile>();

            Rows = rows;
            Columns = columns;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Tile tile = new Tile(i,j,this);
                    Tiles.Add(tile);
                }
            }

            AssignMemoryCardIds();

        }
        private void AssignMemoryCardIds()
        {

        }

        public override string ToString()
        {
            return $"Board: {Rows},{Columns}";
        }
    }
}
