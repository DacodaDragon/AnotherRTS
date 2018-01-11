namespace AnotherRTS.Util.Notification
{
    public delegate void Notify<Context>(Context context);
    public delegate void NotifyChange<Context, Value>(Context context, Value value);
}
