
/// <summary>
/// D is Short for Deterministic
/// </summary>
public class TagFormat
{
   public char input;
   public char machine;
   public string machineCommand;

    public TagFormat()
    {
        machineCommand = "";
    }
    public string GetAllTogether()
    {
        return input.ToString() + machine.ToString() + machineCommand;
    }
}
