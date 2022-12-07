using System;
using UnityEngine;
using System.Linq;

public static class ColorIntConverter 
{
    public static Color[] ToColors(int value)
    {
        string byteStr = Convert.ToString(value, 2);
        byteStr = new string(byteStr.Reverse().ToArray());
        
        var chunks = byteStr.Select((v, i) => new { v, i })
            .GroupBy(x => x.i / 3)
            .Select(f => f.Select(x => x.v));

        Color[] colors = new Color[chunks.Count()];
        
        

        var index = 0;
        foreach (var chunk in chunks)
        {
            int r = 0;
            int g = 0;
            int b = 0;
            var chunkIndex = 0;
            foreach (var c in chunk)
            {
                if (chunkIndex == 0)
                    b = int.Parse(c.ToString());
                if (chunkIndex == 1)
                    g = int.Parse(c.ToString());
                if (chunkIndex == 2)
                    r = int.Parse(c.ToString());
                
                chunkIndex++;
            }

            colors[index] = new Color(r, g, b);
            index++;
        }

        colors = colors.Reverse().ToArray();
        return colors;
    }
    
    public static int ToInt(Color[] colors)
    {
        //colorsは0から順に左詰め
        //例:col[0](1,1,0),col[1](1,0,1)の場合は110101=>53
        
        int count = colors.Length;
        //RGB
        //000
        string byteStr = "";
        for (int i = 0; i < count; i++)
        {
            float r = colors[i].r;
            float b = colors[i].b;
            float g = colors[i].g;

            byteStr += FloatToStr(r);
            byteStr += FloatToStr(g);
            byteStr += FloatToStr(b);
        }

        int result = Convert.ToInt32(byteStr,2);
        return result;
    }

    private static string FloatToStr(float value)
    {
        string result = "";
        if (value > 0.5)
            result = "1";
        else
            result = "0";

        return result;
    }
}
