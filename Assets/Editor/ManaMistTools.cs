using UnityEngine;
using UnityEditor;
using ManaMist.Utility;

public class MenuItems
{
    [MenuItem("ManaMist/Parse Entity Csv")]
    private static void ParseEntityCsv()
    {
        EntityParser.ReadEntityCsv("Entities/entities");
    }
}