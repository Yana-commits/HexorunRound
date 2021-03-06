using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class LevelParameters 
{
    [SerializeField]
    private int zWidth =20;

    [SerializeField]
    private int xHeight= 10;

    [SerializeField]
    private float xOffset = 0.753f;

    [SerializeField]
    private float zOffset = 0.868f;

    //[SerializeField]
    //private int holesNomber = 3;

    public int ZWidth
    {
        get
        {
            return zWidth;
        }

    }
    public int XHeight
    {
        get { return xHeight; }
    }
    public float XOffset
    {
        get { return xOffset; }
    }

    public float ZOffset
    {
        get { return zOffset; }
    }

    //public int HolesNomber
    //{
    //    get
    //    {
    //        return holesNomber;
    //    }

    //}

    public LevelParameters(int koeff)
    {
        if (koeff > 1)
        {
            zWidth = (int)(zWidth + zWidth * (koeff-1)* 0.3f);
        }
        else 
        {
            zWidth = zWidth * koeff;
        }
        xHeight = this.xHeight ;
        //holesNomber = holesNomber * koeff;
    }
}
