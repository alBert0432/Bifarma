using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bifarma.Data.Models;
using Bifarma.Data.Repositories;
using Bifarma.Services;

namespace Bifarma.Pages.Masters.MasterMolecules
{
    public class MoleculesModel : PageModel
    {
        private IMoleculesRepository _MoleculesRepository;
        BifarmaConnection _conn;
        public List<Molecules> listMol { get; set; }

        public MoleculesModel(IMoleculesRepository moleculesRepository)
        {
            _MoleculesRepository = moleculesRepository;
        }

        public void OnGet()
        {
            LoadData();
        }

        public void LoadData()
        {
            listMol = _MoleculesRepository.GetMolecules();
        }

        public void OnPostAddMol(string MoleculeName, string MolDescription, string isActive)
        {
            _MoleculesRepository.OnPostAddMolecules(MoleculeName, MolDescription, isActive);
            LoadData();
        }

        public void OnPostEditMol(string idMolecules, string MoleculeName, string MolDescription, string isActive)
        {
            _MoleculesRepository.OnPostEditMolecules(idMolecules, MoleculeName, MolDescription, isActive);
            LoadData();
        }

        public void OnPostDeleteMol(string idMolecules)
        {
            _MoleculesRepository.OnPostDeleteMolecules(idMolecules);
            LoadData();
        }
    }
}
