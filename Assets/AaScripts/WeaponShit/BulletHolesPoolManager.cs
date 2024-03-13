using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolesPoolManager : MonoBehaviour
{

    [SerializeField] GameObject bulletHoles;

    public static List<GameObject> holeList = new List<GameObject>();
    void Awake()
    {
        CreateHolePool();
    }
    private void CreateHolePool()
    {
        for (int i = 0; i < 30 ; i++)
        {
            GameObject go = Instantiate(bulletHoles, this.transform);
            holeList.Add(go);
            go.SetActive(false);           
        }
    }
}
