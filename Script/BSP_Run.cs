using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP_Run : MonoBehaviour
{
    private BSP bsp;

    public void run_bsp(GameObject parent, Color planeColor, int mapLength, int GRID_SIZE)
    {
        Area area = new Area(0, 0, mapLength, mapLength);

        bsp = new BSP(5);
        Node node = bsp.split(area, 1, null, 0);
        node.Fill(planeColor,parent);

        Node[] leaves = bsp.getLeafNodes();

        for (int i = 0; i < leaves.Length; i++)
        {
            Node n = leaves[i];
            if (n == null) break;
        }

    }
}
