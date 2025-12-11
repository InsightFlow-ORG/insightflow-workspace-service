using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Data
{
    /// <summary>
    /// Clase encargada de poblar datos iniciales en el contexto de la base de datos en memoria.
    /// </summary>
    public class Seeder
    {
        /// <summary>
        /// Método para poblar datos iniciales en el contexto.
        /// </summary>
        /// <param name="context">Contexto de la base de datos en memoria</param>
        public static void Seed(Context context)
        {
            for (int i = 0; i <= 10; i++)
            {
                var Id = Guid.NewGuid();
                context.Workspaces.Add(new Workspace
                {
                    Id = Id,
                    Name = $"Workspace {i + 1}",
                    Description = $"This is the description for Workspace {i + 1}",
                    Theme = "astral",
                    OwnerId = Guid.Parse("b79cf89e-0b7e-4fb7-be91-2105e56cf6e1"),
                    Members = new List<WorkspaceMember>()
                    {
                        new WorkspaceMember
                        {
                            Id = Guid.Parse("b79cf89e-0b7e-4fb7-be91-2105e56cf6e1"),
                            UserName = "Jhon",
                            Role = "Owner"
                        },
                        
                        new WorkspaceMember
                        {
                            Id = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"),
                            UserName = "Raul",
                            Role = "Editor"
                        }
                    },
                });

                Console.WriteLine(Id);
            }
        }
    }
}