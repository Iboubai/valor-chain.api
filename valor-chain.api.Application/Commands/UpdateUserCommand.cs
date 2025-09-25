using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace valor_chain.api.Application.Commands
{
    public class UpdateUserCommand
    {
        // On ne met que les champs que l'utilisateur a le droit de modifier.
        // On peut utiliser des annotations de validation pour s'assurer que les données sont correctes.

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir entre 2 et 50 caractères.")]
        public string? FirstName { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom de famille doit contenir entre 2 et 50 caractères.")]
        public string? LastName { get; set; }

        // On pourrait ajouter une validation plus complexe pour le numéro de téléphone ici.
        public string? PhoneNumber { get; set; }

        // Note : L'email n'est généralement pas inclus car sa modification est un processus plus complexe (vérification, etc.).
    }
}
