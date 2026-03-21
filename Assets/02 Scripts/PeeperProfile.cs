using UnityEngine;

[CreateAssetMenu(fileName = "PeeperProfile", menuName = "Scriptable Objects/PeeperProfile")]
public class PeeperProfile : ScriptableObject
{
    [Header("Basic Information")]
    [SerializeField] private string name;
    [SerializeField] private Material defaultMaterial;
    [Space]
    [SerializeField] private Ingredients ingredient = Ingredients.none;
    [SerializeField] private Ingredients ingredientDislikes;
    [SerializeField] private Skills[] skillOffer;
    [SerializeField] private Skills skillNeeded = Skills.none;

    [Header("Narrative Bits - Stage 01")]
    [SerializeField] [TextArea (2,2)] private string st01_ingredientText;
    [SerializeField][TextArea(2, 2)] private string st01_ingredientDislikeText;
    [SerializeField][TextArea(2, 2)] private string st01_skillOfferText;
    [SerializeField][TextArea(2, 2)] private string st01_skillNeedText;

    [Header("Narrative Bits - Stage 03")]
    [SerializeField][TextArea(2, 2)] private string st03_soupResultText;
    [SerializeField][TextArea(2, 2)] private string st03_skillOfferText;
    [SerializeField][TextArea(2, 2)] private string st03_skillNeedText;

    public Material DefaultMaterial { get => defaultMaterial; set => defaultMaterial = value; }
    public string Name { get => name; set => name = value; }
    public Ingredients Ingredient { get => Ingredient1; set => Ingredient1 = value; }
    public Ingredients Ingredient1 { get => ingredient; set => ingredient = value; }
    public Ingredients IngredientDislikes { get => ingredientDislikes; set => ingredientDislikes = value; }
    public Skills SkillNeeded { get => skillNeeded; set => skillNeeded = value; }
    public string St01_ingredientText { get => st01_ingredientText; set => st01_ingredientText = value; }
    public string St01_ingredientDislikeText { get => st01_ingredientDislikeText; set => st01_ingredientDislikeText = value; }
    public string St01_skillOfferText { get => st01_skillOfferText; set => st01_skillOfferText = value; }
    public string St01_skillNeedText { get => st01_skillNeedText; set => st01_skillNeedText = value; }
    public Skills[] SkillOffer { get => skillOffer; set => skillOffer = value; }

    public enum Ingredients { none, ingredient01, ingredient02, ingredient03, ingredient04, ingredient05, ingredient06};
    public enum Skills { none, skill01, skill02, skill03, skill04, skill05, skill06};
}
