using Bifarma.Data.Models;
using Bifarma.Services;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Bifarma.Data.Repositories
{
    public interface IMoleculesRepository
    {
        List<Molecules> GetMolecules();
        void OnPostAddMolecules(string moleculesName, string molDescription, string isActive);
        void OnPostEditMolecules(string idMolecules, string moleculesName, string molDescription, string isActive);
        void OnPostDeleteMolecules(string idMolecules);
    }

    public class MoleculesRepository : IMoleculesRepository
    {
        private List<Molecules> listMolecules;
        BifarmaConnection _conn;

        public MoleculesRepository(BifarmaConnection conn)
        {
            _conn = conn;
        }

        public List<Molecules> GetMolecules()
        {
            listMolecules = new List<Molecules>();

            var query = @"
                SELECT * FROM m_molecules            
            ";

            var dt = _conn.GetData(query);

            foreach (DataRow dr in dt.Rows)
            {
                var newMolecules = new Molecules();

                newMolecules.idMolecules = dr["idMolecules"].ToString();
                newMolecules.MoleculesName = dr["MoleculesName"].ToString();
                newMolecules.MolDescription = dr["MolDescription"].ToString();
                newMolecules.isActive = dr["isActive"].ToString();
                newMolecules.State = dr["State"].ToString();

                listMolecules.Add(newMolecules);
            }
            return listMolecules;
        }


        public void OnPostAddMolecules(string moleculesName, string molDescription, string isActive)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string idMolecules = new string(Enumerable.Repeat(chars, 15)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            string query = "INSERT INTO m_molecules(idMolecules, MoleculesName, MolDescription, isActive, State) " +
                "VALUES (@string1, @string2, @string3, @string4, '-')";
            _conn.InsertOrUpdateData(query, new List<string> { idMolecules, moleculesName, molDescription, isActive });
        }

        public void OnPostEditMolecules(string idMolecules, string moleculesName, string molDescription, string isActive)
        {
            string query = "UPDATE m_molecules SET MoleculesName = @string1, MolDescription = @string2, " +
                "isActive = @string3 WHERE idMolecules = @string4";
            _conn.InsertOrUpdateData(query, new List<string> { moleculesName, molDescription, isActive, idMolecules});
        }

        public void OnPostDeleteMolecules(string idMolecules)
        {
            string query = "DELETE m_molecules WHERE idMolecules = @string1";
            _conn.DeleteData(query, new List<string> { idMolecules });
        }
    }
}
