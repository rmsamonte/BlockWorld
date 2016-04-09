using UnityEngine;
using System.Collections;

public class UnityUtils
{
    public static GameObject CreateGameObject(string path)
    {
        return GameObject.Instantiate(Resources.Load(path) as GameObject);
    }

    public static GameObject FindGameObject( string name )
    {
        return GameObject.Find( name );
    }
	
    public static GameObject FindChildByName( GameObject root, string name )
    {
        return root.transform.FindChild( name ).gameObject;
    }
}
