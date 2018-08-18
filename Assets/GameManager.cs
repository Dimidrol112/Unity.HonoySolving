using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject donePanel;
    public InputField sizeIf;
    public GameObject cam;
    public GameObject prefab;
    public Tower[] towers;
    public Transform[] bases;
    public float speed = 10;
    public int count = 5;
    public int gap = 1;

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Solve()
    {
        PrepScene();
        StartCoroutine(SolveHonoy());
    }

    public IEnumerator SolveHonoy()
    {
        yield return MoveStack(count, 0, 2);
        donePanel.SetActive(true);
    }

    public void PrepScene()
    {
        count = int.Parse(sizeIf.text);
        //Debug.Log(count);
        towers[1].transform.Translate(new Vector3(count + gap, 0, 0));
        towers[2].transform.Translate(new Vector3(count * 2 + gap * 2, 0, 0));

        Tower.moveSpeed = speed;

        foreach (var _base in bases)
        {
            _base.localScale = new Vector3(count, 1, count);
        }

        towers[0].GenTower(prefab, count);

        cam.transform.SetPositionAndRotation(new Vector3(towers[1].transform.position.x, count, -count * 3), Quaternion.identity);


    }

    public void UpdateSize(float size)
    {
        sizeIf.text = size.ToString();
    }

    public void UpdateSpeed(float _speed)
    {
        speed = _speed;
    }

    public IEnumerator MoveStack(int size, int from, int to)
    {
        if (size <= 0)
            yield break;

        int third = ThirdAxe(from, to);

        yield return MoveStack(size - 1, from, third);

        //Debug.Log(from + " " + to);
        yield return towers[from].MoveRing(towers[to]);

        yield return MoveStack(size - 1, third, to);
    }

    public int ThirdAxe(int from, int to)
    {
        int result = 0;
        switch (from)
        {
            case 0:
                if (to == 1) { result = 2; } else { result = 1; }
                break;
            case 1:
                if (to == 2) { result = 0; } else { result = 2; }
                break;
            case 2:
                if (to == 0) { result = 1; } else { result = 0; }
                break;
        }
        return result;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
