using UnityEngine;
using UnityEditor;
using ManaMist.Utility;

public class MenuItems
{
    [MenuItem("ManaMist/ParseEntityCsv")]
    private static void ParseEntityCsv()
    {
        EntityParser.ReadEntityCsv("Entities/entities");
    }
}