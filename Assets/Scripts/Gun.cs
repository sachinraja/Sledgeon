using UnityEngine;

public struct Gun
{
    public string Name { get; set; }
    public int Damage { get; set; }

    public int Burst { get; set; }

    public float Firerate { get; set; }

    public Gun(string name, int damage, int burst, float firerate)
    {
        Name = name;
        Damage = damage;
        Burst = burst;
        Firerate = firerate;
    }
}
