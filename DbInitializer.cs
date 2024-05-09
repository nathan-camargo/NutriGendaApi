using NutriGendaApi.Source.Data;

namespace NutriGendaApi
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Garante que o banco de dados foi criado
            context.Database.EnsureCreated();

            // Verifica se já existem nutricionistas no banco de dados
            if (context.Nutritionists.Any())
            {
                return;   // DB já foi inicializado
            }

            // Cria um novo nutricionista
            var nutritionists = new Nutritionist[]
            {
            new Nutritionist
            {
                Name = "Dr. John Doe",
                Email = "john.doe@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345"), // Utilize um hash real aqui
                Crn = "CRN123456"
            }
            };

            foreach (Nutritionist n in nutritionists)
            {
                context.Nutritionists.Add(n);
            }
            context.SaveChanges();
        }
    }
}
