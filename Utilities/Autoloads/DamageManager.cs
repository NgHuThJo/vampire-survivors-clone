using System;
using Game.Common.Components;
using Game.Entities.Weapons;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class DamageManager : Node
{
    public static DamageManager Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public int ApplyDamage(IDamageContext context)
    {
        if (context is AoeWeaponContext aoeWeapon)
        {
            var damage = aoeWeapon.Data.Damage;

            return damage;
        }

        throw new Exception("Damge context did not match any pattern");
    }
}
