using UnityEngine;

public class InputController : MonoBehaviour {

    [SerializeField]
    GameObject _weaponPrefab;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.CompareTag("Ground")) {
                    Block block = hit.transform.GetComponent<Block>();
                    DeployWeapon(block);
                }
            }
        }
    }

    void DeployWeapon(Block block) {
        Vector3 blockPos = block.GetPosition();
        Vector3 weaponPos = new Vector3(blockPos.x, _weaponPrefab.transform.position.y, blockPos.z);

        Instantiate(_weaponPrefab, weaponPos, Quaternion.identity);
    }
}