namespace AnotherRTS.Util.StaticTuples
{
    public struct Tuple2<Type1, Type2>
    {
        public readonly Type1 Item1;
        public readonly Type2 Item2;

        public Tuple2(Type1 item1, Type2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}

