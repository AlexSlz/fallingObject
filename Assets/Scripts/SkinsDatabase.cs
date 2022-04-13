using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkinsDB", menuName = "SkinsList")]
public class SkinsDatabase : ScriptableObject
{
    [SerializeField] private List<Skin> _skins;

    public List<Skin> Skins => _skins;
}

[System.Serializable]
public class Skin
{
    [SerializeField] private string _name;
    public string Name => _name;

    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private Color _color;
    public Color Color => _color;
}