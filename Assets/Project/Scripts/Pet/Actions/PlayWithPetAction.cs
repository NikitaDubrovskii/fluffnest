using UnityEngine;

public class PlayWithPetAction : IPetClickAction
{
    private readonly PetHappinessStat petHappinessStat;

    public PlayWithPetAction(PetHappinessStat petHappinessStat)
    {
        this.petHappinessStat = petHappinessStat;
    }

    public void OnPetClicked(GameObject pet)
    {
        if (petHappinessStat != null)
        {
            petHappinessStat.PlayWithPet();
            Debug.Log("Питомец играет через контроллер! 🐱❤️");
        }
        else
        {
            Debug.LogWarning("PetHappinessStat не задан в PetClickHandler!");
        }
    }
}