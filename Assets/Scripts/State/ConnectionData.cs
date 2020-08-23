public struct ConnectionData
{
    public string Tag { get; private set; }
    public State To { get; private set; }
    public ConnectionData(string tag, State to)
    {
        this.Tag = tag;
        this.To = to;
    }
}
