using UnityEngine;

public struct Operator
{
    public string Name { get; set; }
    public int Gun { get; set; }

    public string Bio { get; set; }

    public Operator(string name, int gun, string bio)
    {
        Name = name;
        Gun = gun;
        Bio = bio;
    }
}
