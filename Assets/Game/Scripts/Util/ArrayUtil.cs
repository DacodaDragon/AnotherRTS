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

        /// <summary>
        /// Fills all elements with a value
        /// </summary>
        public static void Fill<Type>(Type[] array, Type value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }

        /// <summary>
        /// Checks if all elements in array equal the same value
        /// </summary>
        public static bool AllEqual<Type>(Type[] array, Type value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                // [TODO] check perf .Equals(obj);
                if (!array[i].Equals(value))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if any element in array equals value
        /// </summary>
        public static bool Contains<Type>(Type[] array, Type value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                // [TODO] check perf .Equals(obj);
                if (array[i].Equals(value))
                    return true;
            }
            return false;
        }
    }
}
