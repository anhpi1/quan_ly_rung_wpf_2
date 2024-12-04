using quan_ly_rung_wpf_2.Models;
using System.Data;

namespace quan_ly_rung_wpf_2.Controllers
{
    public class AnimalSpeciesController
    {
        private AnimalSpeciesModel model;

        public AnimalSpeciesController()
        {
            model = new AnimalSpeciesModel();
        }

        public DataTable LoadAllAnimalSpecies()
        {
            return model.GetAllAnimalSpecies();
        }

        public void AddAnimalSpecies(string name, string food, string disease, string quantity, string description)
        {
            model.AddAnimalSpecies(name, food, disease, quantity, description);
        }

        public void DeleteAnimalSpecies(int id)
        {
            model.DeleteAnimalSpecies(id);
        }
    }
}
