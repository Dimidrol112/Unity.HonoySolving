using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static float moveSpeed = 1;
    public List<Ring> rings = new List<Ring>();

    public void GenTower(GameObject prefab, int count)
    {
        for (int i = count; i > 0; i--)
        {
            var cube = Instantiate(prefab, transform.position, transform.rotation);
            cube.transform.localScale = new Vector3(i, 1, i);
            cube.transform.Translate(new Vector3(0, count - i, 0));
            cube.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0, 1, 0, 1, 0, 1, 0.8f, 1);
            rings.Add(new Ring(cube, i));
        }
    }

    public IEnumerator MoveRing(Tower tow)
    {
        tow.rings.Add(rings[rings.Count - 1]);
        rings.RemoveAt(rings.Count - 1);
        Vector3 ringsPos = new Vector3(tow.transform.position.x, tow.rings.Count - 1, tow.transform.position.z);
        yield return MoveTo(tow.rings[tow.rings.Count - 1].prefab.transform, ringsPos);
    }

    IEnumerator MoveTo(Transform trans, Vector3 pos)
    {
        while (Vector3.Distance(trans.position, pos) > 0.1f)
        {
            trans.position = Vector3.Lerp(trans.position, pos, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }
}

[Serializable]
public class Ring
{
    public Ring(GameObject _prefab, int _size)
    {
        prefab = _prefab;
        size = _size;
    }
    public GameObject prefab;
    public int size = 1;
}