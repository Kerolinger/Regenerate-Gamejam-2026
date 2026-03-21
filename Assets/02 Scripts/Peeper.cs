

using UnityEngine;

public class Peeper : MonoBehaviour
{
    [Header("Please leave empty, will be filled out via code")]
    [SerializeField] private PeeperProfile currentPeeperProfile;

    [Header ("Prefab References")]
    [SerializeField] private MeshRenderer peeperMaterial;
    [SerializeField] private MeshRenderer ingredientMaterial;

    private GameManager gameManager;

    public PeeperProfile CurrentPeeperProfile { get => currentPeeperProfile; set => currentPeeperProfile = value; }

    public void SetPeeper(PeeperProfile newPeeperProfile, Material newIngredientMaterial, GameManager gameManagerRef)
    {
        currentPeeperProfile = newPeeperProfile;

        peeperMaterial.material = currentPeeperProfile.DefaultMaterial;
        ingredientMaterial.material = newIngredientMaterial;
        gameManager = gameManagerRef;

    }

    //void Update()
    //{
    //    if (gameManager == null)
    //        return;

    //    transform.LookAt(gameManager.currentCamera.transform.position, UnityEngine.Vector3.up);
    //    transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
    //}
}
