namespace API_1.Reposetories
{
    public interface ICatigoryRepo
    {

        IEnumerable<Catigory> GetAll();

        Catigory GetById(int id);

        Catigory GetByName(string name);

        void Create(Catigory product);

        void Update(Catigory product);

        void Delete(int id);

        void Save();

    }
}
