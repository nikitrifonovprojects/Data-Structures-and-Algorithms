namespace NT.DataStructures
{
    internal class HashSlot<T>
    {
        internal int HashCode { get; set; }

        internal T Value { get; set; }
    }
}
