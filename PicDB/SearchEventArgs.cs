using System;

/// <summary>
/// arguments for search event
/// </summary>
public class SearchEventArgs : EventArgs
{
    private string text = "";

    public SearchEventArgs(string searchtext)
    {
        text = searchtext;
    }

    /// <summary>
    /// text for filtering
    /// </summary>
    public string Searchtext
    {
        get { return text; }
        set { this.text = value; }
    }
}