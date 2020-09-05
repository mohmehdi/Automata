using System.Collections.Generic;

public interface IInputProcessor
{
    bool SyntaxCheck(string s);
    List<string> GetTags(string s);
}