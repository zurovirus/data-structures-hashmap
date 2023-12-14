using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    /// <summary>
    /// The Item Class.
    /// </summary>
    public class Item : IComparable<Item>
    {
        public string Name { get; private set; }
        public int GoldPieces { get; private set; }
        public double Weight { get; private set; }

        /// <summary>
        /// Initializes an instance of the item class with a name, goldpieces, and weight.
        /// </summary>
        /// <param name="Name">The name of the item.</param>
        /// <param name="GoldPIeces">The cost of the item.</param>
        /// <param name="Weight">The weight of the item.</param>
        public Item(string Name, int GoldPieces, double Weight)
        {
            this.Name = Name;
            this.GoldPieces = GoldPieces;
            this.Weight = Weight;
        }

        /// <summary>
        /// Overrides the Equal method and returns true when the name, goldpieces and weight match.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>The boolean value of the comparison.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            
            if (obj.GetType() != typeof(Item)) return false;

            Item comparedObj = obj as Item;

            return this.Name == comparedObj.Name && this.GoldPieces == comparedObj.GoldPieces && this.Weight == comparedObj.Weight;
        }

        /// <summary>
        /// Compares the name of an item to the name of another item.
        /// </summary>
        /// <param name="other">The item to compare.</param>
        /// <returns>The value of the item in comparison.</returns>
        public int CompareTo(Item other)
        {
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// The ToString method of the class.
        /// </summary>
        /// <returns>The formatted ToString.</returns>
        public override string ToString()
        {
            return $"{Name} is worth {GoldPieces}gp and weighs {Weight}kg";
        }
    }
}
