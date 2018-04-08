using System;

public class SearchEventArgs : EventArgs
{
    private string text = "";

    public SearchEventArgs(string searchtext)
    {
        text = searchtext;
    }

    public string Searchtext
    {
        get { return text; }
        set { this.text = value; }
    }
}