using System.Collections.Generic;

namespace AnotherRTS.Util
{
    public static class ArrayUtil
    {
        /// <summary>
        /// Adds an element to an array
        /// </summary>
        public static void AddToArray<Type>(Type[] array, params Type[] elements)
        {
            List<Type> IntermediateList = new List<Type>(array);
            IntermediateList.AddRange(elements);
            array = IntermediateList.ToArray();
        }

        /// <summary>
        /// Removes an element from an array
        /// </summary>
        public static void RemoveFromArray<Type>(Type[] array, params Type[] elements)
        {
            List<Type> IntermediateList = new List<Type>(array);
            for (int i = 0; i < elements.Length; i++)
            {
                IntermediateList.Remove(elements[i]);
            }
            array = IntermediateList.ToArray();
        }
    }
}
