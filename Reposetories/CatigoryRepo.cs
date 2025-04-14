using API_1.MyAppContext;
using Microsoft.EntityFrameworkCore;

namespace API_1.Reposetories
{
    public class CatigoryRepo : ICatigoryRepo
    {

        private readonly AppDbContext _context;

        public CatigoryRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Catigory Catigory)
        {
            _context.Catigories.Add(Catigory);
            _context.SaveChanges();

        }
        public void Delete(int id)
        {
            _context.Catigories.Remove(_context.Catigories.FirstOrDefault(en => en.Id == id));
            _context.SaveChanges();
        }

        public IEnumerable<Catigory> GetAll()
        {
            IEnumerable<Catigory> Catigorys = _context.Catigories.Include(en => en.Products).ToList();
            return Catigorys;

        }

        public Catigory GetById(int id)
        {
            Catigory? Catigory = _context.Catigories.Include(en => en.Products).FirstOrDefault(en => en.Id == id);
            return Catigory;
        }

        public void Save()
        {
            _context.SaveChanges();

        }

        public void Update(Catigory Catigory)
        {
            _context.Catigories.Update(Catigory);
            _context.SaveChanges();
        }





        //ألزيادة عن الانترفيس
        public Catigory GetByName(string name)
        {
            Catigory? Catigory = _context.Catigories.FirstOrDefault(en => en.Name == name);
            return Catigory;
        }
    }
}
