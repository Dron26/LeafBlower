using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
public class Parametrs : MonoBehaviour
{
    public float FuelLevel;
    public float MaxFuelLevel;
    public float StepChangeLevel;
    public float StepRefuelingLevel;

    public float DirectionX;
    public float DirectionY;
    public float DirectionZ;
    public float EndRang;

    public Parametrs(float maxFuelLevel, float stepChangeLevel, float stepRefuelingLevel, float directionX, float directionY, float directionZ, float endRang)
    {
        MaxFuelLevel = maxFuelLevel;
        FuelLevel = MaxFuelLevel;
        StepChangeLevel = stepChangeLevel;
        StepRefuelingLevel = stepRefuelingLevel;

        DirectionX = directionX;
        DirectionY = directionY;
        DirectionZ = directionZ;
        EndRang = endRang;
    }
}
}
