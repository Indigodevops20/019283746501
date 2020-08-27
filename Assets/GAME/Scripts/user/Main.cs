using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Statistics stats;
    public void ObtenerStats()
    {
        stats.GetStats();
    }

    public void SubirStats()
    {
        stats.SetStats();
    }
}
