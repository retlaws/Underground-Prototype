using UnityEngine;
using Digger.Modules.Core.Sources;
using UnityEngine.UIElements;
using System.Runtime.Remoting.Messaging;

[CreateAssetMenu(fileName = "DiggingTool", menuName = "Digging Tool/Make New Digging Tool", order = 0)]
public class DiggingTool : ScriptableObject
{
    [Header("Attributes")]
    [Range(0.5f, 20f)]
    public float radius = 1f;

    [Range(0, 1)]
    public float opacity = 0.5f;

    [Range(-0.5f, 0.5f)]
    public float depth = 0;

    [Range(1, 10)]
    public float diggingSpeed;

    [Range(0.5f, 5)]
    public float range = 1;

    public BrushType brushType;

    public string animationName; //this is super hacky! Don't do this normally!
}
