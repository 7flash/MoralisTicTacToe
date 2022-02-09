using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class CellState {
    public const string Default = "DefaultState";
    public const string Adam = "AdamState";
    public const string Eve = "EveState";
}

public class ThirdScript : MonoBehaviour
{
    enum State {
        Default,
        Adam,
        Eve
    }

    private string state = CellState.Default;

    // Start is called before the first frame update

    private MeshRenderer adamBody;
    private MeshRenderer eveBody;
    private MeshRenderer defaultBody;

    void Start()
    {
        adamBody = transform.Find(CellState.Adam).GetComponentInChildren<MeshRenderer>();

        eveBody = transform.Find(CellState.Eve).GetComponentInChildren<MeshRenderer>();

        defaultBody = transform.Find(CellState.Default).GetComponentInChildren<MeshRenderer>();

        configureToAllowFading(adamBody);

        configureToAllowFading(eveBody);

        renderOnlyOne(CellState.Default);
    }

    void renderOnlyOne(string currentOne) {
        transform.Find(currentOne).gameObject.SetActive(true);

        if (currentOne != CellState.Adam) {
            transform.Find(CellState.Adam).gameObject.SetActive(false);
        }

        if (currentOne != CellState.Eve) {
            transform.Find(CellState.Eve).gameObject.SetActive(false);
        }

        if (currentOne != CellState.Default) {
            transform.Find(CellState.Default).gameObject.SetActive(false);
        }
    }

    void configureToAllowFading(MeshRenderer mesh) {
        mesh.material.SetFloat("_Mode", 2);
        mesh.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mesh.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mesh.material.SetInt("_ZWrite", 0);
        mesh.material.DisableKeyword("_ALPHATEST_ON");
        mesh.material.EnableKeyword("_ALPHABLEND_ON");
        mesh.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mesh.material.renderQueue = 3000;
    }

    // fade in
    IEnumerator fadeIn(MeshRenderer mesh) {
        Color color = mesh.material.color;
        color.a = 0;
        mesh.material.color = color;
        while (color.a < 1) {
            color.a += 0.01f;
            mesh.material.color = color;
            yield return new WaitForSeconds(0.001f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchToAdam() {
        state = CellState.Adam;
        StartCoroutine(fadeIn(adamBody));
    }

    private void SwitchToEve() {
        state = CellState.Eve;
        StartCoroutine(fadeIn(eveBody));
    }

    private bool isRotating = false;

    private IEnumerator RefreshTile() {
        // rotate -180 degree during 1 second and then back

        isRotating = true;

        float expectedTime = 1f;
        float totalTime = 0f;

        Vector3 start = new Vector3(0, 0, 0);
        Vector3 end = new Vector3(-180, 0, 0);
        // Vector3 almostEnd = new Vector3(-170, 0, 0);

        // rotate for one second

        while (totalTime <= expectedTime) {
            totalTime += Time.deltaTime;

            // if (Quaternion.Angle(transform.rotation, Quaternion.Euler(end)) <= 0.01f) {
            //     break;
            // }

            transform.Rotate(Vector3.Lerp(start, end, Time.deltaTime));

            yield return new WaitForSeconds(0.001f);
        }

        transform.rotation = Quaternion.identity;

        isRotating = false;
    }

    private IEnumerator RefreshTileBack() {
        float expectedTime = 1f;
        float totalTime = 0f;

        Vector3 start = new Vector3(-180, 0, 0);
        Vector3 end = new Vector3(0, 0, 0);

        while (totalTime <= expectedTime) {
            totalTime += Time.deltaTime;

            transform.Rotate(Vector3.Lerp(start, end, Time.deltaTime));

            yield return new WaitForSeconds(0.001f);
        }

        isRotating = false;
    }

    void OnMouseDown()
    {
        if (isRotating) return;

        print(gameObject.name + " " + state);

        var board = GameObject.Find("OneBoard").GetComponent<SecondScript>();

        StartCoroutine(RefreshTile());

        if (state == CellState.Default) {
            if (board.isAdamMove) {
                renderOnlyOne(CellState.Adam);
                SwitchToAdam();
            } else {
                renderOnlyOne(CellState.Eve);
                SwitchToEve();
            }
        } else if (state == CellState.Adam) {
            renderOnlyOne(CellState.Eve);
            SwitchToEve();
        } else if (state == CellState.Eve) {
            renderOnlyOne(CellState.Adam);
            SwitchToAdam();
        }

        bool win = board.OnMouseDown(gameObject.name);

        // if (win) {
            // renderOnlyOne(CellState.Default);
            // state = CellState.Default;
        // }
    }

    // upon collision
    void onTriggerEnter(Collider other) {
        print("THIRD SCRIPT");
        // print("collision on " + gameObject.name + " " + other.gameObject.name);
    }
}
