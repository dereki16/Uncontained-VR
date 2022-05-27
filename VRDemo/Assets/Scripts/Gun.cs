using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed = 20f;
    public GameObject bullet;
    public Transform barrel;
    public Transform muzzleTransform;
    public GameObject muzzle;

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward / 2;
        Destroy(spawnedBullet, 2f);
        Instantiate(muzzle, barrel.position, muzzleTransform.rotation);
    }
}
