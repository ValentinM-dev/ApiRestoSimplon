namespace ApiRestoSimplon.Class
{
    public class Categorie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }

    public class CategorieDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public CategorieDTO() { }

        public CategorieDTO(Categorie categorie)
        {
            Id = categorie.Id;
            Name = categorie.Name;
        }
    }
}

