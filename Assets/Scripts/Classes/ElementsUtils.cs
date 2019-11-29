using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ElementsUtils
{
    public static Gradient getElementGradient(Elements elem)
    {
        Gradient g = new Gradient();
        GradientColorKey[] gck = new GradientColorKey[2];
        GradientAlphaKey[] gak = new GradientAlphaKey[2];
        gck[0].time = -1f;
        gck[1].time = -1f;
        gak[0].time = -1f;
        gak[1].time = -1f;
        gak[0].alpha = 255f;
        gak[1].alpha = 255f;

        switch (elem)
        {
            case Elements.Neutral:
                gck[0].color = Color.white;
                gck[1].color = Color.white;
                break;
            case Elements.Fire:
                gck[0].color = Color.red;
                gck[1].color = Color.red;
                break;
            case Elements.Water:
                gck[0].color = Color.blue;
                gck[1].color = Color.blue;
                break;
            case Elements.Thunder:
                gck[0].color = Color.yellow;
                gck[1].color = Color.yellow;
                break;
            case Elements.Earth:
                gck[0].color = new Color(0.7f, 0.3f, 0f, 1f);
                gck[1].color = new Color(0.7f, 0.3f, 0f, 1f);
                break;
        }

        g.SetKeys(gck, gak);
        return g;
    }

    public static Color getElementColor(Elements elem)
    {
        Color color = new Color();

        switch (elem)
        {
            case Elements.Neutral:
                color = Color.white;
                break;
            case Elements.Fire:
                color = Color.red;
                break;
            case Elements.Water:
                color = Color.blue;
                break;
            case Elements.Thunder:
                color = Color.yellow;
                break;
            case Elements.Earth:
                color.r = 0.7f;
                color.g = 0.3f;
                color.b = 0f;
                color.a = 1f;
                break;
        }

        return color;
    }

    public static bool elementMatch(Elements elem1, Elements elem2)
    {
        switch (elem1)
        {
            case Elements.Neutral:
                if (elem2 == Elements.Neutral) return true;
                else return false;
            case Elements.Fire:
                if (elem2 == Elements.Thunder) return true;
                else return false;
            case Elements.Water:
                if (elem2 == Elements.Fire) return true;
                else return false;
            case Elements.Thunder:
                if (elem2 == Elements.Water) return true;
                else return false;
            case Elements.Earth:
                if (elem2 == Elements.Thunder) return true;
                else return false;
            default:
                return false;
        }
    }
}

public enum Elements
{
    Neutral,
    Fire,
    Water,
    Thunder,
    Earth
}