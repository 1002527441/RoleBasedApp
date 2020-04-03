using Microsoft.AspNetCore.Components;

namespace BlazorMinimal1
{
    public class IndexBase : LayoutComponentBase
    {
        public string GetTextFromMethodInClass()
        {
            return "The source for this text was external C# code in a .cs file";
        }
    }
}