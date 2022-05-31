using System;

namespace Assets.Scripts.Model
{
    class TileWeight : IComparable<TileWeight>
    {
        public int weight;
        private readonly Tile t;


        /// <summary>
        /// Default constructor with given reference Tile and custom weight
        /// </summary>
        /// <param name="t">The Tile that this weight object will reference, </param>
        /// <param name="weight">The weight of this object</param>
        public TileWeight(Tile t, int weight)
        {
            this.t = t;
            this.weight = weight;
        }

        /// <summary>
        /// Returns the current referencing 'Tile'
        /// </summary>
        public Tile Tile
        {
            get { return t; }
        }

        /// <summary>
        /// Compares the given object with the current instance
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>
        /// The return value is less than zero if the current instance is less than the value. 
        /// It’s zero, if the current instance is equal to value, 
        /// whereas return value is more than zero if the current instance is more than value
        /// </returns>
        public int CompareTo(TileWeight other)
        {
            return weight.CompareTo(other.weight);
        }
    }
}
