using UnityEngine;

public class PlayWithPetAction : IPetClickAction
{
    private readonly PetHappinessStat petHappinessStat;

    public PlayWithPetAction(PetHappinessStat petHappinessStat)
    {
        this.petHappinessStat = petHappinessStat;
    }

    /// Обрабатывает клик по питомцу, запуская игру
    public void OnPetClicked(GameObject pet)
    {
        if (petHappinessStat != null)
        {
            petHappinessStat.PlayWithPet();
        }
        else
        {
            Debug.LogWarning("PetHappinessStat не задан в PetClickHandler!");
        }
    }
}