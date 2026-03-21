using System;
using UnityEngine;

public class Peeper : MonoBehaviour
{
    [Header("Please leave empty, will be filled out via code")]
    [SerializeField] private PeeperProfile currentPeeperProfile;

    [Header ("Prefab References")]
    [SerializeField] private MeshRenderer peeperMaterial;
    [SerializeField] private MeshRenderer ingredientMaterial;

    public PeeperProfile CurrentPeeperProfile { get => currentPeeperProfile; set => currentPeeperProfile = value; }

    public void SetPeeper(PeeperProfile newPeeperProfile, Material newIngredientMaterial)
    {
        currentPeeperProfile = newPeeperProfile;

        peeperMaterial.material = currentPeeperProfile.DefaultMaterial;
        ingredientMaterial.material = newIngredientMaterial;
    }
}
