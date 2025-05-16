using System;
public enum GameState
{
    MENU,
    WEAPONSELECTTION,
    GAME,
    GAMEOVER,
    STATECOMPLETE,
    WAVETRANSITION,
    SHOP
}

public enum Stat
{
    Attack,
    AttackSpeed,
    CriticalChance,
    CriticalPercent,
    MoveSpeed,
    MaxHealth,
    Range,
    HealthRecoverySpeed,
    Armor,
    Luck,
    Dodge,
    Lifesteal
}

public static class Enums
{
    public static string FormatStatName(Stat stat)
    {
        string formated = "";
        string unformatedString = stat.ToString();

        if (unformatedString.Length <= 0)
            return "Unvalid Stat Name";

        formated += unformatedString[0];

        for (int i = 1; i < unformatedString.Length; i++)
        {
            if (char.IsUpper(unformatedString[i]))
                formated += " ";

            formated += unformatedString[i];
        }

        return formated;
    }
}
