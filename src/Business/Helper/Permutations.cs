using System.Collections.Generic;
using System.Linq;

namespace Business.Helper
{
    public class Permutations<T>
    {
        public List<List<T>> GeneratePermutations(IList<T> values)
        {
            var current = new T[values.Count];
            var inSelection = new bool[values.Count];
            var results = new List<List<T>>();

            PermuteValues(values, inSelection, current, results, 0);

            return results;
        }

        private void PermuteValues(IList<T> values, IList<bool> inSelection, IList<T> current, ICollection<List<T>> results, int nextPos)
        {
            // See if all of the positions are filled.
            if (nextPos == values.Count)
            {
                // All of the positioned are filled.
                // Save this permutation.
                results.Add(current.ToList());
            }
            else
            {
                // Try options for the next position.
                for (int i = 0; i < values.Count; i++)
                {
                    if (!inSelection[i])
                    {
                        // Add this item to the current permutation.
                        inSelection[i] = true;
                        current[nextPos] = values[i];

                        // Recursively fill the remaining positions.
                        PermuteValues(values, inSelection, current, results, nextPos + 1);

                        // Remove the item from the current permutation.
                        inSelection[i] = false;
                    }
                }
            }

        }
    }
}