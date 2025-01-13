namespace ApiRestoSimplon.Class
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Prenom { get; set; }
        public required string Adress { get; set; }
        public required string PhoneNumber { get; set; }

    }

    public class ClientDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Prenom { get; set; }
        public required string Adress { get; set; }
        public required string PhoneNumber { get; set; }

        public ClientDTO() { }

        public ClientDTO(Client client)
        {
            Id = client.Id;
            Name = client.Name;
            Prenom = client.Prenom;
            Adress = client.Adress;
            PhoneNumber = client.PhoneNumber;
        }
    }
}