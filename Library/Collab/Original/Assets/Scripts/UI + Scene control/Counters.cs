using System.Collections;
using UnityEngine;
using UnityEngine.UI;
 
public class Counters : MonoBehaviour
{
    internal int rabbitCount;
    internal int wolfCount;

    
    [SerializeField] InputField field;
 
    public void RabbitCounter()
    {
        if (field.text != null)
        {
            rabbitCount = int.Parse(field.text);
            // Debug.Log(RabbitCount);


        }
    }

     public int RCgetter()
     {
         return rabbitCount;
     }
    
    public void WolfCounter()
    {
        if (field.text != null)
        {
            wolfCount = int.Parse(field.text);
            // Debug.Log(WolfCount);
        }
    }
    // public int WCgetter()
    // {
    //     return WolfCount;
    // }

}
