using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_4
{
    /// <summary>
    /// The adventure class.
    /// </summary>
    public class Adventure
    {
        private HashMap<StringKey, Item> map;

        /// <summary>
        /// Initializes an instance of the Adventure class with a file path.
        /// </summary>
        /// <param name="filePath">The path of the file to be read.</param>
        /// <exception cref="ArgumentNullException">Throws an error if the filepath does not exist.</exception>
        /// <exception cref="ArgumentException">Throws an error if the file does not exist</exception>
        public Adventure(string filePath)
        {
            if (filePath == null) { throw new ArgumentNullException("The filepath cannot be null"); }
            if (!File.Exists(filePath)) { throw new ArgumentException("Cannot locate file"); }

            string[] lines = File.ReadAllLines(filePath);
            this.map = new HashMap<StringKey, Item>();
            int index = 0;

            foreach (string line in lines)
            {
                string[] stringParts = line.Split(',');
                Item item = new Item(stringParts[0].Trim(),
                                     int.Parse(stringParts[1].Trim()),
                                     double.Parse(stringParts[2].Trim()));
                StringKey key = new StringKey(stringParts[0].Trim());

                map.Put(key, item);
            }

        }

        /// <summary>
        /// Returns the hashmap.
        /// </summary>
        /// <returns>The hashmap to be returned.</returns>
        public HashMap<StringKey, Item> GetMap()
        {
            return map;
        }

        /// <summary>
        /// Prints the current loot.
        /// </summary>
        /// <returns>A list of the loot.</returns>
        public string PrintLootMap()
        {
            List<Item> list = new List<Item>();

            for (int i = 0; i < map.Table.Length; i++)
            {
                if (map.Table[i] != null && map.Table[i].Value.GoldPieces != 0)
                {
                    list.Add(map.Table[i].Value);
                }  
            }

            list.Sort();

            string[] sortedItems = new string[list.Count + 1];
 
            for (int i = 0; i < list.Count; i++)
            {
                sortedItems[i] = list[i].ToString();
            }
            return string.Join("\n", sortedItems);
        }
    }
}
