namespace AnotherRTS.Util.Tuples
{
    public struct Tuple3<Type1, Type2, Type3>
    {
        public readonly Type1 Item1;
        public readonly Type2 Item2;
        public readonly Type3 Item3;

        public Tuple3(Type1 item1, Type2 item2, Type3 item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }
    }
}

