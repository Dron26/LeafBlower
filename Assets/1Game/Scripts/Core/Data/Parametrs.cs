using UnityEngine;

namespace _1Game.Scripts.Core
{
public class Parametrs : MonoBehaviour
{
    public float StepChangeLevel;
    public float StepRefuelingLevel;

    public float DirectionX;
    public float DirectionY;
    public float DirectionZ;
    public float EndRang;

    public Parametrs( float stepChangeLevel, float stepRefuelingLevel, float directionX, float directionY, float directionZ, float endRang)
    {
        StepChangeLevel = stepChangeLevel;
        StepRefuelingLevel = stepRefuelingLevel;

        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;
        EndRang = endRang;
    }
}
}
